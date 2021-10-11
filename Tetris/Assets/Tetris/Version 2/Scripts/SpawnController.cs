using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SpawnController : MonoBehaviour
{
    [SerializeField] private List<BlockController> blockPrefabs;
    private List<GameObject> blocksUIRef;
    private Queue<BlockController> blocksQue;
    private RepositoryBase repositoryBase;
    public void Initialize(RepositoryBase repositoryBase)
    {
        EventManager.initSpawnerBlocksUIEvent += InitUIBlocks;
        transform.position = new Vector3(repositoryBase.FieldInfo.fieldWidth / 2, repositoryBase.FieldInfo.fieldHeight+1, 0); //upper position for spawner 
        this.repositoryBase = repositoryBase;
        blocksQue = new Queue<BlockController>(blockPrefabs.Count);
    }
    private void OnDisable()
    {
        EventManager.initSpawnerBlocksUIEvent -= InitUIBlocks;
    }
    public void SpawnNewBlock()
    {
        if (blocksQue.Count==0)
        {
            foreach (BlockController item in blockPrefabs) //pool
            {
                blocksQue.Enqueue(item);
            }
        }
        BlockController blockController = Instantiate(blocksQue.Dequeue(), transform, false);
        blockController.Initialize(repositoryBase);
        EventManager.CallOnNextSpawnBlock();

        if (blocksQue.Count == 1) //prepare blocks
        {
            ShuffleBlocks();
        }
    }
    private void ShuffleBlocks()
    {
        var lastblock = blockPrefabs[blockPrefabs.Count-1];
        for (int i = 0; i < blockPrefabs.Count; i++)
        {
            int j = Random.Range(0, blockPrefabs.Count);

            var tmp = blockPrefabs[j]; //shuffle game blocks
            blockPrefabs[j] = blockPrefabs[i];
            blockPrefabs[i] = tmp;

            var tmp2 = blocksUIRef[j]; //sync UI blocks
            blocksUIRef[j] = blocksUIRef[i];
            blocksUIRef[i] = tmp2;
        }
        if (lastblock == blockPrefabs[0]) // if last and first next blocks are equal
        {
            var tmp = blockPrefabs[0];
            blockPrefabs[0] = blockPrefabs[blockPrefabs.Count - 1];
            blockPrefabs[blockPrefabs.Count - 1] = tmp;

            var tmp2 = blocksUIRef[0];
            blocksUIRef[0] = blocksUIRef[blocksUIRef.Count - 1];
            blocksUIRef[blocksUIRef.Count - 1] = tmp2;
        }
    }

    private void InitUIBlocks(List<GameObject> initBlocks, Transform holder)
    {
        foreach (BlockController item in blockPrefabs)
        {
            GameObject block = Instantiate(item.gameObject, holder, true);
            initBlocks.Add(block);
            block.transform.position = holder.position;
            block.transform.position -= item.CenterPoint;
            block.GetComponent<BlockController>().enabled = false;
            block.SetActive(false);
        }
        blocksUIRef = initBlocks;
        ShuffleBlocks();
    }
    public void RefreshSpawn()
    {
        foreach (Transform item in transform)
        {
            Destroy(item.gameObject);
        }
        blocksQue = new Queue<BlockController>(blockPrefabs.Count);
        ShuffleBlocks();
    }

}
