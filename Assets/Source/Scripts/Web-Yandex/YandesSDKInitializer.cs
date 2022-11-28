using Agava.YandexGames;
using UnityEngine;

public class YandesSDKInitializer : MonoBehaviour
{
    private void Awake()
    {
        YandexGamesSdk.Initialize();
    }
}
