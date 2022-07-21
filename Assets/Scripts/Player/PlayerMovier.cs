using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovier : MonoBehaviour
{
    [SerializeField] private float _wallSlidingSpeed;
    [SerializeField] private float _jumpForce;

    private Rigidbody2D _rigidBody;
    private Vector3 _startPosition;
    private Quaternion _startRotation;

    public void Jump()
    {
        _rigidBody.simulated = true;
        _rigidBody.AddForce(PlayerJumpDirection.CurrentVector * _jumpForce, ForceMode2D.Impulse);
        PlayerJumpDirection.ChangeDirection();
    }

    public void Reset()
    {
        _rigidBody.velocity = Vector2.zero;
        _rigidBody.angularVelocity = 0;
        transform.rotation = _startRotation;
        transform.position = _startPosition;
        PlayerJumpDirection.ResetDirection();
    }

    public void WallCollision()
    {
        _rigidBody.simulated = false;
    }

    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _startPosition = transform.position;
        _startRotation = transform.rotation;
    }
}

public static class PlayerJumpDirection
{
    public static Vector2 CurrentVector;

    private static float xRigthJumpDirection = 1.1f;
    private static Vector2 rightVector;
    private static Vector2 leftVector;
    private static float yJumpDirection = 1;

    static PlayerJumpDirection()
    {
        rightVector = new Vector2(xRigthJumpDirection, yJumpDirection).normalized;
        leftVector = new Vector2(-xRigthJumpDirection, yJumpDirection).normalized;
        CurrentVector = rightVector;
    }

    public static void ResetDirection()
    {
        CurrentVector = rightVector;
    }

    public static void ChangeDirection()
    {
        if (CurrentVector == rightVector)
            CurrentVector = leftVector;
        else
            CurrentVector = rightVector;
    }
}
