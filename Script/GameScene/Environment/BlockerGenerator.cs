using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockerGenerator : MonoBehaviour
{
    [SerializeField] private float maxTime = 1.5f;
    [SerializeField] private float heightRange = 0.45f;
    [SerializeField] private GameObject[] easyBlockers; // Array to hold easy blocker prefabs
    [SerializeField] private GameObject[] hardBlockers; // Array to hold hard blocker prefabs
    [SerializeField] private float easyProbability = 0.6f;
    [SerializeField] private float hardProbability = 0.4f;
    private float timer;
    private float offScreenOffset = 1f;

    void Start()
    {
        SpawnBlocker();
    }

    void Update()
    {
        if (timer > maxTime)
        {
            SpawnBlocker();
            timer = 0;
        }
        timer += Time.deltaTime;
    }

    void SpawnBlocker()
    {
        GameObject selectedBlocker = GetRandomBlocker();
        Vector3 spawnPos = transform.position + new Vector3(offScreenOffset, Random.Range(-heightRange, heightRange), 0);
        GameObject blocker = Instantiate(selectedBlocker, spawnPos, Quaternion.identity);
        Destroy(blocker, 4f);
    }

    GameObject GetRandomBlocker()
    {
        float easyCumulativeProbability = Mathf.Clamp01(easyProbability);
        float hardCumulativeProbability = Mathf.Clamp01(hardProbability);

        if (easyCumulativeProbability + hardCumulativeProbability < 1)
        {
            hardCumulativeProbability += 1 - (easyCumulativeProbability + hardCumulativeProbability);
        }

        float randomValue = Random.value;

        if (randomValue <= easyCumulativeProbability)
        {
            int randomIndex = Random.Range(0, easyBlockers.Length);
            return easyBlockers[randomIndex];
        }
        else
        {
            int randomIndex = Random.Range(0, hardBlockers.Length);
            return hardBlockers[randomIndex];
        }
    }
}