using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class GameCenter : MonoBehaviour
{
    [Inject] private VideoAdd _videoAdd;

    private PlayerInput _playerInput;
    private bool _isReadyForStart = true;

    public event UnityAction GameStarted;
    public event UnityAction GameEnded;
    public event UnityAction GameRestarted;
    public event UnityAction Game—ontinued;

    public bool IsGameEnded { get; private set; } = false;

    private void Awake()
    {
        _playerInput = new PlayerInput();
        _playerInput.World.Start.performed += ctx => OnStart();
    }

    private void OnEnable()
    {
        _playerInput.Enable();
        _videoAdd.VideoRewardCollected += OnVideoRewardCollected;
    }

    private void OnDisable()
    {
        _playerInput.Disable();
        _videoAdd.VideoRewardCollected -= OnVideoRewardCollected;
    }

    public void OnEnd()
    {
        if (IsGameEnded == false)
        {
            GameEnded?.Invoke();
            IsGameEnded = true;
            _isReadyForStart = false;
        }
    }

    public void OnRestart()
    {
        IsGameEnded = false;
        GameRestarted?.Invoke();
        _isReadyForStart = true;
    }

    private void OnStart()
    {
        if (_isReadyForStart)
        {
            GameStarted?.Invoke();
            _isReadyForStart = false;
        }
    }

    private void OnVideoRewardCollected()
    {
        Game—ontinued?.Invoke();
        IsGameEnded = false;
        _isReadyForStart = true;
    }
}