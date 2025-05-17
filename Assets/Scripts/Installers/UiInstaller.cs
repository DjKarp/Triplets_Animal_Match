using UnityEngine;
using Zenject;

namespace TripletsAnimalMatch
{
    public class UiInstaller : MonoInstaller
    {
        [SerializeField] private FishkiReloadButton _fishkiReloadButton;    
        public override void InstallBindings()
        {
            Container
                .Bind<FishkiReloadButton>()
                .FromInstance(_fishkiReloadButton)
                .AsSingle();
        }
    }
}