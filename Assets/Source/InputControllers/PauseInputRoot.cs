using UnityEngine;
using UnityEngine.Events;

public class PauseInputRoot : MonoBehaviour
{
    private PlayerInput _playerInput;

    public event UnityAction PauseButtonPressed;

    private void Awake()
    {
        _playerInput = new PlayerInput();
        _playerInput.World.Pause.performed += ctx => OnPause();
    }

    private void OnEnable()
    {
        _playerInput.Enable();
    }

    private void OnDisable()
    {
        _playerInput.Disable();
    }

    private void OnPause()
    {
        PauseButtonPressed?.Invoke();
    }
}
