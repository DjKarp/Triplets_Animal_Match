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
            BindGameplayData();
            BindTileData();
        }

        private void BindGameplayData()
        {
            Container
                .Bind<GameplayData>()
                .FromInstance(_gameplayData)
                .AsCached();
        }

        private void BindTileData()
        {
            Container
                .Bind<TileData>()
                .FromInstance(_tileData)
                .AsCached();
        }
    }
}