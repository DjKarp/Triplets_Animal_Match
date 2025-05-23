using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace TripletsAnimalMatch
{
    public class TilesPool
    {
        private TileData _tileData;
        private SignalBus _signalBus;

        private List<Tile> _tiles = new List<Tile>();
        private List<Tile> _tilesTake = new List<Tile>();

        private Transform _parentTransform;

        public TilesPool(Transform parentTransform, TileData tileData, SignalBus signalBus)
        {
            _parentTransform = parentTransform;
            _tileData = tileData;
            _signalBus = signalBus;
        }

        public void Init(List<TileModel> _tileModels)
        {
            _tilesTake.Clear();

            foreach (TileModel model in _tileModels)
            {
                Tile tile = Create(model.TileEffect);

                tile.Init(
                    model,
                    GetShapeSprite((int)model.Shape, (int)model.Color),
                    _tileData.AnimalTexture[(int)model.AnimalType],
                    _tileData.ShapesColliders[(int)model.Shape],
                    _signalBus);
            }
        }

        public void Refresh(List<TileModel> _tileModels)
        {
            _tilesTake.Clear();

            foreach (TileModel model in _tileModels)
            {
                Tile tile = Get(model.TileEffect);

                tile.Init(
                    model,
                    GetShapeSprite((int)model.Shape, (int)model.Color),
                    _tileData.AnimalTexture[(int)model.AnimalType],
                    _tileData.ShapesColliders[(int)model.Shape]);
            }

            _tilesTake.Clear();
        }

        public Tile Get(TileData.TileEffect tileEffect)
        {
            Tile tile = _tiles.FirstOrDefault(t => _tilesTake.Contains(t) == false && t.TileModel.TileEffect == tileEffect);
            _tilesTake.Add(tile);

            return tile;
        }

        public void Release(Tile tile)
        {
            tile.gameObject.SetActive(false);
        }

        private Tile Create(TileData.TileEffect tileEffect)
        {
            _tiles.Add(Object.Instantiate(GetTilePrefabByEffect(tileEffect), _parentTransform));
            _tiles.LastOrDefault().gameObject.SetActive(false);

            return _tiles.LastOrDefault();
        }        

        private Tile GetTilePrefabByEffect(TileData.TileEffect tileEffect)
        {
            foreach (TilePrefabByEffect tilePrefabByEffect in _tileData.TilePrefabByEffects)
                if (tilePrefabByEffect.TileEffect == tileEffect)
                    return tilePrefabByEffect.Tile;

            return _tileData.Tile;
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
    }
}
