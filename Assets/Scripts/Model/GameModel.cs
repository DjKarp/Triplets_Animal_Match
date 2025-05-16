using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace TripletsAnimalMatch
{
    public class GameModel : MonoBehaviour
    {
        private GamePresenter _gamePresenter;
        private FishkiData _fishkiData;
        private GameplayData _gameplayData;


        [Inject]
        public void Construct(GamePresenter gamePresenter, FishkiData fishkiData, GameplayData gameplayData)
        {
            _gamePresenter = gamePresenter;
            _fishkiData = fishkiData;
            _gameplayData = gameplayData;
        }

        public List<Fishka> CreatePoolFishek()
        {
            int tempUniqueFishkiCount = _gameplayData.FishkiMaxCountOnScene / _gameplayData.FishkiCountOnMatch;

            var random = new System.Random();

            for (int i = 0; i < tempUniqueFishkiCount)
        }
    }
}
