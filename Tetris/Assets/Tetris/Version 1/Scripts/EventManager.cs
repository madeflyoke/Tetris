using System;
public class EventManager 
{
    public static Action blockOnGroundEvent;
    public static Action endGameEvent;

    public static void CallOnBlockOnGround()
    {
        blockOnGroundEvent?.Invoke();
    }

    public static void CallOnEndGame()
    {
        endGameEvent?.Invoke();
    }
}
