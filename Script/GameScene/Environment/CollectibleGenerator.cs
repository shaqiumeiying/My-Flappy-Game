using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CollectibleGenerator : MonoBehaviour
{
    public GameObject[] collectiblePrefabs;
    public Vector2 spawnRange;
    public float destroyDelay = 4f;
    private GameObject spawnedCollectible;

    private Dictionary<string, int> collectibleCounts = new Dictionary<string, int>();

    void OnEnable()
    {
        Score.OnScoreThresholdReached += GenerateCollectible;
        LoadCollectibleCounts();
    }

    void OnDisable()
    {
        Score.OnScoreThresholdReached -= GenerateCollectible;
        SaveCollectibleCounts();
    }

    void GenerateCollectible()
    {
        Vector3 spawnPosition = GetOffScreenPosition();
        GameObject collectiblePrefab = collectiblePrefabs[Random.Range(0, collectiblePrefabs.Length)];
        spawnedCollectible = Instantiate(collectiblePrefab, spawnPosition, Quaternion.identity);

        // Start a coroutine to rotate the collectible
        StartCoroutine(ZoomInOut());

        string collectibleName = collectiblePrefab.name;
        if (collectibleCounts.ContainsKey(collectibleName))
        {
            collectibleCounts[collectibleName]++;
        }
        else
        {
            collectibleCounts[collectibleName] = 1;
        }

        Debug.Log($"Generated {collectibleName}: {collectibleCounts[collectibleName]} times");

        StartCoroutine(DestroyCollectibleAfterDelay(destroyDelay));
    }

   IEnumerator ZoomInOut()
{
    while (true)
    {
        // Check if spawnedCollectible is null
        if (spawnedCollectible == null)
        {
            yield break; // Exit the coroutine if the collectible has been destroyed
        }

        // Zoom in
        float elapsedTime = 0f;
        Vector3 startScale = spawnedCollectible.transform.localScale;
        Vector3 targetScale = startScale * 1.2f; // Zoom factor

        while (elapsedTime < 0.5f) // Duration for zooming in
        {
            // Check if spawnedCollectible is null
            if (spawnedCollectible == null)
            {
                yield break; // Exit the coroutine if the collectible has been destroyed
            }

            spawnedCollectible.transform.localScale = Vector3.Lerp(startScale, targetScale, elapsedTime / 0.5f);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Zoom out
        elapsedTime = 0f;
        while (elapsedTime < 0.5f) // Duration for zooming out
        {
            // Check if spawnedCollectible is null
            if (spawnedCollectible == null)
            {
                yield break; // Exit the coroutine if the collectible has been destroyed
            }

            spawnedCollectible.transform.localScale = Vector3.Lerp(targetScale, startScale, elapsedTime / 0.5f);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
}


    Vector3 GetOffScreenPosition()
    {
        // Get the main camera
        Camera mainCamera = Camera.main;
        float cameraHeight = 2f * mainCamera.orthographicSize;
        float cameraWidth = cameraHeight * mainCamera.aspect;

        // Determine spawn position just outside the right side of the screen
        float spawnPosX = mainCamera.transform.position.x + cameraWidth / 2 + spawnRange.x;
        float spawnPosY = Random.Range(-0.3f, 0.3f); // Randomize the spawn position on the y-axis

        return new Vector3(spawnPosX, spawnPosY, 0);
    }

    IEnumerator DestroyCollectibleAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (spawnedCollectible != null)
        {
            Destroy(spawnedCollectible);
        }
    }

    void SaveCollectibleCounts()
    {
        foreach (var pair in collectibleCounts)
        {
            PlayerPrefs.SetInt(pair.Key, pair.Value);
            Debug.Log($"Saved {pair.Key}: {pair.Value}");
        }
        PlayerPrefs.Save();
    }

    void LoadCollectibleCounts()
    {
        collectibleCounts.Clear();
        foreach (GameObject prefab in collectiblePrefabs)
        {
            string collectibleName = prefab.name;
            if (PlayerPrefs.HasKey(collectibleName))
            {
                collectibleCounts[collectibleName] = PlayerPrefs.GetInt(collectibleName);
                Debug.Log($"Loaded {collectibleName}: {collectibleCounts[collectibleName]}");
            }
            else
            {
                collectibleCounts[collectibleName] = 0;
                Debug.Log($"Initialized {collectibleName} count to 0");
            }
        }
    }

    void OnApplicationQuit()
    {
        SaveCollectibleCounts();
    }
}
