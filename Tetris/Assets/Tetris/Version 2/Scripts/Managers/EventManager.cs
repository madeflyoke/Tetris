using System;
using System.Collections.Generic;
using UnityEngine;
public class EventManager 
{
    public static Action <GameState>changeGameStateEvent;

    public static Action blockOnGroundEvent;
    public static Action<Direction> blockMoveEvent;
    public static Action<Direction> inputButtonEvent;

    public static Action<int> pointsAddEvent;
    public static Action nextSpawnBlockEvent;
    public static Action<List<GameObject>, Transform> initSpawnerBlocksUIEvent;

    public static void CallOnChangeGameState(GameState gamestate)
    {
        changeGameStateEvent?.Invoke(gamestate);
    }

    public static void CallOnBlockOnGround()
    {
        blockOnGroundEvent?.Invoke();
    }

    public static void CallOnBlockMove(Direction dir)
    {
        blockMoveEvent?.Invoke(dir);
    }
    public static void CallOnPointsAdd(int points)
    {
        pointsAddEvent?.Invoke(points);
    }

    public static void CallOnNextSpawnBlock()
    {
        nextSpawnBlockEvent?.Invoke();
    }
 
    public static void CallOnInitBlocksUI(List<GameObject> initBlocks, Transform holder)
    {
        initSpawnerBlocksUIEvent?.Invoke(initBlocks,holder);
    }

    public static void CallOnInputButton(Direction dir)
    {
        inputButtonEvent?.Invoke(dir);
    }
}

