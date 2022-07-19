using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimationChanger : MonoBehaviour
{
    private Animator _animator;
    private SpriteRenderer _sprite;

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

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _sprite = GetComponent<SpriteRenderer>();
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
