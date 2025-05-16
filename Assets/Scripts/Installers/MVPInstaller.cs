using UnityEngine;
using Zenject;

namespace TripletsAnimalMatch
{
    public class MVPInstaller : MonoInstaller
    {
        [SerializeField] private GamePresenter _gamePresenter;
        [SerializeField] private GameView _gameView;
        [SerializeField] private GameModel _gameModel;

        public override void InstallBindings()
        {
            Container
                .Bind<GamePresenter>()
                .FromInstance(_gamePresenter)
                .AsSingle();

            Container
                .Bind<GameView>()
                .FromInstance(_gameView)
                .AsSingle();

            Container
                .Bind<GameModel>()
                .FromInstance(_gameModel)
                .AsSingle();
        }
    }
}