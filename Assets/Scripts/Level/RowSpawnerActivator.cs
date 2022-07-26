using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RowSpawnerActivator : MonoBehaviour
{
    [SerializeField] GameCenter _gameCenter;
    [SerializeField] RowSpawner _rowSpawner;

    private void DeActivateRowSpawner()
    {
        _rowSpawner.enabled = false;
    }

    private void ActivateRowSpawner()
    {
        _rowSpawner.enabled = true;
    }

    private void DeactivateRows()
    {
        _rowSpawner.DeactivateRows();
    }

    private void OnEnable()
    {
        _gameCenter.GameStarted += ActivateRowSpawner;
        _gameCenter.GameEnded += DeActivateRowSpawner;
        _gameCenter.GameRestarted += DeactivateRows;
    }

    private void OnDisable()
    {
        _gameCenter.GameStarted -= ActivateRowSpawner;
        _gameCenter.GameEnded -= DeActivateRowSpawner;
        _gameCenter.GameRestarted += DeactivateRows;
    }
}
