using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimationChanger : MonoBehaviour
{
    private Animator _animator;
    private SpriteRenderer _sprite;
    private bool _startFlipX;
    private bool _currentLookDirection;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _sprite = GetComponent<SpriteRenderer>();
        _startFlipX = _sprite.flipX;
        Reset();
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

    public void Reset()
    {
        PlayerLookDirection.ResetDirection();
        _animator.SetTrigger(AnimatorPlayerController.RetartTrigger);
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
