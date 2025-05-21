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

        private Tile _tile;
        private TileModel _tileModel;
        private List<Tile> _tiles = new List<Tile>();
        List<TileModel> _tileModels = new List<TileModel>();
        private Dictionary<TileData.TileEffect, int> tileEffectCounts = new Dictionary<TileData.TileEffect, int>();

        [Inject]
        public void Construct(TileData tileData, GameplayData gameplayData, SignalBus signalBus)
        {
            _tileData = tileData;
            _gameplayData = gameplayData;
            _signalBus = signalBus;
        }

        public void Init(List<Tile> tiles)
        {
            _tiles = tiles;
            var random = new System.Random();
            CreateUniqueTileModels(_tiles.Count == 0 ? _gameplayData.MaxCountTiles : _tiles.Count);

            // Считаем, сколько нужно уникальных тайлов, чтобы в первую очередь создать их. Потом список будет перемешан. 
            int tilesWhitEffectCount;

            if (_tiles.Count ==0)
            {
                tileEffectCounts.Clear();
                tilesWhitEffectCount = _gameplayData.TileEffectCount.Sum(r => r.Count);

                for (int i = 0; i < _tileModels.Count; i++)
                {
                    TileData.TileEffect tileEffect = TileData.TileEffect.None;
                    int weight = random.Next(0, _tileModels.Count - i);

                    if (weight < tilesWhitEffectCount)
                    {
                        tileEffect = GetTileEffect(ref tileEffectCounts);
                        tilesWhitEffectCount--;
                    }

                    _tileModels[i].TileEffect = tileEffect;
                }
            }
            else
            {
                for (int i = 0; i < _tileModels.Count; i++)
                { 
                    _tileModels[i].TileEffect = _tiles[i].TileModel.TileEffect;
                }
            }
        }

        public List<Tile> Create(Transform transform)
        {
            _tiles.Clear();
            var random = new System.Random();

            foreach (TileModel model in _tileModels)
            {
                _tile = null;

                _tile = Instantiate(GetTilePrefabByEffect(model.TileEffect), transform);

                _tile.Init(
                    model,
                    GetShapeSprite((int)model.Shape, (int)model.Color),
                    _tileData.AnimalTexture[(int)model.AnimalType],
                    _tileData.ShapesColliders[(int)model.Shape],
                    _signalBus);

                _tile.gameObject.SetActive(false);

                _tiles.Add(_tile);
            }

            // Вернём в первый раз перемешанный список
            return _tiles.OrderBy(_ => random.Next()).ToList();
        }

        private void CreateUniqueTileModels(int maxTilesCount)
        {
            _tileModels.Clear();
            var random = new System.Random();
            int tempUniqueTilesCount = maxTilesCount / _gameplayData.MatchCountTiles;            

            for (int i = 0; i < tempUniqueTilesCount; i++)
            {
                do
                {
                    _tileModel = new TileModel(
                        (TileData.Shape)random.Next(0, System.Enum.GetNames(typeof(TileData.Shape)).Length),
                        (TileData.Color)random.Next(0, System.Enum.GetNames(typeof(TileData.Color)).Length),
                        (TileData.AnimalType)random.Next(0, System.Enum.GetNames(typeof(TileData.AnimalType)).Length));
                }
                while (CheckModelOnUnique(_tileModel));

                for (int j = 0; j < _gameplayData.MatchCountTiles; j++)
                {
                    _tileModels.Add(new TileModel(_tileModel));
                }
            }
        }

        private bool CheckModelOnUnique(TileModel tileModel)
        {
            foreach (TileModel model in _tileModels)
                if (tileModel.GetKey() == model.GetKey())
                    return true;

            return false;
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

        private Tile GetTilePrefabByEffect(TileData.TileEffect tileEffect)
        {
            foreach (TilePrefabByEffect tilePrefabByEffect in _tileData.TilePrefabByEffects)
                if (tilePrefabByEffect.TileEffect == tileEffect)
                    return tilePrefabByEffect.Tile;

            return _tileData.Tile;
        }
    }
}
