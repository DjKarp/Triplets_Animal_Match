using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace TripletsAnimalMatch
{
    public class GameView : MonoBehaviour
    {
        [SerializeField] private TopPanel _topPanel;
        [SerializeField] private Cloud _cloud;

        private SpawnPoint _spawnPoint;
        private GamePresenter _gamePresenter;
        private GameplayData _gameplayData;

        [Inject]
        public void Construct(GamePresenter gamePresenter, SpawnPoint spawnPoint, GameplayData gameplayData)
        {
            _gamePresenter = gamePresenter;
            _spawnPoint = spawnPoint;
            _gameplayData = gameplayData;
        }

        private void Awake()
        {
            
        }

        public void DropFishkiOnScene(List<Fishka> fishkas)
        {
            _cloud.Show(() => StartCoroutine(DropFishkiOnTime(fishkas)));
        }

        private IEnumerator DropFishkiOnTime(List<Fishka> fishkas)
        {
            foreach (Fishka fishka in fishkas)
            {
                fishka.Transform.position = _spawnPoint.Position;
                fishka.gameObject.SetActive(true);

                yield return new WaitForSeconds(_gameplayData.TimeSpawn);
            }

            _topPanel.Show();
        }
    }
}
