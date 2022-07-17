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
        _rigidBody.WakeUp();
        _rigidBody.AddForce(playerJumpDirection.JumpDirection * _jumpForce, ForceMode2D.Impulse);
        playerJumpDirection.ChangeDirection();
    }

    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Wall>(out Wall wall))
        {
            _rigidBody.Sleep();
        }
    }
}

public static class playerJumpDirection
{
    public static Vector2 JumpDirection;

    private static Vector2 jumpRightVector;
    private static Vector2 jumpLeftVector;
    private static float xRigthJumpDirection = 1.6f;
    private static float yJumpDirection = 1;

    static playerJumpDirection()
    {
        jumpRightVector = new Vector2(xRigthJumpDirection, yJumpDirection).normalized;
        jumpLeftVector = new Vector2(-xRigthJumpDirection, yJumpDirection).normalized;
        JumpDirection = jumpRightVector;
    }

    public static void ChangeDirection()
    {
        if (JumpDirection == jumpRightVector)
            JumpDirection = jumpLeftVector;
        else
            JumpDirection = jumpRightVector;
    }
}
