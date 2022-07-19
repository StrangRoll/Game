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

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _sprite = GetComponent<SpriteRenderer>();
        _neededSecondsToProtect = new WaitForSeconds(_directionChangesProtection);
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

    private static bool Right = false;
    private static bool Left = true;

    static PlayerLookDirection()
    {
        Current = Right;
    }

    public static void ChangeDirection()
    {
        if (Current == Right)
            Current = Left;
        else
            Current = Right;
    }

}

public static class AnimatorPlayerController
{
    public const string JumpTrigger = "JumpTrigger";
    public const string WallCollisionTrigger = "WallCollisionTrigger";
}
