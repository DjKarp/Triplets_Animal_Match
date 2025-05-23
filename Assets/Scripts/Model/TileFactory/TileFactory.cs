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

        private TilesPool _tilesPool;

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

            if (_tilesPool == null)
            {
                _tilesPool = new TilesPool(transform, _tileData, _signalBus);
                _tilesPool.Init(_tileModels);
            }
            else
                _tilesPool.Refresh(_tileModels);
        }

        public List<Tile> Create()
        {
            _tiles.Clear();
            var random = new System.Random();

            foreach (TileModel model in _tileModels)
            {
                _tile = null;
                _tile = _tilesPool.Get(model.TileEffect);
                _tiles.Add(_tile);
            }

            // Вернём в первый раз перемешанный список
            return _tiles.OrderBy(_ => random.Next()).ToList();
        }

        public void Release(Tile tile)
        {
            _tilesPool.Release(tile);
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
    }
}
