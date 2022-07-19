using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovier : MonoBehaviour
{
    [SerializeField] private float _wallSlidingSpeed;
    [SerializeField] private float _jumpForce;

    private Rigidbody2D _rigidBody;

    public void Jump()
    {
        _rigidBody.simulated = true;
        _rigidBody.AddForce(PlayerJumpDirection.CurrentVector * _jumpForce, ForceMode2D.Impulse);
        PlayerJumpDirection.ChangeDirection();
    }

    public void WallCollision()
    {
        _rigidBody.simulated = false;
    }

    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
    }
}

public static class PlayerJumpDirection
{
    public static Vector2 CurrentVector;

    private static float xRigthJumpDirection = 1.1f;
    private static Vector2 RightVector;
    private static Vector2 LeftVector;
    private static float yJumpDirection = 1;

    static PlayerJumpDirection()
    {
        RightVector = new Vector2(xRigthJumpDirection, yJumpDirection).normalized;
        LeftVector = new Vector2(-xRigthJumpDirection, yJumpDirection).normalized;
        CurrentVector = RightVector;
    }

    public static void ChangeDirection()
    {
        if (CurrentVector == RightVector)
            CurrentVector = LeftVector;
        else
            CurrentVector = RightVector;
    }
}
