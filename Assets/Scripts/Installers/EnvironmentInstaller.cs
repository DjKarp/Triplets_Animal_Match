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
            BindSpawnPoint();
            BindTopPanel();
        }        

        private void BindSpawnPoint()
        {
            Container
                .Bind<SpawnPoint>()
                .FromInstance(_spawnPoint)
                .AsCached();
        }
        private void BindTopPanel()
        {
            Container
                .Bind<TopPanel>()
                .FromInstance(_topPanel)
                .AsSingle();
        }

    }
}