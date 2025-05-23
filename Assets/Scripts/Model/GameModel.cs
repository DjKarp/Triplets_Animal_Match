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
        private GameplayData _gameplayData;
        private TopPanel _topPanel;
        private SignalBus _signalBus;

        private TileFactory _tileFactory;

        private AudioService _audioService;

        private List<Tile> _activeTiles = new List<Tile>();
        public List<Tile> ActiveTiles { get => _activeTiles; set => _activeTiles = value; }
        private Dictionary<string, List<Tile>> _topPanelsGroup = new Dictionary<string, List<Tile>>();

        [Inject]
        public void Construct(GamePresenter gamePresenter, GameplayData gameplayData, TopPanel topPanel, AudioService audioService, SignalBus signalBus, TileFactory tileFactory)
        {
            _gamePresenter = gamePresenter;
            _gameplayData = gameplayData;
            _topPanel = topPanel;
            _audioService = audioService;

            _signalBus = signalBus;

            _tileFactory = tileFactory;
        }

        private void Start()
        {

            _signalBus.Subscribe<TileOnFinishSignal>(CheckConditions);

            _tileFactory.Init(_activeTiles);
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
            _tileFactory.Release(tileOnFinishSignal.Tile);
            _gamePresenter.CheckOnWin();
            CheckFrozedTiles();
        }

        private void CheckFrozedTiles()
        {
            if (_gameplayData.MaxCountTiles - GetTilesCount() > _gameplayData.NumberTilesToUnfreeze && _activeTiles.OfType<TileFrozen>().Where(t => t.IsFreezed).Any())
                foreach (TileFrozen tile in _activeTiles.OfType<TileFrozen>().Where(t => t.IsFreezed).ToList())
                    tile.Unfreeze();
        }

        public List<Tile> CreateTiles()
        {
            var random = new System.Random();

            _activeTiles.Clear();            
            _activeTiles = _tileFactory.Create().OrderBy(_ => random.Next()).ToList();    // Перемешаем второй раз

            return _activeTiles;
        }

        public List<Tile> RefreshTiles()
        {
            _tileFactory.Init(_activeTiles);

            return CreateTiles();
        }
    }
}
