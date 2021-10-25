using UnityEngine;

public class FieldInfo
{
    public readonly int fieldWidth = 10;
    public readonly int fieldHeight = 20;
    public Transform[,] blockCells;

    public FieldInfo()
    {
        blockCells = new Transform[fieldWidth,fieldHeight + 3];//+3 for spawner
    }

}
