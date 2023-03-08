using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class ItemDropper : MonoBehaviour
{
    [SerializeField]
    private List<ItemSpawnData> itemsToDrop = new List<ItemSpawnData>();
    float[] itemWeights;

    [SerializeField]
    [Range(0f, 1f)]
    private float dropChance;

    private void Start()
    {
        itemWeights = itemsToDrop.Select(item => item.rate).ToArray();
    }

    public void DropItem()
    {
        float dropValue = Random.value;
        if (dropChance > dropValue)
        {
            int index = GetRandomWeightedIndex(itemWeights);
            Instantiate(itemsToDrop[index].itemPrefab, transform.position, Quaternion.identity);
        }
    }

    private int GetRandomWeightedIndex(float[] itemWeights)
    {
        float sum = 0f;
        for (int i = 0; i < itemWeights.Length; i++)
        {
            sum += itemWeights[i];
        }

        float randomValue = Random.Range(0, sum);
        float tempSum = 0;
        for (int i = 0; i < itemsToDrop.Count; i++)
        {
            //roulette wheel selection
            if (randomValue > tempSum && randomValue < tempSum + itemWeights[i])
            {
                return i;
            }
            tempSum += itemWeights[i];
        }
        return 0;
    }
}

[Serializable]
public struct ItemSpawnData
{
    [Range(0,1)]
    public float rate;
    public GameObject itemPrefab;
}
