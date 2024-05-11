namespace Fischless.GamepadCapture.RawInput;

public delegate void RecordEventMethod(Event e);

public static class Recorder
{
    public static event Action<Event>? Received;

    public static void RecordInput(double precise, bool pressed, Input input)
    {
        Received?.Invoke(new Event(precise, pressed, input));
    }
}
