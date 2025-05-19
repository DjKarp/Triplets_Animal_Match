using UnityEngine;
using Zenject;

namespace TripletsAnimalMatch
{
    public class EntryPoint : MonoBehaviour
    {
        private GamePresenter _gamePresenter;
        private SignalBus _signalBus;
        
        [Inject]
        public void Construct(GamePresenter gamePresenter, SignalBus signalBus)
        {
            _gamePresenter = gamePresenter;
            _signalBus = signalBus;
        }

        private void Awake()
        {
            _signalBus.Subscribe<FinishShowLogoSignal>(StartGame);
        }

        private void StartGame(FinishShowLogoSignal finishShowLogoSignal)
        {
            _gamePresenter.Init();
        }

        private void OnDisable()
        {
            _signalBus.Unsubscribe<FinishShowLogoSignal>(StartGame);
        }
    }
}
