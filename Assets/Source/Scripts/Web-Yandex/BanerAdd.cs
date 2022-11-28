using Agava.YandexGames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanerAdd : MonoBehaviour
{
    private void Awake()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        StartCoroutine(ShowBannerAtStart());
#endif
    }

    private IEnumerator ShowBannerAtStart()
    {
        yield return YandexGamesSdk.Initialize();
        InterstitialAd.Show();
    }
}
