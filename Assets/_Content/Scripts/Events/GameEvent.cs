using System;

public static class GameEvent<T> where T : Delegate
{
    public static T Trigger { get; private set; }

    public static void Register(T response)
    {
        Trigger = Delegate.Combine(Trigger, response) as T;
    }

    public static void Deregister(T response)
    {
        Trigger = Delegate.Remove(Trigger, response) as T;
    }
}
