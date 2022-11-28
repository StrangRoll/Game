using UnityEngine;
using Zenject;

public class GameCenterInstaller : MonoInstaller
{
    [SerializeField] private GameCenter _center;

    public override void InstallBindings()
    {
        Container
            .Bind<GameCenter>()
            .FromInstance(_center)
            .AsSingle();
    }
}