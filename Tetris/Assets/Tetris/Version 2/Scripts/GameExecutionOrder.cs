using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameExecutionOrder : MonoBehaviour
{
    private void Awake()
    {
        GamePlayUIController uIController = FindObjectOfType<GamePlayUIController>();
        uIController.Initialize();

        RepositoryBase repositoryBase = FindObjectOfType<RepositoryBase>();
        repositoryBase.Initialize();

        GridCreator gridCreator = FindObjectOfType<GridCreator>();
        gridCreator.Initialize(repositoryBase);

        SpawnController spawnController = FindObjectOfType<SpawnController>();
        spawnController.Initialize(repositoryBase);

        GameController gameController = FindObjectOfType<GameController>();
        gameController.Initialize(repositoryBase,spawnController);
    }


}
