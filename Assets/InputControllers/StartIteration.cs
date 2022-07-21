using UnityEngine.InputSystem;

public class StartIteration : IInputInteraction
{
    public float MaxTapDuration = 0.2f;

    private static bool IsFirstClickReleased = false;
    private static GameCenter _gameCenter;

    public static void ResetStartIteraton()
    {
        IsFirstClickReleased = false;
    }

    public void Process(ref InputInteractionContext context)
    {
        if (context.timerHasExpired || IsFirstClickReleased)
        {
            context.Canceled();
            return;
        }

        switch (context.phase)
        {
            case InputActionPhase.Waiting:
                context.Started();
                context.SetTimeout(MaxTapDuration);
                break;

            case InputActionPhase.Started:
                context.Performed();
                IsFirstClickReleased = true;
                break;
        }
    }

    public void Reset()
    {
    }

    [UnityEditor.InitializeOnLoadMethod]
    private static void Register()
    {
        InputSystem.RegisterInteraction<StartIteration>();
    }
}
