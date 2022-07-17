using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CameraMovier : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _wallHeight;

    private float _previousPositionY;

    public event UnityAction<float> WallHeightReached;

    private void Start()
    {
        _previousPositionY = transform.position.y;
    }

    private void Update()
    {
        transform.Translate(Vector3.up * _speed * Time.deltaTime);

        if (transform.position.y - _previousPositionY >= _wallHeight)
        {
            WallHeightReached?.Invoke(_wallHeight * 2);
            _previousPositionY = transform.position.y;
        }
    }
}
