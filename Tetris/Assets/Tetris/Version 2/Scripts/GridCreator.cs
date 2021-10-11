using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCreator : MonoBehaviour
{
    //[SerializeField] private GameObject cell;  //all comments for spawning cells inside grid
    [SerializeField] private GameObject borderCell;
    //[SerializeField] private GameObject cellsHandler;
    [SerializeField] private GameObject borderHandler;
    private int width;
    private int height;

    public void Initialize(RepositoryBase repos)
    {      
        width = repos.FieldInfo.fieldWidth;
        height = repos.FieldInfo.fieldHeight;       
        //SpawnFillCells();
        SpawnBorderCells();
    }

    //private void SpawnFillCells()    
    //{
    //    for (int y = 0; y < height; y++)
    //    {
    //        for (int x = 0; x < width; x++)
    //        {
    //            Instantiate(cell, new Vector3(x, y, 0), Quaternion.identity, cellsHandler.transform);
    //        }
    //    }
    //}

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
