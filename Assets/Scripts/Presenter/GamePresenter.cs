using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace TripletsAnimalMatch
{
    public class GamePresenter : MonoBehaviour
    {
        private GameView _gameView;
        private GameModel _gameModel;

        private List<Fishka> _fishkiStart = new List<Fishka>();
        private List<Fishka> _fishkiOnPanel = new List<Fishka>();
        private List<Fishka> _fishkiCompleate = new List<Fishka>();

        [Inject]
        public void Construct(GameView gameView, GameModel gameModel)
        {
            _gameView = gameView;
            _gameModel = gameModel;
        }

        public void Init()
        {
            _fishkiStart = _gameModel.GetCreatePoolFishek();            
            _gameView.DropFishkiOnScene(_fishkiStart);
        }

        private void OnFishkaClick(Fishka fishka)
        {

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
