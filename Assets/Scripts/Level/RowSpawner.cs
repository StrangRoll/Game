using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RowSpawner : MonoBehaviour
{
    [SerializeField] private float _minSecondsBetweenSpawn;
    [SerializeField] private float _maxSecondsBetweenSpawn;
    [SerializeField] private Transform _leftSpawnPosition;
    [SerializeField] private Transform _rightSpawnPosition;
    [SerializeField] private Row _rowPrefab;
    [SerializeField] private float _minRowSpeed;
    [SerializeField] private float _maxRowSpeed;
    [SerializeField] private Camera _camera;

    private float _leftSpawnX;
    private float _rightSpawnX;
    private float _spawnHeight;
    private IEnumerator _spawnCoroutine;

    private void Start()
    {
        _leftSpawnX = _leftSpawnPosition.position.x;
        _rightSpawnX = _rightSpawnPosition.position.x;
        var screenHeight = _camera.ViewportToScreenPoint(Vector3.up).y;
        var rowHeight = _rowPrefab.GetComponent<SpriteRenderer>().sprite.rect.height;
        _spawnHeight = screenHeight + rowHeight / 2;
    }

    private void OnEnable()
    {
        _spawnCoroutine = RowSpawnTimer();
        StartCoroutine(_spawnCoroutine);
    }

    private void OnDisable()
    {
        StopCoroutine(_spawnCoroutine);
    }

    private IEnumerator RowSpawnTimer()
    {
        while (true)
        {
            var timeBeforNewRow = Random.Range(_minSecondsBetweenSpawn, _maxSecondsBetweenSpawn);
            yield return new WaitForSeconds(timeBeforNewRow);
            var newRowX = Random.Range(_leftSpawnX, _rightSpawnX);
            var newRowY = _camera.ScreenToWorldPoint(Vector3.up * _spawnHeight).y;
            var newRowPosition = new Vector3(newRowX, newRowY, 0);
            var newRow = Instantiate(_rowPrefab, newRowPosition, Quaternion.identity, transform);
            var newRowSpeed = Random.Range(_minRowSpeed, _maxRowSpeed);
            int newRowDirection;
            if (Random.Range(0, 2) == 0)
                newRowDirection = RowDirection.Right;
            else
                newRowDirection = RowDirection.Left;

            newRow.Init(newRowDirection, newRowSpeed);
        }
    }
}
