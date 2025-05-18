using UnityEngine;
using Zenject;

namespace TripletsAnimalMatch
{
    public class DataInstaller : MonoInstaller
    {
        [SerializeField] private TileData _tileData;
        [SerializeField] private GameplayData _gameplayData;

        public override void InstallBindings()
        {
            Container
                .Bind<GameplayData>()
                .FromInstance(_gameplayData)
                .AsCached();

            Container
                .Bind<TileData>()
                .FromInstance(_tileData)
                .AsCached();
        }
    }
}