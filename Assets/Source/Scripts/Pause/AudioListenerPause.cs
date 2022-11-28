using UnityEngine;
using Zenject;

public class AudioListenerPause : MonoBehaviour, IPauseHandler
{
    [Inject] private PauseManager _pauseManager;

    private void OnEnable()
    {
        _pauseManager.Register(this);
    }

    private void OnDisable()
    {
        _pauseManager.UnRegister(this);
    }

    public void Pause(bool isPause)
    {
        AudioListener.pause = isPause;
        AudioListener.volume = isPause ? 0f : 1f;
    }
}
