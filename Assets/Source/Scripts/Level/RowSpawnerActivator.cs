using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RowSpawnerActivator : MonoBehaviour
{
    [SerializeField] private GameCenter _gameCenter;
    [SerializeField] private RowSpawner _rowSpawner;

    private void OnEnable()
    {
        _gameCenter.GameStarted += OnGameStarted;
        _gameCenter.GameEnded += OnGameEnded;
        _gameCenter.GameRestarted += OnGameRestarted;
    }

    private void OnDisable()
    {
        _gameCenter.GameStarted -= OnGameStarted;
        _gameCenter.GameEnded -= OnGameEnded;
        _gameCenter.GameRestarted += OnGameRestarted;
    }

    private void OnGameEnded()
    {
        _rowSpawner.enabled = false;
    }

    private void OnGameStarted()
    {
        _rowSpawner.enabled = true;
    }

    private void OnGameRestarted()
    {
        _rowSpawner.DeactivateRows();
    }
}
