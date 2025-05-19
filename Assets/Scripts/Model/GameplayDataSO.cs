using System.Collections.Generic;
using UnityEngine;

namespace TripletsAnimalMatch
{
    [CreateAssetMenu(fileName = "GameplayData", menuName = "CreateGameplayData", order = 1)]
    public class GameplayDataSO : ScriptableObject
    {
        [Tooltip("������������ ���������� ������, ������� �������� �� ����� ������ (������) � ������ (������� ����). � ��������� - 63.")]
        [SerializeField] private int _maxCountTiles = 60;
        public int MaxCountTiles { get => _maxCountTiles; }


        [Tooltip("���������� ������ � ����� ����, ������� ��������� ��� �� ������������. �� ������� - ��� ���������� ���������� ������. �� ������� - 3.")]
        [SerializeField] private int _matchCountTiles = 3;
        public int MatchCountTiles { get => _matchCountTiles; }


        [Tooltip("�������� � ������� ���������� ����� �� ������.")]
        [SerializeField] private float _timeSpawn = 0.5f;
        public float TimeSpawn { get => _timeSpawn; }


        [Tooltip("����� ��� �������� ����� ����� �� �����, ��� �� ���� ��������, �� ����� ����. � ��� �� ����� �� �������� ����� �� ���� � ������.")]
        [SerializeField] private float _moveTileTime = 1.0f;
        public float MoveTileTime { get => _moveTileTime; }


        [Tooltip("���������� ������, ������� ����� ��������� � ������, ����� ������������� Freezed �����.")]
        [SerializeField] private int _numberTilesToUnfreeze = 5;
        public float NumberTilesToUnfreeze { get => _numberTilesToUnfreeze; }


        [Tooltip("������ �� �������� ������� ������ ������ - TileEffect � ���������� �� �� ������. �.�. ������� � ����� ����������� ������ ����� �����������. ")]
        public List<TileEffectCount> TileEffectCount;
    }
}
