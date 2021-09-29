using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SpawnController : MonoBehaviour
{
    [SerializeField] private List<BlockController>blockPrefabs;
    private Queue<BlockController> blocksPool;
    private RepositoryBase repositoryBase;

    public void Initialize(RepositoryBase repositoryBase)
    {
        this.repositoryBase = repositoryBase;
        blocksPool = new Queue<BlockController>(blockPrefabs.Count);
        ShuffleBlocks();
    }

    public void SpawnNewBlock()
    {
        if (blocksPool.Count == 0)
        {
            ShuffleBlocks();
        }
        BlockController blockController = Instantiate(blocksPool.Dequeue(), transform, false);
        blockController.Initialize(repositoryBase);
    }
    private void ShuffleBlocks()
    {
        blockPrefabs = blockPrefabs.OrderBy(x => Random.Range(0, blockPrefabs.Count - 1)).ToList();
        foreach (BlockController item in blockPrefabs)
        {
            blocksPool.Enqueue(item);
        }
    }
}
