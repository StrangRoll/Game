using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Row : MonoBehaviour
{
    private int _direction;
    private float _speed;
    private bool _isReadyToChangeDirection = true;
    private float _doubleChangeDirectionDefendorTime = 0.5f;
    private WaitForSeconds _whaitDoubleDirectionDefend;

    public void Init(int startDirection, float speed)
    {
        _direction = startDirection;
        _speed = speed;
    }

    private void Awake()
    {
        _whaitDoubleDirectionDefend = new WaitForSeconds(_doubleChangeDirectionDefendorTime);
    }

    private void Update()
    {
        transform.position += Vector3.right * _direction * _speed * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Wall>(out Wall wall))
        {
            if (_isReadyToChangeDirection)
            {
                _direction = RowDirection.ChangeDirection(_direction);
                _isReadyToChangeDirection = false;
                StartCoroutine(DoubleDirectionChangeDefender());
            }
        }
    }

    private IEnumerator DoubleDirectionChangeDefender()
    {
        yield return _whaitDoubleDirectionDefend;
        _isReadyToChangeDirection = true;
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
