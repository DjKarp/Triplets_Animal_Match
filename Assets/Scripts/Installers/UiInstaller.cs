using UnityEngine;
using Zenject;

namespace TripletsAnimalMatch
{
    public class UiInstaller : MonoInstaller
    {
        [SerializeField] private ReloadTilesButton _reloadTilesButton;    
        public override void InstallBindings()
        {
            Container
                .Bind<ReloadTilesButton>()
                .FromInstance(_reloadTilesButton)
                .AsSingle();
        }
    }
}