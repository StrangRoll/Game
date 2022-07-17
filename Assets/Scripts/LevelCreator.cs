using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCreator : MonoBehaviour
{
    [SerializeField] private CameraMovier _cameraMovier;
    [SerializeField] private Wall[] _rightWalls;
    [SerializeField] private Wall[] _leftWalls;

    private Queue<Wall> _leftWallsQueue;
    private Queue<Wall> _rightWallsQueue;

    private void Start()
    {
        _leftWallsQueue = new Queue<Wall>();
        _rightWallsQueue = new Queue<Wall>();

        foreach (Wall wall in _rightWalls)
        {
            _rightWallsQueue.Enqueue(wall);
        }

        foreach (Wall wall in _leftWalls)
        {
            _leftWallsQueue.Enqueue(wall);
        }
    }

    private void SpawnWall(float elevateWallCount)
    {
        Wall lowerLeftWall = _leftWallsQueue.Dequeue();
        lowerLeftWall.transform.position += new Vector3(0, elevateWallCount, 0);
        Wall lowerRightWall = _rightWallsQueue.Dequeue();
        lowerRightWall.transform.position += new Vector3(0, elevateWallCount, 0);
        _leftWallsQueue.Enqueue(lowerLeftWall);
        _rightWallsQueue.Enqueue(lowerRightWall);
    }

    private void OnEnable()
    {
        _cameraMovier.WallHeightReached += SpawnWall;
    }

    private void OnDisable()
    {
        _cameraMovier.WallHeightReached -= SpawnWall;
    }
}