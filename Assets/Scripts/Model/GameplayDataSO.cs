using System.Collections.Generic;
using UnityEngine;

namespace TripletsAnimalMatch
{
    [CreateAssetMenu(fileName = "GameplayData", menuName = "CreateGameplayData", order = 1)]
    public class GameplayDataSO : ScriptableObject
    {
        [Tooltip("Максимальное количество тайлов, которые сыпяться из точки спавна (облако) в стакан (игровое поле). В референсе - 63.")]
        [SerializeField] private int _maxCountTiles = 60;
        public int MaxCountTiles { get => _maxCountTiles; }


        [Tooltip("Количество тайлов в экшен баре, которое требуется для их исчезновения. По другому - это количество одинаковых тайлов. По заданию - 3.")]
        [SerializeField] private int _matchCountTiles = 3;
        public int MatchCountTiles { get => _matchCountTiles; }


        [Tooltip("Интервал с которым спавняться тайлы из облака.")]
        [SerializeField] private float _timeSpawn = 0.5f;
        public float TimeSpawn { get => _timeSpawn; }


        [Tooltip("Время для анимации полёта тайла из места, где по нему щёлкнули, до экшен бара. И это же время на анимацию полёта из бара в облако.")]
        [SerializeField] private float _moveTileTime = 1.0f;
        public float MoveTileTime { get => _moveTileTime; }


        [Tooltip("Количество тайлов, которое нужно отправить в облако, чтобы разморозились Freezed тайлы.")]
        [SerializeField] private int _numberTilesToUnfreeze = 5;
        public float NumberTilesToUnfreeze { get => _numberTilesToUnfreeze; }


        [Tooltip("Список из названия эффекта особых тайлов - TileEffect и количества их на уровне. Т.е. сколько и каких специальных тайлов будет создаваться. ")]
        public List<TileEffectCount> TileEffectCount;
    }
}
