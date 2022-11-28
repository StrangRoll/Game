using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class WithCameraMovier : MonoBehaviour, IPauseHandler
{
    [Inject] private PauseManager _pauseManager;

    [SerializeField] private float _speed;
    [SerializeField] private float _wallHeight;
    
    private float _currentSpeed;

    private float _previousPositionY;
    private float _startPositionY;

    public event UnityAction<float> WallHeightReached;

    private void OnEnable()
    {
        _pauseManager.Register(this);
    }

    private void Start()
    {
        _previousPositionY = transform.position.y;
        _startPositionY = transform.position.y;
        _currentSpeed = _speed;
    }

    private void Update()
    {
        transform.Translate(Vector3.up * _currentSpeed * Time.deltaTime);

        if (transform.position.y - _previousPositionY >= _wallHeight)
        {
            WallHeightReached?.Invoke(_wallHeight);
            _previousPositionY = transform.position.y;
        }
    }

    private void OnDisable()
    {
        _pauseManager.UnRegister(this);
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

    public void Reset()
    {
        transform.position = new Vector3(transform.position.x, _startPositionY, transform.position.z);
        _previousPositionY = _startPositionY;
    }
}
