using Agava.YandexGames;
using UnityEngine;
using UnityEngine.Events;

public class VideoAdd : MonoBehaviour
{
    [SerializeField] private GameOverUIActivator _gameOverUI;

    public event UnityAction VideoRewardCollected;

    private bool _isRewarded = false;

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
        VideoRewardCollected?.Invoke();
#endif

#if UNITY_WEBGL && !UNITY_EDITOR
        VideoAd.Show(() => OnVideoOpened(), () => OnRewarded(), () => OnClose());
#endif
    }

    private void OnVideoOpened(){}

    private void OnRewarded()
    {
        _isRewarded = true;
    }

    private void OnClose()
    {
        if (_isRewarded)
            VideoRewardCollected?.Invoke();

        _isRewarded = false;
    }
}
