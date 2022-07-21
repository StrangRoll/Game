using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovierActivator : MonoBehaviour
{
    [SerializeField] GameCenter _gameCenter;
    [SerializeField] CameraMovier _cameraMovier;

    private void DeActivateCameraMovier()
    {
        _cameraMovier.enabled = false;
    }

    private void ActivateCameraMovier()
    {
        _cameraMovier.enabled = true;
    }

    private void ResetCameraPosition()
    {
        _cameraMovier.Reset();
    }

    private void OnEnable()
    {
        _gameCenter.GameStarted += ActivateCameraMovier;
        _gameCenter.GameEnded += DeActivateCameraMovier;
        _gameCenter.GameRestarted += ResetCameraPosition;
    }

    private void OnDisable()
    {
        _gameCenter.GameStarted -= ActivateCameraMovier;
        _gameCenter.GameEnded -= DeActivateCameraMovier;
        _gameCenter.GameRestarted += ResetCameraPosition;
    }
}
