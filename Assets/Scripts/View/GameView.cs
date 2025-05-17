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
        [SerializeField] private ScreenWinner _screenWinner;
        [SerializeField] private ScreenLooser _screenLooser;
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

            _screenWinner.gameObject.SetActive(true);
            _screenLooser.gameObject.SetActive(true);

            _signalBus.Subscribe<FishkaOnTopPanelSignal>(AddedFishkuOnTopPanel);
        }

        public void GoFishkuOnTopPanel(Fishka fishka)
        {
            Vector3 position = _topPanel.GetNextFishkaPosition(fishka);
            if (position != Vector3.zero)
                fishka.MoveToTopPanel(position, _gameplayData.TimeMoveFishkaToTopPanel);
        }

        public void GoFishkuToFinishPlace(Fishka fishka)
        {
            fishka.MoveToFinishPlace(_spawnPoint.Position, _gameplayData.TimeMoveFishkaToTopPanel / 2.0f);
            _topPanel.RemoveFishkaFromTopPanel(fishka);
        }

        public void ShowScreenGameOver()
        {
            StopGame();
            _screenLooser.Show();
        }

        public void ShowScreenWinner()
        {
            StopGame();
            _screenWinner.Show();
        }

        public void DropFishkiOnScene(List<Fishka> fishkas, bool isStart = true)
        {
            _cloud.Show(() => StartCoroutine(DropFishkiOnTime(fishkas, isStart)));
        }

        private IEnumerator DropFishkiOnTime(List<Fishka> fishkas, bool isStart = true)
        {
            foreach (Fishka fishka in fishkas)
            {
                fishka.Transform.position = _spawnPoint.Position;
                fishka.gameObject.SetActive(true);

                yield return new WaitForSeconds(_gameplayData.TimeSpawn);
            }

            if (isStart) 
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

        public void EraseGameField(List<Fishka> fishkas, Action onComplete)
        {
            for (int i = fishkas.Count - 1; i >= 0; i--)
                fishkas[i].DestroyFromGamefield();

            for (int i = _topPanel.FishkasPlace.Length - 1; i >= 0; i--)
            {
                if (_topPanel.FishkasPlace[i] != null)
                {
                    _topPanel.RemoveFishkaFromTopPanel(_topPanel.FishkasPlace[i], true);
                    _topPanel.FishkasPlace[i].DestroyFromGamefield();
                }
            }

            onComplete?.Invoke();
        }

        public void AddedFishkuOnTopPanel(FishkaOnTopPanelSignal fishkaOnTopPanel)
        {
            _topPanel.AddedFishkuOnTopPanel(fishkaOnTopPanel.Fishka, fishkaOnTopPanel.NumberPosition);
        }

        private void StopGame()
        {
            _reloadButton.Hide();
            StartStopGameplay(false);
        }

        private void OnDisable()
        {

        }
    }
}
