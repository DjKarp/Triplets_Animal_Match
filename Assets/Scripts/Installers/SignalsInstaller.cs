using UnityEngine;
using Zenject;

namespace TripletsAnimalMatch
{
    public class SignalsInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);

            BinTileSignals();
            BindGameplaySignals();
            BindMenuAndLogoSignals();
        }               

        private void BinTileSignals()
        {
            Container
                .DeclareSignal<ClickOnTileSignal>()
                .OptionalSubscriber();

            Container
                .DeclareSignal<TileOnTopPanelSignal>()
                .OptionalSubscriber();

            Container
                .DeclareSignal<TileOnFinishSignal>()
                .OptionalSubscriber();
        }

        private void BindGameplaySignals()
        {
            Container
                .DeclareSignal<IsGameplayActiveSignal>()
                .OptionalSubscriber();

            Container
                .DeclareSignal<PlayGameSignals>()
                .OptionalSubscriber();
        }

        private void BindMenuAndLogoSignals()
        {
            Container
                .DeclareSignal<FinishShowLogoSignal>()
                .OptionalSubscriber();
        }
    }
}