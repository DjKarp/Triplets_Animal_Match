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
                .DeclareSignal<ClickOnTileSignal>()
                .OptionalSubscriber();

            Container
                .DeclareSignal<IsGameplayActiveSignal>()
                .OptionalSubscriber();

            Container
                .DeclareSignal<TileOnTopPanelSignal>()
                .OptionalSubscriber();

            Container
                .DeclareSignal<TileOnFinishSignal>()
                .OptionalSubscriber();

            Container
                .DeclareSignal<PlayGameSignals>()
                .OptionalSubscriber();
        }
    }
}