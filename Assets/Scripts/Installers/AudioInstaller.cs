using UnityEngine;
using Zenject;

public class AudioInstaller : MonoInstaller
{
    [SerializeField] private TripletsAnimalMatch.AudioService _audioService;

    public override void InstallBindings()
    {
        Container
            .Bind<TripletsAnimalMatch.AudioService>()
            .FromInstance(_audioService)
            .AsSingle();
    }
}