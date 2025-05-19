using System;
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

        private AudioService _audioService;

        private Transform _transform;

        private List<Tile> _activeTiles = new List<Tile>();
        public List<Tile> ActiveTiles { get => _activeTiles; set => _activeTiles = value; }
        private Dictionary<string, List<Tile>> _topPanelsGroup = new Dictionary<string, List<Tile>>();        

        [Inject]
        public void Construct(GamePresenter gamePresenter, TileData tileData, GameplayData gameplayData, TopPanel topPanel, AudioService audioService, SignalBus signalBus)
        {
            _gamePresenter = gamePresenter;
            _tileData = tileData;
            _gameplayData = gameplayData;
            _topPanel = topPanel;
            _audioService = audioService;

            _signalBus = signalBus;
        }

        private void Awake()
        {
            _transform = gameObject.transform;

            _signalBus.Subscribe<TileOnFinishSignal>(CheckConditions);
        }

        public bool IsGameOver()
        {
            return _topPanel.TilesContainer.All(x => x != null) || (_topPanel.TilesContainer.Where(x => x != null).Any() && _activeTiles.Count == 0);
        }

        public bool IsWinner()
        {
            return _topPanel.TilesContainer.All(x => x == null) && _activeTiles.Count == 0;
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
                        CheckExplodindTile(_topPanelsGroup[key]);

                        return _topPanelsGroup[key];
                    }
                }
            }

            return null;
        }

        public void CheckExplodindTile(List<Tile> tiles)
        {
            foreach (Tile tile in tiles.Where(t => t is TileExploding).ToList())
            {
                int indexOnTopPanel = Array.IndexOf(_topPanel.TilesContainer, tile);

                if (indexOnTopPanel > 0)
                {
                    Tile checkTileLeft = _topPanel.TilesContainer[indexOnTopPanel - 1];

                    if (checkTileLeft != null && tiles.Contains(checkTileLeft) == false)
                    {
                        _topPanel.RemoveTileFromPanel(checkTileLeft, true);
                        _audioService.PlayGameplayAudio(AudioService.AudioGameplay.ExplousenTile);
                    }
                }

                if (indexOnTopPanel < _topPanel.TilesContainer.Length)
                {
                    Tile checkTileRight = _topPanel.TilesContainer[indexOnTopPanel + 1];

                    if (checkTileRight != null && tiles.Contains(checkTileRight) == false)
                    {
                        _topPanel.RemoveTileFromPanel(checkTileRight, true);
                        _audioService.PlayGameplayAudio(AudioService.AudioGameplay.ExplousenTile);
                    }
                }
            }
        }

        private void CheckConditions(TileOnFinishSignal tileOnFinishSignal)
        {
            _gamePresenter.CheckOnWin();
            CheckFrozedTiles();
        }

        private void CheckFrozedTiles()
        {
            if (_gameplayData.MaxCountTiles - GetTilesCount() > _gameplayData.NumberTilesToUnfreeze && _activeTiles.OfType<TileFrozen>().Where(t => t.IsFreezed).Any())
                foreach (TileFrozen tile in _activeTiles.OfType<TileFrozen>().Where(t => t.IsFreezed).ToList())
                    tile.Unfreeze();
        }

        public List<Tile> CreateTiles(int tilesCount = 0)
        {
            _activeTiles.Clear();

            Tile tile;
            List<Tile> tiles = new List<Tile>();
            Dictionary<TileData.TileEffect, int> tileEffectCounts = new Dictionary<TileData.TileEffect, int>();
            var random = new System.Random();

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

                    tile = Instantiate(GetTilePrefabByEffect(modifyModel), _transform);
                    tile.Init(modifyModel, GetShapeSprite((int)modifyModel.Shape, (int)modifyModel.Color), _tileData.AnimalTexture[(int)modifyModel.AnimalType], _tileData.ShapesColliders[(int)modifyModel.Shape], _signalBus);
                    tile.gameObject.SetActive(false);

                    tiles.Add(tile);
                }
            }  
            
            _activeTiles = tiles.OrderBy(_ => random.Next()).ToList();

            return _activeTiles;
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
