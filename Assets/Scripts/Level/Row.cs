using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Row : MonoBehaviour
{
    private int _direction;
    private float _speed;

    public void Init(int startDirection, float speed)
    {
        _direction = startDirection;
        _speed = speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Wall>(out Wall wall))
        {
            _direction = RowDirection.ChangeDirection(_direction);
        }
    }

    private void Update()
    {
        transform.position += Vector3.right * _direction * _speed * Time.deltaTime;
    }
}

public static class RowDirection
{
    public static int Right = 1;
    public static int Left = -1;

    public static int ChangeDirection(int direction)
    {
        return direction * (-1);
    }
}
