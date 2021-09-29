using System;
public class EventManager 
{
    public static Action blockOnGroundEvent;
    public static Action endGameEvent;
    public static Action<Direction> blockMoveEvent;
    public static Action spawnBlockEvent;

    public static void CallOnBlockOnGround()
    {
        blockOnGroundEvent?.Invoke();
    }

    public static void CallOnEndGame()
    {
        endGameEvent?.Invoke();
    }
    public static void CallOnBlockMove(Direction dir)
    {
        blockMoveEvent?.Invoke(dir);
    }
    public static void CallOnSpawnBlock()
    {
        spawnBlockEvent?.Invoke();
    }
}

