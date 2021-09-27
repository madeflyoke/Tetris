using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCreator : MonoBehaviour
{
    [SerializeField] private GameObject cell;
    [SerializeField] private GameObject borderCell;
    [SerializeField] private GameObject cellsHandler;
    [SerializeField] private GameObject borderHandler;
    public static int width = 9;
    public static int height = 19;
    void Awake()
    {
        SpawnFillCells();
        SpawnBorderCells();       
    }

    private void SpawnFillCells()
    {
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                Instantiate(cell, new Vector3(x, y, 0), Quaternion.identity, cellsHandler.transform);
            }
        }
    }

    private void SpawnBorderCells()
    {
        for (int x = -1; x <= width; x = width)
        {
            for (int y = height; y >= -1; y--)
            {
                if (x == width && y == -1)
                {
                    for (; x > -1; x--)
                    {
                        Instantiate(borderCell, new Vector3(x, y, 0), Quaternion.identity, borderHandler.transform);
                    }                     
                    return;
                }
                Instantiate(borderCell, new Vector3(x, y, 0), Quaternion.identity, borderHandler.transform);
            }
        }
    }

}
