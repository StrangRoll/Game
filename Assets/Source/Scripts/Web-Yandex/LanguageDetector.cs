using Agava.YandexGames;
using System.Collections;
using UnityEngine;

public class LanguageDetector : MonoBehaviour
{
    private string _russian = "Russian";
    private string _english = "English";
    private string _turkish = "Turkish";

    private void Awake()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        StartCoroutine(ChangeLanguage());
#endif
    }

    private IEnumerator ChangeLanguage()
    {
        yield return YandexGamesSdk.Initialize();

        var language = YandexGamesSdk.Environment.i18n.lang;

        switch (language)
        {
            case "ru":
                Lean.Localization.LeanLocalization.SetCurrentLanguageAll(_russian);
                break;
            case "be":
                Lean.Localization.LeanLocalization.SetCurrentLanguageAll(_russian);
                break;
            case "kk":
                Lean.Localization.LeanLocalization.SetCurrentLanguageAll(_russian);
                break;
            case "uz":
                Lean.Localization.LeanLocalization.SetCurrentLanguageAll(_russian);
                break;
            case "uk":
                Lean.Localization.LeanLocalization.SetCurrentLanguageAll(_russian);
                break;
            case "tr":
                Lean.Localization.LeanLocalization.SetCurrentLanguageAll(_turkish);
                break;
            case "en":
                Lean.Localization.LeanLocalization.SetCurrentLanguageAll(_english);
                break;
            default:
                Lean.Localization.LeanLocalization.SetCurrentLanguageAll(_english);
                break;
        }
    }
}
