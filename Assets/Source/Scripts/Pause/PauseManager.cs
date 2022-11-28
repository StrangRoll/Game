using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour, IPauseHandler
{
    [SerializeField] private PauseInputRoot _root;

    private List<IPauseHandler> _pauseHandlers = new List<IPauseHandler>();

    public bool IsPaused { get; private set; } = false;

    private void OnEnable()
    {
        _root.PauseButtonPressed += OnPauseButtonPressed;
    }

    private void OnDisable()
    {
        _root.PauseButtonPressed += OnPauseButtonPressed;
    }

    public void Pause(bool isPause)
    {
        IsPaused = isPause;

        foreach (var pauseHandler in _pauseHandlers)
        {
            pauseHandler.Pause(isPause);
        }
    }

    public void Register(IPauseHandler pauseHandler)
    {
        _pauseHandlers.Add(pauseHandler);
    }

    public void UnRegister(IPauseHandler pauseHandler)
    {
        if (_pauseHandlers.Contains(pauseHandler)){
            _pauseHandlers.Remove(pauseHandler);
        }
        else
        {
            Debug.LogError($"Try to remove {pauseHandler} from pause handler list, but list don't contains it");
        }
    }

    private void OnPauseButtonPressed()
    {
        Pause(!IsPaused);
    }
}
