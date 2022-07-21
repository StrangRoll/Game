using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameCenter : MonoBehaviour
{
    private PlayerInput _playerInput;
    private GameCenter _center;

    public event UnityAction GameStarted;
    public event UnityAction GameEnded;
    public event UnityAction GameRestarted;

    public void OnEnd()
    {
        GameEnded?.Invoke();
    }

    public void OnRestart()
    {
        StartIteration.ResetStartIteraton();
        GameRestarted?.Invoke();
    }

    private void Awake()
    {
        _playerInput = new PlayerInput();
        _playerInput.World.Start.performed += ctx => _center.OnStart();
        _center = GetComponent<GameCenter>();
    }

    private void OnStart()
    {
        GameStarted?.Invoke();
    }

    private void OnEnable()
    {
        _playerInput.Enable();
    }

    private void OnDisable()
    {
        _playerInput.Disable();
    }
}
