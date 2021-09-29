using UnityEngine;

public class FieldInfo
{
    public readonly int fieldWidth = 9;
    public readonly int fieldHeight = 19;
    public Transform[,] blockCells; 

    public FieldInfo()
    {
        blockCells = new Transform[fieldWidth,fieldHeight + 3];//+3 for spawner
    }

}
