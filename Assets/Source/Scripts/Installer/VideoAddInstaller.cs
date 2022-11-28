using UnityEngine;
using Zenject;

public class VideoAddInstaller : MonoInstaller
{
    [SerializeField] private VideoAdd _videoAdd;

    public override void InstallBindings()
    {
        Container
            .Bind<VideoAdd>()
            .FromInstance(_videoAdd)
            .AsSingle();
    }
}