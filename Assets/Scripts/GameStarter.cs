using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameStarter : MonoBehaviour
{
    public event UnityAction GameStarted;

    private PlayerInput _playerInput;

    private void Awake()
    {
        _playerInput = new PlayerInput();
        _playerInput.World.Start.performed += ctx => OnStart();
    }

    private void OnEnable()
    {
        _playerInput.Enable();
    }

    private void OnDisable()
    {
        _playerInput.Disable();
    }

    private void OnStart()
    {
        GameStarted?.Invoke();
    }

}
