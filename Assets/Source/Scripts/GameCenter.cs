using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class GameCenter : MonoBehaviour
{
    [Inject] private VideoAdd _videoAdd;

    private PlayerInput _playerInput;
    private GameCenter _center;
    private bool _isGameEnded = false;

    public event UnityAction GameStarted;
    public event UnityAction GameEnded;
    public event UnityAction GameRestarted;
    public event UnityAction Game—ontinued;

    private void Awake()
    {
        _playerInput = new PlayerInput();
        _playerInput.World.Start.performed += ctx => _center.OnStart();
        _center = GetComponent<GameCenter>();
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

    private void OnVideoRewardCollected()
    {
        Game—ontinued?.Invoke();
        _isGameEnded = false;
        StartIteration.ResetStartIteraton();
    }
}