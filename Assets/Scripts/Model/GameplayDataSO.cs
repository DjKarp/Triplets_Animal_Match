using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TripletsAnimalMatch
{
    [CreateAssetMenu(fileName = "GameplayData", menuName = "CreateScriptableObject", order = 1)]
    public class GameplayDataSO : ScriptableObject
    {
        private int _fishkiMaxCountOnScene = 60;
        public int FishkiMaxCountOnScene { get => _fishkiMaxCountOnScene; }

        private int _fishkiCountOnMatch = 3;
        public int FishkiCountOnMatch { get => _fishkiCountOnMatch; }
    }
}
