using Agava.WebUtility;
using UnityEngine;
using Zenject;

public class OnGameCollapsed : MonoBehaviour
{
    [Inject] private PauseManager _pause;

    private void OnEnable()
    {
        WebApplication.InBackgroundChangeEvent += OnInBackgroundChange;
    }

    private void OnDisable()
    {
        WebApplication.InBackgroundChangeEvent -= OnInBackgroundChange;
    }

    private void OnInBackgroundChange(bool inBackground)
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        AudioListener.pause = inBackground;
        AudioListener.volume = inBackground ? 0f : 1f;
#endif
    }
}
