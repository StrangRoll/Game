using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

[RequireComponent(typeof(PlayerMovier))]
[RequireComponent(typeof(PlayerAnimationChanger))]
[RequireComponent(typeof(PlayerSoundController))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]

public class Player : MonoBehaviour
{
    [SerializeField] private GameCenter _gameCenter;

    [Inject] private Camera _camera;
    [Inject] private PlayerReviver _reviver;

    private PlayerInput _playerInput;
    private PlayerMovier _movier;
    private PlayerAnimationChanger _animationChanger;
    private PlayerSoundController _soundController;
    private int _startYPosition;
    private int _score = 0;
    private bool _isJumping = false;
    private BoxCollider2D _collider;
    private WaitForFixedUpdate OneFixedFrame = new WaitForFixedUpdate();
    private float _heroHalfHeight;
    private float _screenHeight;

    public event UnityAction<int> ScoreChanged; 

    private void Awake()
    {
        _playerInput = new PlayerInput();
        _playerInput.Player.Jump.performed += ctx => OnJump();
        _movier = GetComponent<PlayerMovier>();
        _animationChanger = GetComponent<PlayerAnimationChanger>();
        _soundController = GetComponent<PlayerSoundController>();
        _startYPosition = (int)transform.position.y;
        _collider = GetComponent<BoxCollider2D>();
        _heroHalfHeight = GetComponent<SpriteRenderer>().sprite.rect.height / 2;
        _screenHeight = _camera.ViewportToScreenPoint(Vector3.up).y;
    }

    private void OnEnable()
    {
        _playerInput.Enable();
        _gameCenter.GameEnded += OnGameEnded;
        _gameCenter.GameRestarted += OnGameRestarted;
        _reviver.PlayerRevived += OnPlayerRevived;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Wall>(out Wall wall) && _isJumping)
        {
            _isJumping = false;
            _animationChanger.WallCollision();
            _movier.WallCollision();
        }

        if (collision.gameObject.TryGetComponent<Row>(out Row row))
        {
            _gameCenter.OnEnd();
        }

        if (collision.gameObject.TryGetComponent<DeadZone>(out DeadZone deadZone))
        {
            _gameCenter.OnEnd();
        }
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

    private void OnDisable()
    {
        _playerInput.Disable();
        _gameCenter.GameEnded -= OnGameEnded;
        _gameCenter.GameRestarted -= OnGameRestarted;
        _reviver.PlayerRevived -= OnPlayerRevived;
    }

    public void OnPlayerRevived()
    {
        _collider.enabled = true;
        _isJumping = false;
    }

    private void OnJump()
    {
        if (_isJumping == false)
        {
            _movier.Jump();
            _animationChanger.StartJumpAnimation();
            _soundController.Jump();
            StartCoroutine(WaitOneFixedFrameAndChangeIsjumping());
        }
    } 

    private void OnGameEnded()
    {
        _movier.ResetGravityScale();
        _soundController.Death();
        _collider.enabled = false;
    }

    private void OnGameRestarted()
    {
        _collider.enabled = true;
        _movier.Reset();
        _animationChanger.Reset();
        _isJumping = false;
        _score = 0;
        ScoreChanged?.Invoke(_score);
    }

    private IEnumerator WaitOneFixedFrameAndChangeIsjumping()
    {
        yield return OneFixedFrame;
        _isJumping = true;
    }
}
