using UnityEngine.InputSystem;

[UnityEditor.InitializeOnLoad]

public class StartIteration : IInputInteraction
{
    private static bool IsFirstClickReleased = false;

    static StartIteration()
    {
        InputSystem.RegisterInteraction<StartIteration>();
    }

    public static void ResetStartIteraton()
    {
        IsFirstClickReleased = false;
    }

    public void Process(ref InputInteractionContext context)
    {
        if (IsFirstClickReleased)
        {
            context.Canceled();
            return;
        }

        context.Performed();
        IsFirstClickReleased = true;
    }

    public void Reset()
    {
    }
}
