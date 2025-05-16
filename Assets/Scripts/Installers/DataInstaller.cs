using UnityEngine;
using Zenject;

namespace TripletsAnimalMatch
{
    public class DataInstaller : MonoInstaller
    {
        [SerializeField] private FishkiData _fishkiData;
        [SerializeField] private GameplayData _gameplayData;

        public override void InstallBindings()
        {
            Container
                .Bind<GameplayData>()
                .FromInstance(_gameplayData)
                .AsCached();

            Container
                .Bind<FishkiData>()
                .FromInstance(_fishkiData)
                .AsCached();
        }
    }
}