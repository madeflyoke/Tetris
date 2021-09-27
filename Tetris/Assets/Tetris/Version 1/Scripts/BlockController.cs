using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour
{
    [SerializeField] private Vector3 rotationPoint;
    private float prevTime;
    private float fallTime = 0.9f;
    private float shiftFallTime = 10f;

    private int width = GridCreator.width;
    private int height = GridCreator.height;
    private static Transform[,] blockCells= new Transform[GridCreator.width, GridCreator.height + 1];

    private void Awake()
    {
        CheckEndLine();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            transform.position += new Vector3(-1f, 0f, 0f);
            if (!ValidMove())
            {
                transform.position -= new Vector3(-1f, 0f, 0f);
            }
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            transform.position += new Vector3(1f, 0f, 0f);
            if (!ValidMove())
            {
                transform.position -= new Vector3(1f, 0f, 0f);
            }
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), 90f);
            if (!ValidMove())
            {
                transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), -90f);
            }
        }

        if (Time.time > prevTime + (Input.GetKey(KeyCode.S) ? fallTime / shiftFallTime : fallTime))
        {
            transform.position += new Vector3(0f, -1f, 0f);
            if (!ValidMove())
            {
                transform.position -= new Vector3(0f, -1f, 0f);
                AddBlockCell();
                CheckLines();
                enabled = false;
                EventManager.CallOnBlockOnGround();
            }
            prevTime = Time.time;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.TransformPoint(rotationPoint), 0.1f);
    }

    private void CheckEndLine()
    {
        for (int x = 0; x < width; x++)
        {
            if (blockCells[x,height]!=null)
            {
                transform.position += new Vector3(0f, 1f, 0f);
                EventManager.CallOnEndGame();
                Time.timeScale = 0;
                enabled = false;
            }
        }
    }

    private void CheckLines()
    {
        for (int y = height - 1; y >= 0; y--)
        {
            if (IsFullLine(y))
            {
                DeleteLine(y);
                DownLine(y);
            }
        }

    }
    private bool IsFullLine(int y)
    {
        for (int x = 0; x < width; x++)
        {
            if (blockCells[x, y] == null)
            {
                return false;
            }
        }
        return true;
    }

    private void DeleteLine(int y)
    {
        for (int x = 0; x < width; x++)
        {
            Destroy(blockCells[x, y].gameObject);
            blockCells[x, y] = null;
        }
    }

    private void DownLine(int y1)
    {
        for (int y = y1; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (blockCells[x, y] != null)
                {
                    blockCells[x, y - 1] = blockCells[x, y];
                    blockCells[x, y] = null;
                    blockCells[x, y - 1].transform.position -= new Vector3(0, 1, 0);
                }
            }
        }
    }

    private void AddBlockCell()
    {
        foreach (Transform item in transform)
        {
            int posX = Mathf.RoundToInt(item.transform.position.x);
            int posY = Mathf.RoundToInt(item.transform.position.y);

            blockCells[posX, posY] = item;
        }
    }

    private bool ValidMove()
    {
        foreach (Transform item in transform)
        {
            int posX = Mathf.RoundToInt(item.transform.position.x);
            int posY = Mathf.RoundToInt(item.transform.position.y);

            if (posX < 0 || posX >= width || posY < 0 /*|| posY >= height*/)
            {
                return false;
            }          

            if (blockCells[posX, posY] != null)
            {          
                return false;
            }
        }
        return true;
    }
}
