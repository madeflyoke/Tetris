using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GamePlayUIController : MonoBehaviour
{
    [SerializeField] private Text scoreUI;
    [SerializeField] private Transform nextBlockUI;
    private List<GameObject> blocksUI = new List<GameObject>();
    private int currentBlockIndex = 1; //start with 2nd block (1st index)
    private GameObject prevBlock;//cache previous ui block
    public void Initialize()
    {
        EventManager.pointsAddEvent += PointsAddUI;
        EventManager.nextSpawnBlockEvent += ChangeNextBlockUI;
        EventManager.CallOnInitBlocksUI(blocksUI, nextBlockUI);
        prevBlock = blocksUI[0];
    }
    private void OnDisable()
    {
        EventManager.pointsAddEvent -= PointsAddUI;
        EventManager.nextSpawnBlockEvent -= ChangeNextBlockUI;
    }

    private void PointsAddUI(int points)
    {
        scoreUI.text = "SCORE:" + points.ToString();
    }
    private void ChangeNextBlockUI()
    {
        if (currentBlockIndex == blocksUI.Count)
            currentBlockIndex = 0;

        blocksUI[currentBlockIndex].SetActive(true);
        prevBlock.SetActive(false);
        prevBlock = blocksUI[currentBlockIndex];
        currentBlockIndex++;
    }

    public void RefreshGameplayUI()
    {
        foreach (GameObject item in blocksUI)
        {
            item.SetActive(false);
        }
        scoreUI.text = "SCORE:0";
        prevBlock = blocksUI[0];
        currentBlockIndex = 1;
    }
}
