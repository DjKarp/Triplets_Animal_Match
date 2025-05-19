using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace TripletsAnimalMatch
{
    public class GamePresenter : MonoBehaviour
    {
        private SignalBus _signalBus;

        private GameView _gameView;
        private GameModel _gameModel;
        private AudioService _audioService;

        [Inject]
        public void Construct(GameView gameView, GameModel gameModel, SignalBus signalBus, AudioService audioService)
        {
            _gameView = gameView;
            _gameModel = gameModel;
            _audioService = audioService;

            _signalBus = signalBus;
        }

        public void Init()
        {
            _audioService.StartFalling();
            _gameView.DropTileOnScene(_gameModel.CreateTiles());

            _signalBus.Subscribe<ClickOnTileSignal>(OnTileClick);
            _signalBus.Subscribe<TileOnTopPanelSignal>(TryMatchTilesOnPanel);
            _signalBus.Subscribe<IsGameplayActiveSignal>(StartGameplay);
        }

        public void ReloadTiles()
        {           
            StartCoroutine(WaitBeforeDrop(_gameModel.GetTilesCount()));
        }

        private IEnumerator WaitBeforeDrop(int count)
        {
            while(!_gameModel.IsAllClickedTileMoveOnPanel())
            {
                yield return new WaitForEndOfFrame();
            }

            _gameView.EraseGameField(_gameModel.ActiveTiles, () => _gameModel.ActiveTiles = new List<Tile>());

            yield return new WaitForSeconds(1.0f);

            _audioService.StartFalling();
            _gameView.DropTileOnScene(_gameModel.CreateTiles(count), false);
        }

        private void OnTileClick(ClickOnTileSignal clickOnTileSignal)
        {
            _audioService.PlayGameplayAudio(AudioService.AudioGameplay.ClickOnTile);

            if (_gameView.IsTopPanelHaveFreePlace())
            {
                clickOnTileSignal.Tile.SwitchOffRigidbodyAndCollider();
                _gameView.GoTileOnPanel(clickOnTileSignal.Tile);
                _gameModel.RemoveTileFromListActiveTiles(clickOnTileSignal.Tile);                
            }            
        }

        private void TryMatchTilesOnPanel(TileOnTopPanelSignal tileOnTopPanelSignal)
        {
            var triplesMatch = _gameModel.CheckMatch();

            if (triplesMatch != null)
            {
                foreach (Tile tile in triplesMatch)
                {
                    _gameView.GoTileToFinish(tile);
                }

                _audioService.PlayGameplayAudio(AudioService.AudioGameplay.Match);
            }
            else
            {
                CheckGameOver();
            }
        }

        private void CheckGameOver()
        {
            if (_gameModel.IsWinner() == false && _gameModel.IsGameOver())
            {
                _audioService.PlayUIAudio(AudioService.AudioUI.LooseGame);

                _gameView.ShowScreenGameOver();
            }
        }

        public void CheckOnWin()
        {
            if (_gameModel.IsWinner())
            {
                _audioService.PlayUIAudio(AudioService.AudioUI.Winner);

                _gameView.ShowScreenWinner();
            }
        }
        private void StartGameplay(IsGameplayActiveSignal startStopGameplay)
        {
            _audioService.StopFalling();
        }

    }
}
