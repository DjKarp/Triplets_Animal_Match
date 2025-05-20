using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace TripletsAnimalMatch
{
    public class TileFactory : MonoBehaviour
    {
        private TileData _tileData;
        private GameplayData _gameplayData;
        private SignalBus _signalBus;

        private Tile tile;
        private TileModel _tileModel;
        private List<Tile> tiles = new List<Tile>();
        private Dictionary<TileData.TileEffect, int> tileEffectCounts = new Dictionary<TileData.TileEffect, int>();

        [Inject]
        public void Construct(TileData tileData, GameplayData gameplayData, SignalBus signalBus)
        {
            _tileData = tileData;
            _gameplayData = gameplayData;
            _signalBus = signalBus;
        }

        public List<Tile> Create(Transform transform, int tilesCount = 0)
        {
            tiles.Clear();
            tileEffectCounts.Clear();

            var random = new System.Random();

            // Считаем, сколько нужно уникальных тайлов, чтобы пв первую очередь создать их. Потом список будет перемешан. 
            int tilesWhitEffectCount = _gameplayData.TileEffectCount.Sum(r => r.Count);

            List<TileModel> tileModels = new List<TileModel>(CreateUniqueTileModels(tilesCount == 0 ? _gameplayData.MaxCountTiles : tilesCount));

            foreach (TileModel model in tileModels)
            {
                tile = null;
                for (int i = 0; i < _gameplayData.MatchCountTiles; i++)
                {
                    TileModel modifyModel = new TileModel(model.Shape, model.Color, model.AnimalType);
                    int weight = random.Next(0, _gameplayData.MaxCountTiles - tiles.Count);

                    if (weight < tilesWhitEffectCount)
                    {
                        modifyModel.TileEffect = GetTileEffect(ref tileEffectCounts);
                        tilesWhitEffectCount--;
                    }

                    tile = Instantiate(GetTilePrefabByEffect(modifyModel), transform);

                    tile.Init(
                        modifyModel, 
                        GetShapeSprite((int)modifyModel.Shape, (int)modifyModel.Color), 
                        _tileData.AnimalTexture[(int)modifyModel.AnimalType], 
                        _tileData.ShapesColliders[(int)modifyModel.Shape], 
                        _signalBus);

                    tile.gameObject.SetActive(false);

                    tiles.Add(tile);
                }
            }

            // Вернём в первый раз перемешанный список
            return tiles.OrderBy(_ => random.Next()).ToList();
        }

        private List<TileModel> CreateUniqueTileModels(int maxTilesCount)
        {
            int tempUniqueTilesCount = maxTilesCount / _gameplayData.MatchCountTiles;
            List<TileModel> _tileModels = new List<TileModel>();
            var random = new System.Random();

            for (int i = 0; i < tempUniqueTilesCount; i++)
            {
                do
                {
                    _tileModel = new TileModel(
                        (TileData.Shape)random.Next(0, System.Enum.GetNames(typeof(TileData.Shape)).Length),
                        (TileData.Color)random.Next(0, System.Enum.GetNames(typeof(TileData.Color)).Length),
                        (TileData.AnimalType)random.Next(0, System.Enum.GetNames(typeof(TileData.AnimalType)).Length));
                }
                while (_tileModels.Contains(_tileModel));

                _tileModels.Add(_tileModel);
            }

            return _tileModels;
        }

        private Sprite GetShapeSprite(int shapeNumber, int colorNumber)
        {
            switch (shapeNumber)
            {
                default:
                case 0:
                    return _tileData.ShapesCircle[colorNumber];

                case 1:
                    return _tileData.ShapesHexagon[colorNumber];

                case 2:
                    return _tileData.ShapesPentagon[colorNumber];

                case 3:
                    return _tileData.ShapesRectangle[colorNumber];
            }
        }

        private TileData.TileEffect GetTileEffect(ref Dictionary<TileData.TileEffect, int> tileEffectCounts)
        {
            foreach (TileEffectCount tileEffectOnData in _gameplayData.TileEffectCount)
            {
                if (tileEffectCounts.ContainsKey(tileEffectOnData.TileEffect) == false)
                {
                    tileEffectCounts.Add(tileEffectOnData.TileEffect, 1);
                    return tileEffectOnData.TileEffect;
                }
                else if (tileEffectCounts.ContainsKey(tileEffectOnData.TileEffect) && tileEffectCounts[tileEffectOnData.TileEffect] < tileEffectOnData.Count)
                {
                    tileEffectCounts[tileEffectOnData.TileEffect]++;
                    return tileEffectOnData.TileEffect;
                }
            }

            return TileData.TileEffect.None;
        }

        private Tile GetTilePrefabByEffect(TileModel model)
        {
            foreach (TilePrefabByEffect tileEffect in _tileData.TilePrefabByEffects)
                if (tileEffect.TileEffect == model.TileEffect)
                    return tileEffect.Tile;

            return _tileData.Tile;
        }
    }
}
