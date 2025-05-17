using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TripletsAnimalMatch
{
    [CreateAssetMenu(fileName = "GameplayData", menuName = "CreateScriptableObject", order = 1)]
    public class GameplayDataSO : ScriptableObject
    {
        [SerializeField] private int _fishkiMaxCountOnScene = 60;
        public int FishkiMaxCountOnScene { get => _fishkiMaxCountOnScene; }

        [SerializeField] private int _fishkiCountOnMatch = 3;
        public int FishkiCountOnMatch { get => _fishkiCountOnMatch; }

        [SerializeField] private float _timeSpawn = 0.5f;

        public float TimeSpawn { get => _timeSpawn; }

        [SerializeField] private float _timeMoveFishkaToTopPanel = 1.0f;

        public float TimeMoveFishkaToTopPanel { get => _timeMoveFishkaToTopPanel; }
    }
}
