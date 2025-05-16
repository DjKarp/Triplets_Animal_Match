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

        private float _timeSpawn = 0.5f;
        private List<Fishka> _fishki = new List<Fishka>();

        [Inject]
        public void Construct(GameView gameView, GameModel gameModel)
        {
            _gameView = gameView;
            _gameModel = gameModel;
        }

        public void Init(Transform spawnPoint)
        {
            _fishki = _gameModel
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
