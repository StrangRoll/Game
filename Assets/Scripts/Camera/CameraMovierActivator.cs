using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovierActivator : MonoBehaviour
{
    [SerializeField] GameStarter _gameStarter;
    [SerializeField] CameraMovier _cameraMovier;

    private void ActivateCameraMovier()
    {
        _cameraMovier.enabled = true;
    }

    private void OnEnable()
    {
        _gameStarter.GameStarted += ActivateCameraMovier;
    }

    private void OnDisable()
    {
        _gameStarter.GameStarted -= ActivateCameraMovier;
    }
}
