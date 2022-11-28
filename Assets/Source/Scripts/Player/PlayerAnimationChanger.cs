using UnityEngine;
using Zenject;

[RequireComponent(typeof(Animator))]
public class PlayerAnimationChanger : MonoBehaviour, IPauseHandler
{
    [Inject] private PlayerReviver _reviver;
    [Inject] private PauseManager _pauseManager;

    private Animator _animator;
    private SpriteRenderer _sprite;
    private bool _startFlipX;
    private bool _currentLookDirection;

    private void OnEnable()
    {
        _reviver.PlayerRevived += OnPlayerRevived;
        _pauseManager.Register(this);
    }

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _sprite = GetComponent<SpriteRenderer>();
        _startFlipX = _sprite.flipX;
        Reset();
    }

    private void OnDisable()
    {
        _reviver.PlayerRevived -= OnPlayerRevived;
        _pauseManager.UnRegister(this);
    }

    public void StartJumpAnimation()
    {
        _animator.SetTrigger(AnimatorPlayerController.JumpTrigger);
    }

    public void WallCollision()
    {
        _animator.SetTrigger(AnimatorPlayerController.WallCollisionTrigger);
        _currentLookDirection = PlayerLookDirection.ChangeDirection(_currentLookDirection);
        _sprite.flipX = _currentLookDirection;
    }

    public void Pause(bool isPause)
    {
        if (isPause)
        {
            _animator.speed = 0;
        }
        else
        {
            _animator.speed = 1;
        }
    }

    public void Reset()
    {
        PlayerLookDirection.ResetDirection();
        _animator.SetTrigger(AnimatorPlayerController.RetartTrigger);
        _sprite.flipX = _startFlipX;
        _currentLookDirection = PlayerLookDirection.ResetDirection();
    }

    private void OnPlayerRevived()
    {
        PlayerLookDirection.ResetDirection();
        _animator.SetTrigger(AnimatorPlayerController.WallCollisionTrigger);
        _sprite.flipX = _startFlipX;
        _currentLookDirection = PlayerLookDirection.ResetDirection();
    }
}

public static class PlayerLookDirection
{
    private static bool right = false;
    private static bool left = true;
    private static bool start;

    static PlayerLookDirection()
    {
        start = right;
    }

    public static bool ResetDirection()
    {
        return start;
    }

    public static bool ChangeDirection(bool currentLookDirection)
    {
        if (currentLookDirection == right)
            return left;
        else
            return right;
    }

}

public static class AnimatorPlayerController
{
    public const string JumpTrigger = "JumpTrigger";
    public const string WallCollisionTrigger = "WallCollisionTrigger";
    public const string RetartTrigger = "RestartTrigger";
}
