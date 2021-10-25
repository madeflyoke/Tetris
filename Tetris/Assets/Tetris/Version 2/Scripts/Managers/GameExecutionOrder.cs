using UnityEngine;

public enum GameState
{
    Launch,
    Gameplay,
    End,
    Restart,
    Exit
}
public class GameExecutionOrder : MonoBehaviour //strict order
{
    RepositoryBase repositoryBase;
    GridCreator gridCreator;
    SpawnController spawnController;
    GamePlayUIController gamePlayUIController;
    GameController gameController;
    private void OnEnable()
    {
        EventManager.changeGameStateEvent += GameStateChange;
    }
    private void OnDisable()
    {
        EventManager.changeGameStateEvent -= GameStateChange;
    }

    private void Awake()
    {
        repositoryBase = FindObjectOfType<RepositoryBase>();
        gridCreator = FindObjectOfType<GridCreator>();
        spawnController = FindObjectOfType<SpawnController>();
        gamePlayUIController = FindObjectOfType<GamePlayUIController>();
        gameController = FindObjectOfType<GameController>();         
    }
    private void Start()
    {
        EventManager.CallOnChangeGameState(GameState.Launch);
    }
    private void GameStateChange(GameState gamestate)
    {
        switch (gamestate)
        {
            case GameState.Launch:
                PreInitialize();
                break;
            case GameState.Gameplay:
                Initialize();
                break;
            case GameState.Restart:
                RestartInitialize();
                break;
        }
    }
    private void PreInitialize()
    {
        repositoryBase.Initialize();
        spawnController.Initialize(repositoryBase);
        gamePlayUIController.Initialize();
    }
    private void Initialize()
    {
        gridCreator.Initialize(repositoryBase);
        gameController.Initialize(repositoryBase, spawnController);
    }

    private void RestartInitialize()
    {
        repositoryBase.Initialize();
        spawnController.RefreshSpawn();
        gamePlayUIController.RefreshGameplayUI();
        gameController.Initialize(repositoryBase,spawnController);
    }


}
