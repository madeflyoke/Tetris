using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SpawnController : MonoBehaviour
{
    [SerializeField] private List<GameObject> blockPrefabs;
    private Queue<GameObject> blocksPool = new Queue<GameObject>(7);
    private void OnEnable()
    {
        EventManager.blockOnGroundEvent += SpawnNewBlock;
    }

    private void OnDisable()
    {
        EventManager.blockOnGroundEvent -= SpawnNewBlock;

    }
    private void Start()
    {
        foreach (GameObject item in blockPrefabs)
        {
            blocksPool.Enqueue(item);
        }
        SpawnNewBlock();
    }

    private void SpawnNewBlock()
    {
        if (blocksPool.Count == 0)
        {
            blockPrefabs = blockPrefabs.OrderBy(x => Random.Range(0, blockPrefabs.Count - 1)).ToList();
            foreach (GameObject item in blockPrefabs)
            {
                blocksPool.Enqueue(item);
            }
        }
        Instantiate(blocksPool.Dequeue(), transform, false);
    }
}
