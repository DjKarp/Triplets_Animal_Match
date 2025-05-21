using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace TripletsAnimalMatch
{
    public class GameView : MonoBehaviour
    {
        [SerializeField] private TopPanel _topPanel;
        [SerializeField] private Cloud _cloud;
        [SerializeField] private ScreenWinner _screenWinner;
        [SerializeField] private ScreenLooser _screenLooser;
        [SerializeField] private ScreenMainMenu _screenMainMenu;
        private ReloadTilesButton _reloadButton;

        private SpawnPoint _spawnPoint;
        private GamePresenter _gamePresenter;
        private GameplayData _gameplayData;

        private SignalBus _signalBus;

        [Inject]
        public void Construct(GamePresenter gamePresenter, SpawnPoint spawnPoint, GameplayData gameplayData, SignalBus signalBus, ReloadTilesButton reloadTilesButton)
        {
            _gamePresenter = gamePresenter;
            _spawnPoint = spawnPoint;
            _gameplayData = gameplayData;
            _reloadButton = reloadTilesButton;

            _signalBus = signalBus;
        }

        private void Start()
        {
            _reloadButton.Hide();

            _screenWinner.gameObject.SetActive(true);
            _screenLooser.gameObject.SetActive(true);
            _screenMainMenu.gameObject.SetActive(true);

            _signalBus.Subscribe<TileOnTopPanelSignal>(AddedTileOnPanel);
            _signalBus.Subscribe<PlayGameSignals>(HideMainMenu);
        }

        public void GoTileOnPanel(Tile tile)
        {
            Vector3 position = _topPanel.GetNextTilePosition(tile);
            if (position != Vector3.zero)
                tile.MoveToTopPanel(position, _gameplayData.MoveTileTime);
        }

        public void GoTileToFinish(Tile tile)
        {
            tile.MoveToFinish(_spawnPoint.Position, _gameplayData.MoveTileTime / 2.0f);
            _topPanel.RemoveTileFromPanel(tile);
        }

        public void ShowScreenGameOver()
        {
            StopGame();
            _screenLooser.Show();
        }

        public void ShowScreenWinner()
        {
            StopGame();
            _screenWinner.Show();
        }

        public void HideMainMenu(PlayGameSignals playGameSignals)
        {
            _screenMainMenu.Hide();
        }

        public void DropTileOnScene(List<Tile> tiles, bool isStart = true)
        {
            _cloud.Show(() => StartCoroutine(DropTileWhitDelay(tiles, isStart)));
        }

        private IEnumerator DropTileWhitDelay(List<Tile> tiles, bool isStart = true)
        {
            foreach (Tile tile in tiles)
            {
                tile.Transform.position = _spawnPoint.Position;
                tile.gameObject.SetActive(true);

                yield return new WaitForSeconds(_gameplayData.TimeSpawn);
            }

            if (isStart) 
                _topPanel.Show();
            _reloadButton.Show();

            StartStopGameplay(true);
        }

        private void StartStopGameplay(bool isStart)
        {
            _signalBus.Fire(new IsGameplayActiveSignal(isStart));
        }

        public void ReloadTiles()
        {
            _reloadButton.Hide();
            StartStopGameplay(false);
            _gamePresenter.ReloadTiles();
        }

        public void EraseGameField(List<Tile> tiles)
        {
            for (int i = tiles.Count - 1; i >= 0; i--)
                tiles[i].DestroyFromGamefield();

            for (int i = _topPanel.TilesContainer.Length - 1; i >= 0; i--)
            {
                if (_topPanel.TilesContainer[i] != null)
                {
                    _topPanel.RemoveTileFromPanel(_topPanel.TilesContainer[i], true);
                    _topPanel.TilesContainer[i].DestroyFromGamefield();
                }
            }            
        }

        public void AddedTileOnPanel(TileOnTopPanelSignal tileOnTopPanelSignal)
        {
            _topPanel.AddedTileOnPanel(tileOnTopPanelSignal.Tile, tileOnTopPanelSignal.NumberPosition);
        }

        public bool IsTopPanelHaveFreePlace()
        {
            return _topPanel.IsHaveFreePlace;
        }

        private void StopGame()
        {
            _reloadButton.Hide();
            StartStopGameplay(false);
        }
    }
}
