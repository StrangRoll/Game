using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimationChanger : MonoBehaviour
{
    private Animator _animator;
    private SpriteRenderer _sprite;
    private bool _startFlipX;

    public void StartJumpAnimation()
    {
        _animator.SetTrigger(AnimatorPlayerController.JumpTrigger);
    }

    public void WallCollision()
    {
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
        _startFlipX = _sprite.flipX;
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
