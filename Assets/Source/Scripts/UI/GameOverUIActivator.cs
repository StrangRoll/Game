using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class GameOverUIActivator : MonoBehaviour
{
    [SerializeField] private TMP_Text _gameOverText;
    [SerializeField] private Button _restartButton;

    [Inject] private GameCenter _gameCenter;

    private void OnEnable()
    {
        _gameCenter.GameEnded += OnGameEnded;
        _gameCenter.GameRestarted += OnGameRestarted;
        _restartButton.onClick.AddListener(OnRestartButtonClicked);
    }

    private void OnDisable()
    {
        _gameCenter.GameEnded -= OnGameEnded;
        _gameCenter.GameRestarted -= OnGameRestarted;
        _restartButton.onClick.RemoveListener(OnRestartButtonClicked);
    }

    private void OnGameEnded()
    {
        _gameOverText.gameObject.SetActive(true);
        _restartButton.gameObject.SetActive(true);
    }

    private void OnGameRestarted()
    {
        _gameOverText.gameObject.SetActive(false);
        _restartButton.gameObject.SetActive(false);
    }

    private void OnRestartButtonClicked()
    {
        _gameCenter.OnRestart();
    }
}
