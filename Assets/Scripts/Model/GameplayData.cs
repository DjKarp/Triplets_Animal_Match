using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TripletsAnimalMatch
{
    public class GameplayData : MonoBehaviour
    {
        [SerializeField] GameplayDataSO _gameplayDataSO;

        public int FishkiMaxCountOnScene { get => _gameplayDataSO.FishkiMaxCountOnScene; }
        public int FishkiCountOnMatch { get => _gameplayDataSO.FishkiCountOnMatch; }
        public float TimeSpawn { get => _gameplayDataSO.TimeSpawn; }

        public float TimeMoveFishkaToTopPanel { get => _gameplayDataSO.TimeMoveFishkaToTopPanel; }
    }
}
