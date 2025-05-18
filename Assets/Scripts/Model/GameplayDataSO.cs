using UnityEngine;

namespace TripletsAnimalMatch
{
    [CreateAssetMenu(fileName = "GameplayData", menuName = "CreateGameplayData", order = 1)]
    public class GameplayDataSO : ScriptableObject
    {
        [SerializeField] private int _maxCountTiles = 60;
        public int MaxCountTiles { get => _maxCountTiles; }


        [SerializeField] private int _matchCountTiles = 3;
        public int MatchCountTiles { get => _matchCountTiles; }


        [SerializeField] private float _timeSpawn = 0.5f;
        public float TimeSpawn { get => _timeSpawn; }


        [SerializeField] private float _moveTileTime = 1.0f;
        public float MoveTileTime { get => _moveTileTime; }
    }
}
