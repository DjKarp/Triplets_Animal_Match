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

        private List<Fishka> _fishkiStart = new List<Fishka>();
        private List<Fishka> _fishkiOnPanel = new List<Fishka>();
        private List<Fishka> _fishkiCompleate = new List<Fishka>();

        [Inject]
        public void Construct(GameView gameView, GameModel gameModel, SignalBus signalBus)
        {
            _gameView = gameView;
            _gameModel = gameModel;

            _signalBus = signalBus;
        }

        public void Init()
        {
            _fishkiStart = _gameModel.GetCreatePoolFishek();            
            _gameView.DropFishkiOnScene(_fishkiStart);

            _signalBus.Subscribe<ClickOnFishkaSignal>(OnFishkaClick);
        }

        private void OnFishkaClick(ClickOnFishkaSignal clickOnFishkaSignal)
        {
            _gameView.GoFishkuOnTopPanel(clickOnFishkaSignal.Fishka);
        }

        private void TryMatchFishikiOnTopPanel()
        {

        }

        private void CheckGameOver()
        {

        }

        private void CheckOnWin()
        {

        }
    }
}
