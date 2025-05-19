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

        [Inject]
        public void Construct(GameView gameView, GameModel gameModel, SignalBus signalBus)
        {
            _gameView = gameView;
            _gameModel = gameModel;

            _signalBus = signalBus;
        }

        public void Init()
        {         
            _gameView.DropTileOnScene(_gameModel.CreateTiles());

            _signalBus.Subscribe<ClickOnTileSignal>(OnTileClick);
            _signalBus.Subscribe<TileOnTopPanelSignal>(TryMatchTilesOnPanel);
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

            _gameView.DropTileOnScene(_gameModel.CreateTiles(count), false);
        }

        private void OnTileClick(ClickOnTileSignal clickOnTileSignal)
        {
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
                _gameView.ShowScreenGameOver();
            }
        }

        public void CheckOnWin()
        {
            if (_gameModel.IsWinner())
            {
                _gameView.ShowScreenWinner();
            }
        }
    }
}
