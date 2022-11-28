using UnityEngine;
using Zenject;

[RequireComponent(typeof(Rigidbody2D))]

public class PlayerMovier : MonoBehaviour, IPauseHandler
{
    [SerializeField] private float _jumpForce;

    [Inject] private PlayerReviver _reviver;
    [Inject] private PauseManager _pauseManager;

    private Rigidbody2D _rigidBody;
    private Vector3 _startPosition;
    private Quaternion _startRotation;
    private Vector2 _currentJumpDirection;
    private float _startGravityScale;
    private Vector2 _oldVelocity;
    private float _oldGravityScale;

    private void OnEnable()
    {
        _reviver.PlayerRevived += OnPlayerRevived;
        _pauseManager.Register(this);
    }

    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _startGravityScale = _rigidBody.gravityScale;
        _startPosition = transform.position;
        _startRotation = transform.rotation;
        Reset();
    }

    private void OnDisable()
    {
        _reviver.PlayerRevived -= OnPlayerRevived;
        _pauseManager.UnRegister(this);
    }

    public void Jump()
    {
        _rigidBody.gravityScale = _startGravityScale; 
        _rigidBody.velocity = _currentJumpDirection * _jumpForce;
        _currentJumpDirection = PlayerJumpDirection.ChangeDirection(_currentJumpDirection);
    }

    public void Reset()
    {
        _rigidBody.velocity = Vector2.zero;
        _rigidBody.angularVelocity = 0;
        transform.rotation = _startRotation;
        transform.position = _startPosition;
        _currentJumpDirection = PlayerJumpDirection.ResetDirection();
    }

    public void WallCollision()
    {
        _rigidBody.gravityScale = 0;
    }

    public void ResetGravityScale()
    {
        _rigidBody.gravityScale = _startGravityScale;
    }

    public void Pause(bool isPause)
    {
        if (isPause)
        {
            _oldVelocity = _rigidBody.velocity;
            _rigidBody.velocity = Vector2.zero;
            _oldGravityScale = _rigidBody.gravityScale;
            _rigidBody.gravityScale = 0;
        }
        else
        {
            _rigidBody.gravityScale = _oldGravityScale;
            _rigidBody.velocity = _oldVelocity;
        }
    }

    private void OnPlayerRevived()
    {
        _rigidBody.velocity = Vector2.zero;
        _rigidBody.angularVelocity = 0;
        _rigidBody.gravityScale = 0;
        transform.rotation = _startRotation;
        _currentJumpDirection = PlayerJumpDirection.ResetDirection();
    }
}

public static class PlayerJumpDirection
{
    private static float xRigthJumpDirection = 1.1f;
    private static Vector2 rightVector;
    private static Vector2 leftVector;
    private static float yJumpDirection = 1;

    static PlayerJumpDirection()
    {
        rightVector = new Vector2(xRigthJumpDirection, yJumpDirection).normalized;
        leftVector = new Vector2(-xRigthJumpDirection, yJumpDirection).normalized;
    }

    public static Vector2 ResetDirection()
    {
        return rightVector;
    }

    public static Vector2 ChangeDirection(Vector2 currentDirection)
    {
        if (currentDirection == rightVector)
            return leftVector;
        else
            return rightVector;
    }
}
