using UnityEngine.InputSystem;

public class StartIterationRegistration
{
    [UnityEditor.InitializeOnLoadMethod]
    private static void Register()
    {
        InputSystem.RegisterInteraction<StartIteration>();
    }
}
