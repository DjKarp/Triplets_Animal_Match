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
            _gameView.DropFishkiOnScene(_gameModel.GetCreatePoolFishek());

            _signalBus.Subscribe<ClickOnFishkaSignal>(OnFishkaClick);
            _signalBus.Subscribe<FishkaOnTopPanelSignal>(TryMatchFishikiOnTopPanel);
        }

        public void ReloadFishki()
        {           
            StartCoroutine(WaitBeforeDrop(_gameModel.GetCountFishkiOnGameplace()));
        }

        private IEnumerator WaitBeforeDrop(int count)
        {
            while(!_gameModel.IsAllMoveFishkiOnTopPanel())
            {
                yield return new WaitForEndOfFrame();
            }

            _gameView.EraseGameField(_gameModel.FishkiList, () => _gameModel.FishkiList = new List<Fishka>());

            yield return new WaitForSeconds(1.0f);

            _gameView.DropFishkiOnScene(_gameModel.GetCreatePoolFishek(count), false);
        }

        private void OnFishkaClick(ClickOnFishkaSignal clickOnFishkaSignal)
        {
            _gameModel.RemoveFishkaFromGameplace(clickOnFishkaSignal.Fishka);
            _gameView.GoFishkuOnTopPanel(clickOnFishkaSignal.Fishka);
        }

        private void TryMatchFishikiOnTopPanel(FishkaOnTopPanelSignal fishkaOnTopPanel)
        {
            var triplesMatch = _gameModel.CheckMatch();

            if (triplesMatch != null)
            {
                foreach (Fishka fishka in triplesMatch)
                {
                    _gameView.GoFishkuToFinishPlace(fishka);
                    CheckOnWin();
                }
            }

            CheckGameOver();
        }

        private void CheckGameOver()
        {
            if (_gameModel.IsGameOver())
                _gameView.ShowScreenGameOver();
        }

        private void CheckOnWin()
        {
            if (_gameModel.IsWinner())
                _gameView.ShowScreenWinner();
        }
    }
}
