using UnityEngine;

namespace TripletsAnimalMatch
{
    public class GameplayData : MonoBehaviour
    {
        [SerializeField] GameplayDataSO _gameplayDataSO;

        public int MaxCountTiles { get => _gameplayDataSO.MaxCountTiles; }
        public int MatchCountTiles { get => _gameplayDataSO.MatchCountTiles; }
        public float TimeSpawn { get => _gameplayDataSO.TimeSpawn; }

        public float MoveTileTime { get => _gameplayDataSO.MoveTileTime; }
    }
}
