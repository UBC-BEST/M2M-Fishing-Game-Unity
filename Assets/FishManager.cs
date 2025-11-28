using UnityEngine;
using System.Collections.Generic;

public class FishManager : MonoBehaviour
{
    [Header("Fish Prefabs")]
    public GameObject fishShallow;
    public GameObject fishMidShallow;
    public GameObject fishMidDeep;
    public GameObject fishDeep;

    [Header("Spawn X Range")]
    public float minX = -7f;
    public float maxX = 7f;

    [Header("Fixed Depth Levels (will be shuffled)")]
    public float depth1 = 0f;      // Shallowest
    public float depth2 = -2f;
    public float depth3 = -4f;
    public float depth4 = -10f;    // Deepest

    private GameObject shallowInstance;
    private GameObject midShallowInstance;
    private GameObject midDeepInstance;
    private GameObject deepInstance;

    private bool allFishDestroyed = false;

    void Start()
    {
        SpawnAllFish();
    }

    void Update()
    {
        // Check if all 4 fish are destroyed
        if (shallowInstance == null && midShallowInstance == null &&
            midDeepInstance == null && deepInstance == null)
        {
            if (!allFishDestroyed)
            {
                allFishDestroyed = true;
                Debug.Log("=== All 4 fish caught! Respawning with shuffled depths... ===");
                SpawnAllFish();
            }
        }
        else
        {
            allFishDestroyed = false;
        }
    }

    void SpawnAllFish()
    {
        // Create list of depths and shuffle them
        List<float> shuffledDepths = new List<float> { depth1, depth2, depth3, depth4 };
        ShuffleList(shuffledDepths);

        Debug.Log($"Shuffled depths: [{shuffledDepths[0]}, {shuffledDepths[1]}, {shuffledDepths[2]}, {shuffledDepths[3]}]");

        // Spawn each fish at a shuffled depth
        float x1 = Random.Range(minX, maxX);
        shallowInstance = Instantiate(fishShallow, new Vector3(x1, shuffledDepths[0], 0f), Quaternion.identity);
        UpdateFishDepth(shallowInstance, shuffledDepths[0]);
        Debug.Log($"Fish 1 (Shallow prefab) spawned at depth: {shuffledDepths[0]}");

        float x2 = Random.Range(minX, maxX);
        midShallowInstance = Instantiate(fishMidShallow, new Vector3(x2, shuffledDepths[1], 0f), Quaternion.identity);
        UpdateFishDepth(midShallowInstance, shuffledDepths[1]);
        Debug.Log($"Fish 2 (MidShallow prefab) spawned at depth: {shuffledDepths[1]}");

        float x3 = Random.Range(minX, maxX);
        midDeepInstance = Instantiate(fishMidDeep, new Vector3(x3, shuffledDepths[2], 0f), Quaternion.identity);
        UpdateFishDepth(midDeepInstance, shuffledDepths[2]);
        Debug.Log($"Fish 3 (MidDeep prefab) spawned at depth: {shuffledDepths[2]}");

        float x4 = Random.Range(minX, maxX);
        deepInstance = Instantiate(fishDeep, new Vector3(x4, shuffledDepths[3], 0f), Quaternion.identity);
        UpdateFishDepth(deepInstance, shuffledDepths[3]);
        Debug.Log($"Fish 4 (Deep prefab) spawned at depth: {shuffledDepths[3]}");
    }

    void UpdateFishDepth(GameObject fish, float newDepth)
    {
        if (fish == null) return;

        Fish1_0 fishScript = fish.GetComponent<Fish1_0>();
        if (fishScript != null)
        {
            fishScript.depth = newDepth;
            // Force position update
            Vector3 pos = fish.transform.position;
            fish.transform.position = new Vector3(pos.x, newDepth, pos.z);
        }
    }

    // Fisher-Yates shuffle algorithm
    void ShuffleList(List<float> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            float temp = list[i];
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }
}