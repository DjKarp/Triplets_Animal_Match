using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace TripletsAnimalMatch
{
    public class GameModel : MonoBehaviour
    {
        private GamePresenter _gamePresenter;
        private TileData _tileData;
        private GameplayData _gameplayData;
        private TopPanel _topPanel;
        private SignalBus _signalBus;

        private Transform _transform;

        private List<Tile> _activeTiles = new List<Tile>();
        public List<Tile> ActiveTiles { get => _activeTiles; set => _activeTiles = value; }
        private Dictionary<string, List<Tile>> _topPanelsGroup = new Dictionary<string, List<Tile>>();

        [Inject]
        public void Construct(GamePresenter gamePresenter, TileData tileData, GameplayData gameplayData, TopPanel topPanel, SignalBus signalBus)
        {
            _gamePresenter = gamePresenter;
            _tileData = tileData;
            _gameplayData = gameplayData;
            _topPanel = topPanel;

            _signalBus = signalBus;
        }

        private void Awake()
        {
            _transform = gameObject.transform;
        }

        public bool IsGameOver()
        {
            return _topPanel.TilesContainer.All(x => x != null);
        }

        public bool IsWinner()
        {
            return _topPanel.TilesContainer.All(x => x == null) && _activeTiles.Count == 0;
        }

        public List<Tile> CreateTiles(int tilesCount = 0)
        {
            _activeTiles.Clear();
            List<Tile> tiles = new List<Tile>();
            Tile tile;
            var random = new System.Random();

            List<TileModel> tileModels = new List<TileModel>(CreateUniqueTileModels(tilesCount == 0 ? _gameplayData.MaxCountTiles : tilesCount));

            foreach (TileModel model in tileModels)
            {
                tile = null;
                for (int i = 0; i < _gameplayData.MatchCountTiles; i++)
                {
                    tile = Instantiate(_tileData.Tile, _transform);
                    tile.Init(model, GetShapeSprite((int)model.Shape, (int)model.Color), _tileData.AnimalTexture[(int)model.AnimalType], _tileData.ShapesColliders[(int)model.Shape], _signalBus);
                    tile.gameObject.SetActive(false);
                    tiles.Add(tile);
                }
            }

            _activeTiles = tiles.OrderBy(_ => random.Next()).ToList();

            return _activeTiles;
        }

        public List<Tile> CheckMatch()
        {
            _topPanelsGroup.Clear();

            foreach (Tile tile in _topPanel.TilesContainer)
            {
                if (tile != null)
                {
                    string key = tile.TileModel.GetKey();

                    if (!_topPanelsGroup.ContainsKey(key))
                        _topPanelsGroup.Add(key, new List<Tile>());
                    _topPanelsGroup[key].Add(tile);

                    if (_topPanelsGroup[key].Count == _gameplayData.MatchCountTiles)
                    {
                        _activeTiles.RemoveAll(tile => _topPanelsGroup[key].Contains(tile));
                        return _topPanelsGroup[key];
                    }
                }
            }

            return null;
        }

        public void RemoveTileFromListActiveTiles(Tile tile)
        {
            _activeTiles.Remove(tile);
        }        

        public int GetTilesCount()
        {
            return _activeTiles.Count + _topPanel.PlaceUseCount;
        }

        public bool IsAllClickedTileMoveOnPanel()
        {
            return _topPanel.IsAllClickedTileMoveOnPanel();
        }

        private List<TileModel> CreateUniqueTileModels(int maxTilesCount)
        {
            int tempUniqueTilesCount = maxTilesCount / _gameplayData.MatchCountTiles;
            TileModel tileModel;
            List<TileModel> _tileModels = new List<TileModel>();
            var random = new System.Random();

            for (int i = 0; i < tempUniqueTilesCount; i++)
            {
                do
                {
                    tileModel = new TileModel(
                        (TileData.Shape)random.Next(0, System.Enum.GetNames(typeof(TileData.Shape)).Length),
                        (TileData.Color)random.Next(0, System.Enum.GetNames(typeof(TileData.Color)).Length),
                        (TileData.AnimalType)random.Next(0, System.Enum.GetNames(typeof(TileData.AnimalType)).Length));
                }
                while (_tileModels.Contains(tileModel));
                
                _tileModels.Add(tileModel);
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
    }
}
