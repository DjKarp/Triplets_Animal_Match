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

        private SignalBus _signalBus;

        [Inject]
        public void Construct(GamePresenter gamePresenter, SpawnPoint spawnPoint, GameplayData gameplayData, SignalBus signalBus)
        {
            _gamePresenter = gamePresenter;
            _spawnPoint = spawnPoint;
            _gameplayData = gameplayData;

            _signalBus = signalBus;
        }

        private void Awake()
        {
            
        }

        public void GoFishkuOnTopPanel(Fishka fishka)
        {
            fishka.MoveToTopPanel(_topPanel.GetNextFishkaPosition(fishka), _gameplayData.TimeMoveFishkaToTopPanel);
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

            _signalBus.Fire(new StartStopGameplay(true));
        }
    }
}
