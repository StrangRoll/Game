using UnityEngine;
using Zenject;

public class PauseManagerInstaller : MonoInstaller
{
    [SerializeField] private PauseManager _manager;

    public override void InstallBindings()
    {
        Container
            .Bind<PauseManager>()
            .FromInstance(_manager)
            .AsSingle();
    }
}