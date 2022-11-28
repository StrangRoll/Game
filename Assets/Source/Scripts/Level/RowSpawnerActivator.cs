using UnityEngine;
using Zenject;

public class RowSpawnerActivator : MonoBehaviour
{
    [SerializeField] private RowSpawner _rowSpawner;

    [Inject] private GameCenter _gameCenter;

    private void OnEnable()
    {
        _gameCenter.GameStarted += OnGameStarted;
        _gameCenter.GameEnded += OnGameEnded;
        _gameCenter.GameRestarted += OnGameRestarted;
        _gameCenter.Game—ontinued += OnGame—ontinued;
    }

    private void OnDisable()
    {
        _gameCenter.GameStarted -= OnGameStarted;
        _gameCenter.GameEnded -= OnGameEnded;
        _gameCenter.GameRestarted += OnGameRestarted;
        _gameCenter.Game—ontinued -= OnGame—ontinued;
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

    private void OnGame—ontinued()
    {
        _rowSpawner.DeactivateRows();
    }
}
