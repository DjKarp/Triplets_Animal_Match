using UnityEngine;
using Zenject;

namespace TripletsAnimalMatch
{
    public class EnvironmentInstaller : MonoInstaller
    {
        [SerializeField] private SpawnPoint _spawnPoint;

        public override void InstallBindings()
        {
            Container
                .Bind<SpawnPoint>()
                .FromInstance(_spawnPoint)
                .AsCached();
        }
    }
}