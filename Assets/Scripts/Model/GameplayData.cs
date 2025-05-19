using System.Collections.Generic;
using UnityEngine;

namespace TripletsAnimalMatch
{
    public class GameplayData : MonoBehaviour
    {
        [SerializeField] private GameplayDataSO _gameplayDataSO;

        public int MaxCountTiles { get => _gameplayDataSO.MaxCountTiles; }
        public int MatchCountTiles { get => _gameplayDataSO.MatchCountTiles; }
        public float TimeSpawn { get => _gameplayDataSO.TimeSpawn; }
        public float MoveTileTime { get => _gameplayDataSO.MoveTileTime; }
        public float NumberTilesToUnfreeze { get => _gameplayDataSO.NumberTilesToUnfreeze; }
        public List<TileEffectCount> TileEffectCount { get => _gameplayDataSO.TileEffectCount; }
    }
}
