using UnityEngine;
using Zenject;

namespace TripletsAnimalMatch
{
    public class SignalsInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);

            Container
                .DeclareSignal<ClickOnFishkaSignal>()
                .OptionalSubscriber();

            Container
                .DeclareSignal<StartStopGameplaySignal>()
                .OptionalSubscriber();

            Container
                .DeclareSignal<FishkaOnTopPanelSignal>()
                .OptionalSubscriber();
        }
    }
}