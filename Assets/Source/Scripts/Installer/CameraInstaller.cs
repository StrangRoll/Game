using UnityEngine;
using Zenject;

public class CameraInstaller : MonoInstaller
{
    [SerializeField] private WithCameraMovier _cameraMovier;
    [SerializeField] private Camera _camera;

    public override void InstallBindings()
    {
        Container
            .Bind<WithCameraMovier>()
            .FromInstance(_cameraMovier)
            .AsSingle();

        Container
            .Bind<Camera>()
            .FromInstance(_camera)
            .AsSingle();
    }
}