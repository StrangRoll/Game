using Agava.YandexGames;
using UnityEngine;
using UnityEngine.Events;

public class VideoAdd : MonoBehaviour
{
    [SerializeField] private GameOverUIActivator _gameOverUI;

    public event UnityAction VideoRewardCollected;

    private void OnEnable()
    {
        _gameOverUI.ContinueButtonPressed += OnContinueButtonPressed;
    }

    private void OnDisable()
    {
        _gameOverUI.ContinueButtonPressed -= OnContinueButtonPressed;
    }

    private void OnContinueButtonPressed()
    {
#if UNITY_EDITOR
        OnRewarded();
#endif

#if UNITY_WEBGL && !UNITY_EDITOR
        VideoAd.Show(() => OnVideoOpened(), () => OnRewarded());
#endif
    }

    private void OnVideoOpened(){ }

    private void OnRewarded()
    {
        VideoRewardCollected?.Invoke();
    }
}
