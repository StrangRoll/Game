using UnityEngine;
using Zenject;

public class Row : MonoBehaviour, IPauseHandler
{
    [Inject] private PauseManager _pauseManager;

    private int _direction;
    private float _currentSpeed;
    private float _speed;

    private void OnEnable()
    {
        _pauseManager.Register(this);
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
        transform.position += Vector3.right * _direction * _currentSpeed * Time.deltaTime;
    }

    private void OnDisable()
    {
        _pauseManager.Register(this);
    }

    public void Init(int startDirection, float speed)
    {
        _direction = startDirection;
        _speed = speed;
        _currentSpeed = speed;
    }

    public void Pause(bool isPause)
    {
        if (isPause)
        {
            _currentSpeed = 0;
        }
        else
        {
            _currentSpeed = _speed;
        }
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
