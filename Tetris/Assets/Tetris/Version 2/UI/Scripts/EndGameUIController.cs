using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGameUIController : MonoBehaviour
{
    [SerializeField] private GameObject endGameScreen;
    [SerializeField] private Text finalScore;
    [SerializeField] private Text maxScore;
    [SerializeField] private Button retryButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private Button resetMaxScoreButton;
    private void OnEnable()
    {
        endGameScreen.SetActive(false);
        EventManager.endGameEvent += Initialize;
    }
    private void OnDisable()
    {
        EventManager.endGameEvent -= Initialize;
       
    }
    private void Initialize(RepositoryBase repos)
    {
        GetComponentInParent<Canvas>().sortingOrder = 2;
        endGameScreen.SetActive(true);
        finalScore.text = "FINAL SCORE:"+ repos.PlayerPrefsInfo.CurrentScore.ToString();
        maxScore.text = "MAX:" + repos.PlayerPrefsInfo.MaxScore.ToString();
        retryButton.onClick.AddListener(() => EventManager.CallOnRestartGame());
        exitButton.onClick.AddListener(() => EventManager.CallOnExitGame());
        resetMaxScoreButton.onClick.AddListener(()=> {repos.PlayerPrefsInfo.ResetMaxScore(); maxScore.text = "MAX:" + repos.PlayerPrefsInfo.MaxScore.ToString();});
    }

}
