using UnityEngine;
using Zenject;

namespace TripletsAnimalMatch
{
    public class UiInstaller : MonoInstaller
    {
        [SerializeField] private ReloadTilesButton _reloadTilesButton;    
        public override void InstallBindings()
        {
            BindGameplayButtonsInstallers();
        }

        private void BindGameplayButtonsInstallers()
        {
            Container
                .Bind<ReloadTilesButton>()
                .FromInstance(_reloadTilesButton)
                .AsSingle();
        }
    }
}