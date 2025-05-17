using UnityEngine;
using Zenject;

namespace TripletsAnimalMatch
{
    public class EnvironmentInstaller : MonoInstaller
    {
        [SerializeField] private SpawnPoint _spawnPoint;
        [SerializeField] private TopPanel _topPanel;

        public override void InstallBindings()
        {
            Container
                .Bind<SpawnPoint>()
                .FromInstance(_spawnPoint)
                .AsCached();

            Container
                .Bind<TopPanel>()
                .FromInstance(_topPanel)
                .AsSingle();
        }
    }
}