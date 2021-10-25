using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private GamePlayUIController gamePlayScreen;
    [SerializeField] private EndGameUIController endGameScreen;
    [SerializeField] private StartGameUIController startGameScreen;
    private RepositoryBase repositoryBase;
    private void OnEnable()
    {
        repositoryBase = FindObjectOfType<RepositoryBase>();
        EventManager.changeGameStateEvent += ChangedGameState;

    }
    private void OnDisable()
    {
        EventManager.changeGameStateEvent -= ChangedGameState;
    }
    private void ChangedGameState(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.Launch:
                startGameScreen.gameObject.SetActive(true);
                startGameScreen.Initialize();
                break;
            case GameState.Gameplay:
                startGameScreen.gameObject.SetActive(false);
                gamePlayScreen.gameObject.SetActive(true);
                endGameScreen.gameObject.SetActive(false);
                break;
            case GameState.End:
                endGameScreen.gameObject.SetActive(true);
                endGameScreen.Initialize(repositoryBase);
                break;
            case GameState.Restart:
                endGameScreen.gameObject.SetActive(false);
                gamePlayScreen.gameObject.SetActive(true);
                this.GetComponent<Canvas>().sortingOrder = 0;
                
                break;
        }
    }

}
