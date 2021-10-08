using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour
{
    [SerializeField] private Vector3 rotationPoint;
    [SerializeField] private Vector3 centerPoint;
    public Vector3 CenterPoint { get => centerPoint; }

    private int width;
    private int height;
    private Transform[,] blockCells;

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
        Gizmos.color = Color.black;
        Gizmos.DrawSphere(transform.TransformPoint(centerPoint), 0.1f);

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
