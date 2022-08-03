using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovierActivator : MonoBehaviour
{
    [SerializeField] GameCenter _gameCenter;
    [SerializeField] CameraMovier _cameraMovier;

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
