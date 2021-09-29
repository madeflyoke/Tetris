using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
    Left,
    Right,
    Down,
    Rotation
}

public class GameController : MonoBehaviour
{
    [SerializeField] private float fallTime = 0.9f;
    [SerializeField] private float shiftFallTime = 10f;
    private float prevTime;
    private RepositoryBase repositoryBase;//????????
    private SpawnController spawnController;
    private Transform[,] blockCells;//??

    private void OnEnable()
    {
        EventManager.blockOnGroundEvent += SpawnBlock;
    }

    private void OnDisable()
    {
        EventManager.blockOnGroundEvent -= SpawnBlock;

    }
    public void Initialize(RepositoryBase repositoryBase, SpawnController spawnController)
    {
        this.repositoryBase = repositoryBase;
        this.spawnController = spawnController;
        blockCells = repositoryBase.FieldInfo.blockCells;
    }

    private void Start()
    {
        spawnController.SpawnNewBlock();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            EventManager.CallOnBlockMove(Direction.Left);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            EventManager.CallOnBlockMove(Direction.Right);
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            EventManager.CallOnBlockMove(Direction.Rotation);
        }

        if (Time.time > prevTime + (Input.GetKey(KeyCode.S) ? fallTime / shiftFallTime : fallTime))
        {
            EventManager.CallOnBlockMove(Direction.Down);
            prevTime = Time.time;
        }

    }

    private void SpawnBlock()
    {
        if (!EndLine())
        {
            spawnController.SpawnNewBlock();
        }
        else
            Debug.Log("EndGame");

    }

    private bool EndLine()
    {
        for (int x = 0; x < repositoryBase.FieldInfo.fieldWidth; x++)
        {
            if (blockCells[x, repositoryBase.FieldInfo.fieldHeight] != null)
            {
                transform.position += new Vector3(0f, 1f, 0f);
                return true;
            }
        }
        return false;
    }
}
