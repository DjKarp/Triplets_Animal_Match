using UnityEngine;
using Zenject;

namespace TripletsAnimalMatch
{
    public class MVPInstaller : MonoInstaller
    {
        [SerializeField] private GamePresenter _gamePresenter;
        [SerializeField] private GameView _gameView;
        [SerializeField] private GameModel _gameModel;

        [SerializeField] private TileFactory _tileFactory;

        public override void InstallBindings()
        {
            BindGamePresenter();
            BindGameView();
            BindGameModel();

            BindTileFactory();
        }

        private void BindGamePresenter()
        {
            Container
                .Bind<GamePresenter>()
                .FromInstance(_gamePresenter)
                .AsSingle();
        }

        private void BindGameView()
        {
            Container
                .Bind<GameView>()
                .FromInstance(_gameView)
                .AsSingle();
        }

        private void BindGameModel()
        {
            Container
                .Bind<GameModel>()
                .FromInstance(_gameModel)
                .AsSingle();
        }

        private void BindTileFactory()
        {
            Container
                .Bind<TileFactory>()
                .FromInstance(_tileFactory)
                .AsSingle();
        }
    }
}