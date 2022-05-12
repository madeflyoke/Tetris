using UnityEngine;
using UnityEngine.UI;

public class EndGameUIController : MonoBehaviour
{
    [SerializeField] private Text finalScore;
    [SerializeField] private Text maxScore;
    [SerializeField] private Button retryButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private Button resetMaxScoreButton;

    public void Initialize(RepositoryBase repos)
    {
        GetComponentInParent<Canvas>().sortingOrder = 2;
        finalScore.text = "FINAL SCORE:"+ repos.PlayerPrefsInfo.CurrentScore.ToString();
        maxScore.text = "MAX:" + repos.PlayerPrefsInfo.MaxScore.ToString();
        retryButton.onClick.AddListener(() => EventManager.CallOnChangeGameState(GameState.Restart));
        exitButton.onClick.AddListener(() => Application.Quit());
        resetMaxScoreButton.onClick.AddListener(()=> {repos.PlayerPrefsInfo.ResetMaxScore(); maxScore.text = "MAX:" + repos.PlayerPrefsInfo.MaxScore.ToString();});
    }

}
