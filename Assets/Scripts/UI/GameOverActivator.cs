using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverActivator : MonoBehaviour
{
    [SerializeField] private GameCenter _gameCenter;
    [SerializeField] private TMP_Text _gameOverText;
    [SerializeField] private Button _restartButton;

    private void OnEnd()
    {
        _gameOverText.gameObject.SetActive(true);
        _restartButton.gameObject.SetActive(true);
    }

    private void OnRestart()
    {
        _gameOverText.gameObject.SetActive(false);
        _restartButton.gameObject.SetActive(false);
    }

    private void OnRestartButtonClicked()
    {
        _gameCenter.OnRestart();
    }

    private void OnEnable()
    {
        _gameCenter.GameEnded += OnEnd;
        _gameCenter.GameRestarted += OnRestart;
        _restartButton.onClick.AddListener(OnRestartButtonClicked);
    }

    private void OnDisable()
    {
        _gameCenter.GameEnded -= OnEnd;
        _gameCenter.GameRestarted -= OnRestart;
        _restartButton.onClick.RemoveListener(OnRestartButtonClicked);
    }
}
