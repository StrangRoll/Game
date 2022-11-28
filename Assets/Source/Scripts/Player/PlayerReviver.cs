using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class PlayerReviver : MonoBehaviour
{
    [SerializeField] private float _xRevivePosition;
    [SerializeField] private PlayerMovier _playerMovier;

    [Inject] private Player _player;
    [Inject] private GameCenter _center;
    [Inject] private Camera _camera;

    public event UnityAction PlayerRevived;

    private void OnEnable()
    {
        _center.Game—ontinued += OnGame—ontinued;
    }

    private void OnDisable()
    {
        _center.Game—ontinued -= OnGame—ontinued;
    }

    private void OnGame—ontinued()
    {
        var revivePosition = new Vector3(_xRevivePosition, _camera.transform.position.y, 0);
        _player.transform.position = revivePosition;
        PlayerRevived?.Invoke();
    }

}
