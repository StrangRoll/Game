using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class PlayerReviver : MonoBehaviour
{
    [SerializeField] private float _xRevivePosition;
    [SerializeField] private PlayerMovier _playerMovier;

    [Inject] private Player _player;
    [Inject] private GameCenter _center;

    private Vector3 _deathPosition;

    public event UnityAction PlayerRevived;

    private void OnEnable()
    {
        _center.Game—ontinued += OnGame—ontinued;
        _center.GameEnded += OnGameEnded;
    }

    private void OnDisable()
    {
        _center.Game—ontinued -= OnGame—ontinued;
        _center.GameEnded -= OnGameEnded;
    }

    private void OnGame—ontinued()
    {
        var revivePosition = new Vector3(_xRevivePosition, _deathPosition.y, 0);
        _player.transform.position = revivePosition;
        PlayerRevived?.Invoke();
    }

    private void OnGameEnded()
    {
        _deathPosition = _player.transform.position;
    }

}
