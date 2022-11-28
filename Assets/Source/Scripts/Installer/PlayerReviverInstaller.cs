using UnityEngine;
using Zenject;

public class PlayerReviverInstaller : MonoInstaller
{
    [SerializeField] private PlayerReviver _reviver;

    public override void InstallBindings()
    {
        Container
            .Bind<PlayerReviver>()
            .FromInstance(_reviver)
            .AsSingle();
    }
}