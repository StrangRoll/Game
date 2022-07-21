using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimationChanger : MonoBehaviour
{
    private Animator _animator;
    private SpriteRenderer _sprite;
    private float _directionChangesProtection = 0.5f;
    private WaitForSeconds _neededSecondsToProtect;
    private bool _isDirectionReadyToChange = true;
    private bool _startFlipX;

    public void StartJumpAnimation()
    {
        _animator.SetTrigger(AnimatorPlayerController.JumpTrigger);
    }

    public void WallCollision()
    {
        if (_isDirectionReadyToChange == false)
            return;

        _isDirectionReadyToChange = false;
        StartCoroutine(DirectionProtectionTimer());
        _animator.SetTrigger(AnimatorPlayerController.WallCollisionTrigger);
        PlayerLookDirection.ChangeDirection();
        _sprite.flipX = PlayerLookDirection.Current;
    }

    public void Reset()
    {
        PlayerLookDirection.ResetDirection();
        _animator.SetTrigger(AnimatorPlayerController.RetartTrigger);
        _sprite.flipX = _startFlipX;
    }

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _sprite = GetComponent<SpriteRenderer>();
        _neededSecondsToProtect = new WaitForSeconds(_directionChangesProtection);
        _startFlipX = _sprite.flipX;
    }

    private IEnumerator DirectionProtectionTimer()
    {
        yield return _neededSecondsToProtect;
        _isDirectionReadyToChange = true;
    }
}

public static class PlayerLookDirection
{

    public static bool Current;

    private static bool right = false;
    private static bool left = true;
    private static bool start;

    static PlayerLookDirection()
    {
        Current = right;
    }

    public static void ResetDirection()
    {
        Current = start;
    }

    public static void ChangeDirection()
    {
        if (Current == right)
            Current = left;
        else
            Current = right;
    }

}

public static class AnimatorPlayerController
{
    public const string JumpTrigger = "JumpTrigger";
    public const string WallCollisionTrigger = "WallCollisionTrigger";
    public const string RetartTrigger = "RestartTrigger";
}
