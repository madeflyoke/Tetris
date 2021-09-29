using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour
{
    [SerializeField] private Vector3 rotationPoint;
    private int width;
    private int height;
    private Transform[,] blockCells;
    

    private void Awake()
    {
        CheckEndLine();
    }

    public void Initialize(RepositoryBase repos)
    {
        width = repos.FieldInfo.fieldWidth;
        height = repos.FieldInfo.fieldHeight;
        blockCells = repos.FieldInfo.blockCells;
    }

    private void OnEnable()
    {
        EventManager.blockMoveEvent += MoveBlock;
    }

    private void OnDisable()
    {
        EventManager.blockMoveEvent -= MoveBlock;
    }

    private void MoveBlock(Direction dir)
    {
        switch (dir)
        {
            case Direction.Left:
                transform.position += new Vector3(-1f, 0f, 0f);
                if (!ValidMove())
                {
                    transform.position -= new Vector3(-1f, 0f, 0f);
                }
                break;
            case Direction.Right:
                transform.position += new Vector3(1f, 0f, 0f);
                if (!ValidMove())
                {
                    transform.position -= new Vector3(1f, 0f, 0f);
                }
                break;
            case Direction.Down:
                transform.position += new Vector3(0f, -1f, 0f);
                if (!ValidMove())
                {
                    transform.position -= new Vector3(0f, -1f, 0f);
                    AddBlockCell();
                    CheckLines();
                    enabled = false;
                    EventManager.CallOnBlockOnGround();
                }
                break;
            case Direction.Rotation:
                transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), 90f);
                if (!ValidMove())
                {
                    transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), -90f);
                }
                break;
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
