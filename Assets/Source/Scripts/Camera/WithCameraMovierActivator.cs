using UnityEngine;
using Zenject;

public class WithCameraMovierActivator : MonoBehaviour
{
    [SerializeField] private WithCameraMovier _withCameraMovier;

    [Inject] private GameCenter _gameCenter;

    private void OnEnable()
    {
        _gameCenter.GameStarted += OnGameStarted;
        _gameCenter.GameEnded += OnGameEnded;
        _gameCenter.GameRestarted += OnGameRestarted;
    }

    private void OnDisable()
    {
        _gameCenter.GameStarted -= OnGameStarted;
        _gameCenter.GameEnded -= OnGameEnded;
        _gameCenter.GameRestarted -= OnGameRestarted;
    }

    private void OnGameEnded()
    {
        _withCameraMovier.enabled = false;
    }

    private void OnGameStarted()
    {
        _withCameraMovier.enabled = true;
    }

    private void OnGameRestarted()
    {
        _withCameraMovier.Reset();
    }
}
