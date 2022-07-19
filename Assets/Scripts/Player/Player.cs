using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(PlayerMovier))]
[RequireComponent(typeof(PlayerAnimationChanger))]

public class Player : MonoBehaviour
{
    private PlayerInput _playerInput;
    private PlayerMovier _movier;
    private PlayerAnimationChanger _animationChanger;
    private int _startYPosition;
    private int _score = 0;

    public event UnityAction<int> ScoreChanged; 

    private void Awake()
    {
        _playerInput = new PlayerInput();
        _playerInput.Player.Jump.performed += ctx => OnJump();
        _movier = GetComponent<PlayerMovier>();
        _animationChanger = GetComponent<PlayerAnimationChanger>();
        _startYPosition = (int)transform.position.y;
    }

    private void Update()
    {
        var currentYPosition = (int)transform.position.y;

        if (currentYPosition - _startYPosition > _score)
        {
            _score = currentYPosition - _startYPosition;
            ScoreChanged?.Invoke(_score);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Wall>(out Wall wall))
        {
            _movier.WallCollision();
            _animationChanger.WallCollision();
        }
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
