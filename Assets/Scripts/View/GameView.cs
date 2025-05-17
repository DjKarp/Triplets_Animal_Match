using System;
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
        private FishkiReloadButton _reloadButton;

        private SpawnPoint _spawnPoint;
        private GamePresenter _gamePresenter;
        private GameplayData _gameplayData;

        private SignalBus _signalBus;

        [Inject]
        public void Construct(GamePresenter gamePresenter, SpawnPoint spawnPoint, GameplayData gameplayData, SignalBus signalBus, FishkiReloadButton fishkiReloadButton)
        {
            _gamePresenter = gamePresenter;
            _spawnPoint = spawnPoint;
            _gameplayData = gameplayData;
            _reloadButton = fishkiReloadButton;

            _signalBus = signalBus;
        }

        private void Start()
        {
            _reloadButton.Hide();

            _signalBus.Subscribe<FishkaOnTopPanelSignal>(AddedFishkuOnTopPanel);
        }

        public void GoFishkuOnTopPanel(Fishka fishka)
        {
            Vector3 position = _topPanel.GetNextFishkaPosition(fishka);
            if (position != Vector3.zero)
                fishka.MoveToTopPanel(position, _gameplayData.TimeMoveFishkaToTopPanel);
            else
                Debug.LogError("Chech position on TopPanel! => " + fishka.gameObject.GetInstanceID());
        }

        public void GoFishkuToFinishPlace(Fishka fishka)
        {
            fishka.MoveToFinishPlace(_spawnPoint.Position, _gameplayData.TimeMoveFishkaToTopPanel / 2.0f);
            _topPanel.RemoveFishkaFromTopPanel(fishka);
        }

        public void ShowScreenGameOver()
        {
            StartStopGameplay(false);
            Debug.LogError("Game Over!");
        }

        public void ShowScreenWinner()
        {
            StartStopGameplay(false);
            Debug.LogError("You Win!");
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
            _reloadButton.Show();

            StartStopGameplay(true);
        }

        private void StartStopGameplay(bool isStart)
        {
            _signalBus.Fire(new StartStopGameplaySignal(isStart));
        }

        public void ReloadFishkiOnScene()
        {
            _reloadButton.Hide();
            StartStopGameplay(false);
            _gamePresenter.ReloadFishki();
        }

        public void EraseGameField(List<Fishka> fishkas, Action onCompleate)
        {
            for (int i = fishkas.Count - 1; i > 0; i--)
            {
                fishkas[i].DestroyFromGamefield();
            }
            onCompleate?.Invoke();
        }

        public void AddedFishkuOnTopPanel(FishkaOnTopPanelSignal fishkaOnTopPanel)
        {
            _topPanel.AddedFishkuOnTopPanel(fishkaOnTopPanel.Fishka, fishkaOnTopPanel.NumberPosition);
        }

        private void OnDisable()
        {

        }
    }
}
