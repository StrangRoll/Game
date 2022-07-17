using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovier))]
[RequireComponent(typeof(PlayerAnimationChanger))]

public class Player : MonoBehaviour
{
    private PlayerInput _playerInput;
    private PlayerMovier _movier;
    private PlayerAnimationChanger _animationChanger;


    private void Awake()
    {
        _playerInput = new PlayerInput();
        _playerInput.Player.Jump.performed += ctx => OnJump();
        _movier = GetComponent<PlayerMovier>();
        _animationChanger = GetComponent<PlayerAnimationChanger>();
    }

    private void OnEnable()
    {
        _playerInput.Enable();
    }

    private void OnDisable()
    {
        _playerInput.Disable();
    }

    private void OnJump()
    {
        _movier.Jump();
        _animationChanger.StartJumpAnimation();
    }

  
}
