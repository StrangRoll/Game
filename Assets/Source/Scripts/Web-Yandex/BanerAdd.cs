using Agava.YandexGames;
using System.Collections;
using UnityEngine;
using Zenject;

public class BanerAdd : MonoBehaviour
{
    [Inject] private GameCenter _center;

    private int _gamesPerBanner = 5;
    private int _gameEndedCount = 0;

    private void Awake()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        StartCoroutine(ShowBannerAtStart());
#endif
    }

    private void OnEnable()
    {
        _center.GameEnded += OnGameEnded;
    }

    private void OnDisable()
    {
        _center.GameEnded -= OnGameEnded;
    }

    private IEnumerator ShowBannerAtStart()
    {
        yield return YandexGamesSdk.Initialize();
        InterstitialAd.Show();
    }

    private void OnGameEnded()
    {
        _gameEndedCount++;

        if (_gameEndedCount >= 5)
        {
            _gameEndedCount = 0;

#if UNITY_WEBGL && !UNITY_EDITOR
            InterstitialAd.Show();
#endif
        }
    }
}
