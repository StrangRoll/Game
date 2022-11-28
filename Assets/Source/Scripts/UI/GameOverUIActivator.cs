using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Zenject;

public class GameOverUIActivator : MonoBehaviour
{
    [SerializeField] private TMP_Text _gameOverText;
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _continueButton;

    [Inject] private GameCenter _gameCenter;

    private bool _isReviveUsed = false;

    public event UnityAction ContinueButtonPressed;

    private void OnEnable()
    {
        _gameCenter.GameEnded += OnGameEnded;
        _gameCenter.GameRestarted += OnGameRestarted;
        _gameCenter.Game—ontinued += OnGame—ontinued;
        _restartButton.onClick.AddListener(OnRestartButtonClicked);
        _continueButton.onClick.AddListener(OnContinueButtonClicked);
    }

    private void OnDisable()
    {
        _gameCenter.GameEnded -= OnGameEnded;
        _gameCenter.GameRestarted -= OnGameRestarted;
        _gameCenter.Game—ontinued -= OnGame—ontinued;
        _restartButton.onClick.RemoveListener(OnRestartButtonClicked);
        _continueButton.onClick.RemoveListener(OnContinueButtonClicked);
    }

    private void OnGameEnded()
    {
        _gameOverText.gameObject.SetActive(true);
        _restartButton.gameObject.SetActive(true);

        if (_isReviveUsed == false)
            _continueButton.gameObject.SetActive(true);
    }

    private void OnGameRestarted()
    {
        _isReviveUsed = false;
        DisableGameOverUI();
    }

    private void OnGame—ontinued()
    {
        _isReviveUsed = true;
        DisableGameOverUI();
    }   

    private void OnRestartButtonClicked()
    {
        _gameCenter.OnRestart();
    }

    private void OnContinueButtonClicked()
    {
        ContinueButtonPressed?.Invoke();
    }

    private void DisableGameOverUI()
    {
        _gameOverText.gameObject.SetActive(false);
        _restartButton.gameObject.SetActive(false);
        _continueButton.gameObject.SetActive(false);
    }
}
