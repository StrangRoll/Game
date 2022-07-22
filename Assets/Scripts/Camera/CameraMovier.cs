using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CameraMovier : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _wallHeight;

    private float _previousPositionY;
    private float _startPositionY;

    public event UnityAction<float> WallHeightReached;

    public void Reset()
    {
        transform.position = new Vector3(transform.position.x, _startPositionY, transform.position.z);
        _previousPositionY = _startPositionY;
    }

    private void Start()
    {
        _previousPositionY = transform.position.y;
        _startPositionY = transform.position.y;
    }

    private void Update()
    {
        transform.Translate(Vector3.up * _speed * Time.deltaTime);

        if (transform.position.y - _previousPositionY >= _wallHeight)
        {
            WallHeightReached?.Invoke(_wallHeight);
            _previousPositionY = transform.position.y;
        }
    }
}
