using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class LevelCreator : MonoBehaviour
{
    [SerializeField] private Wall[] _rightWalls;
    [SerializeField] private Wall[] _leftWalls;

    [Inject] private CameraMovier _cameraMovier;
    [Inject] private GameCenter _gameCenter;

    private Vector3[] _rightWallsStartPosition;
    private Vector3[] _leftWallsStartPosition;
    private Queue<Wall> _leftWallsQueue;
    private Queue<Wall> _rightWallsQueue;
    private int _wallsCount;

    private void OnEnable()
    {
        _cameraMovier.WallHeightReached += OnWallHeightReached;
        _gameCenter.GameRestarted += OnGameRestarted;
    }

    private void Start()
    {
        _leftWallsQueue = new Queue<Wall>();
        _rightWallsQueue = new Queue<Wall>();
        _wallsCount = _rightWalls.Length;
        _leftWallsStartPosition = new Vector3[_wallsCount];
        _rightWallsStartPosition = new Vector3[_wallsCount];

        for (int i = 0; i < _wallsCount; i++)
        {
            _rightWallsQueue.Enqueue(_rightWalls[i]);
            _rightWallsStartPosition[i] = _rightWalls[i].transform.position;
        }

        for (int i = 0; i < _wallsCount; i++)
        {
            _leftWallsQueue.Enqueue(_leftWalls[i]);
            _leftWallsStartPosition[i] = _leftWalls[i].transform.position;
        }
    }

    private void OnDisable()
    {
        _cameraMovier.WallHeightReached -= OnWallHeightReached;
        _gameCenter.GameRestarted -= OnGameRestarted;
    }

    private void OnWallHeightReached(float wallHeight)
    {
        var elevateWallCount = _wallsCount * wallHeight;
        Wall lowerLeftWall = _leftWallsQueue.Dequeue();
        lowerLeftWall.transform.position += new Vector3(0, elevateWallCount, 0);
        Wall lowerRightWall = _rightWallsQueue.Dequeue();
        lowerRightWall.transform.position += new Vector3(0, elevateWallCount, 0);
        _leftWallsQueue.Enqueue(lowerLeftWall);
        _rightWallsQueue.Enqueue(lowerRightWall);
    }

    private void OnGameRestarted()
    {
        _leftWallsQueue = new Queue<Wall>();
        _rightWallsQueue = new Queue<Wall>();

        for (int i = 0; i<_wallsCount; i++)
        {
            _rightWallsQueue.Enqueue(_rightWalls[i]);
            _rightWalls[i].transform.position = _rightWallsStartPosition[i];
        }

        for (int i = 0; i < _wallsCount; i++)
        {
            _leftWallsQueue.Enqueue(_leftWalls[i]);
            _leftWalls[i].transform.position = _leftWallsStartPosition[i];
        }
    }
}