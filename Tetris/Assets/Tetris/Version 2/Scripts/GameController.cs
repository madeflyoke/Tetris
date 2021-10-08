using UnityEngine;
using UnityEngine.SceneManagement;

public enum Direction
{
    Left,
    Right,
    Down,
    Rotation,
    ShiftDown
}

public class GameController : MonoBehaviour
{
    [SerializeField] private float fallTime = 0.9f;
    [SerializeField] private float shiftFallTime = 10f;
    [SerializeField] private int pointsByLine;
    private float currentSpeed;
    private float prevTime;
    private RepositoryBase repositoryBase;
    private SpawnController spawnController;
    private Transform[,] blockCells;

    private void OnEnable()
    {
        EventManager.blockOnGroundEvent += SpawnBlock;
        EventManager.inputButtonEvent += InputHolder;
        EventManager.restartGameEvent += RestartGame;
        EventManager.exitGameEvent += ExitGame;
    }

    private void OnDisable()
    {
        EventManager.blockOnGroundEvent -= SpawnBlock;
        EventManager.inputButtonEvent -= InputHolder;
        EventManager.restartGameEvent -= RestartGame;
        EventManager.exitGameEvent -= ExitGame;


    }
    public void Initialize(RepositoryBase repositoryBase, SpawnController spawnController)
    {
        this.repositoryBase = repositoryBase;
        this.spawnController = spawnController;
        blockCells = repositoryBase.FieldInfo.blockCells;
        currentSpeed = fallTime;
    }

    private void Start()
    {
        spawnController.SpawnNewBlock();
    }

    private void Update()
    {
        if (Time.time > prevTime + currentSpeed)
        {
            EventManager.CallOnBlockMove(Direction.Down);
            prevTime = Time.time;
        }
    }

    private void InputHolder(Direction dir)
    {
        switch (dir)
        {
            case Direction.Left:
                EventManager.CallOnBlockMove(Direction.Left);
                break;
            case Direction.Right:
                EventManager.CallOnBlockMove(Direction.Right);
                break;
            case Direction.Down:
                {
                    currentSpeed = fallTime;
                    break;
                }
            case Direction.Rotation:
                EventManager.CallOnBlockMove(Direction.Rotation);
                break;
            case Direction.ShiftDown:
                {
                    currentSpeed = fallTime / shiftFallTime;
                    break;
                }
        }
    }

    private void SpawnBlock()
    {
        CheckFullLines();
        if (!EndLine())
        {
            spawnController.SpawnNewBlock();
        }
        else
        {           
            repositoryBase.PlayerPrefsInfo.SaveMaxScore();
            EventManager.CallOnEndGame(repositoryBase);
        }
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

    private void CheckFullLines()
    {
        for (int y = repositoryBase.FieldInfo.fieldHeight - 1; y >= 0; y--)
        {
            if (IsFullLine(y))
            {
                DeleteLine(y);
                repositoryBase.PlayerPrefsInfo.CurrentScore += pointsByLine;
                EventManager.CallOnPointsAdd(repositoryBase.PlayerPrefsInfo.CurrentScore);
            }
        }

    }
    private bool IsFullLine(int y)
    {
        for (int x = 0; x < repositoryBase.FieldInfo.fieldWidth; x++)
        {
            if (blockCells[x, y] == null)
            {
                return false;
            }
        }
        return true;
    }

    private void DeleteLine(int y1)
    {
        for (int x = 0; x < repositoryBase.FieldInfo.fieldWidth; x++) //delete line
        {
            Destroy(blockCells[x, y1].gameObject);
            blockCells[x, y1] = null;
        }

        for (int y = y1; y < repositoryBase.FieldInfo.fieldHeight; y++) //down line
        {
            for (int x = 0; x < repositoryBase.FieldInfo.fieldWidth; x++)
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
    private void RestartGame()
    {
        SceneManager.LoadSceneAsync(0);
    }
    private void ExitGame()
    {
        Application.Quit();
    }
}
