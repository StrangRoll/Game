using UnityEngine;
using Zenject;

public class CameraMovierActivator : MonoBehaviour
{
    [Inject] private GameCenter _gameCenter;
    [Inject] private CameraMovier _cameraMovier;

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
        _gameCenter.GameRestarted += OnGameRestarted;
    }

    private void OnGameEnded()
    {
        _cameraMovier.enabled = false;
    }

    private void OnGameStarted()
    {
        _cameraMovier.enabled = true;
    }

    private void OnGameRestarted()
    {
        _cameraMovier.Reset();
    }
}
