using UnityEngine;
using Zenject;

public class CameraInstaller : MonoInstaller
{
    [SerializeField] private CameraMovier _movier;
    [SerializeField] private Camera _camera;

    public override void InstallBindings()
    {
        Container
            .Bind<CameraMovier>()
            .FromInstance(_movier)
            .AsSingle();

        Container
            .Bind<Camera>()
            .FromInstance(_camera)
            .AsSingle();
    }
}