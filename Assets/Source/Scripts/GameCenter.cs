using UnityEngine;
using UnityEngine.Events;

public class GameCenter : MonoBehaviour
{
    private PlayerInput _playerInput;
    private GameCenter _center;
    private bool _isGameEnded = false;

    public event UnityAction GameStarted;
    public event UnityAction GameEnded;
    public event UnityAction GameRestarted;

    private void Awake()
    {
        _playerInput = new PlayerInput();
        _playerInput.World.Start.performed += ctx => _center.OnStart();
        _center = GetComponent<GameCenter>();
    }

    private void OnEnable()
    {
        _playerInput.Enable();
    }

    private void OnDisable()
    {
        _playerInput.Disable();
    }

    public void OnEnd()
    {
        if (_isGameEnded == false)
        {
            GameEnded?.Invoke();
            _isGameEnded = true;
        }
    }

    public void OnRestart()
    {
        _isGameEnded = false;
        StartIteration.ResetStartIteraton();
        GameRestarted?.Invoke();
    }

    private void OnStart()
    {
        GameStarted?.Invoke();
    }
}