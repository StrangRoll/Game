using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(PlayerMovier))]
[RequireComponent(typeof(PlayerAnimationChanger))]
[RequireComponent(typeof(BoxCollider2D))]

public class Player : MonoBehaviour
{
    [SerializeField] private GameCenter _gameCenter;

    private PlayerInput _playerInput;
    private PlayerMovier _movier;
    private PlayerAnimationChanger _animationChanger;
    private int _startYPosition;
    private int _score = 0;
    private bool _isJumping = false;
    private BoxCollider2D _collider;

    public event UnityAction<int> ScoreChanged; 

    private void Awake()
    {
        _playerInput = new PlayerInput();
        _playerInput.Player.Jump.performed += ctx => OnJump();
        _movier = GetComponent<PlayerMovier>();
        _animationChanger = GetComponent<PlayerAnimationChanger>();
        _startYPosition = (int)transform.position.y;
        _collider = GetComponent<BoxCollider2D>();
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
            _animationChanger.WallCollision();
            _movier.WallCollision();
            _isJumping = false;
        }

        if (collision.gameObject.TryGetComponent<Row>(out Row row))
        {
            _gameCenter.OnEnd();
        }
    }

    private void OnJump()
    {
        if (_isJumping == false)
        {
            _animationChanger.StartJumpAnimation();
            _movier.Jump();
            _isJumping = true;
        }
    } 

    private void OnEnd()
    {
        _collider.enabled = false;
    }

    private void OnRestart()
    {
        _collider.enabled = true;
        _movier.Reset();
        _animationChanger.Reset();
        _isJumping = false;
        _score = 0;
        ScoreChanged?.Invoke(_score);
    }

    private void OnEnable()
    {
        _playerInput.Enable();
        _gameCenter.GameEnded += OnEnd;
        _gameCenter.GameRestarted += OnRestart;
    }

    private void OnDisable()
    {
        _playerInput.Disable();
        _gameCenter.GameEnded -= OnEnd;
        _gameCenter.GameRestarted -= OnRestart;
    }
}
