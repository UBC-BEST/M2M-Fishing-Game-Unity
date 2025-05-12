using UnityEngine;

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

    private GameObject shallowInstance;
    private GameObject midShallowInstance;
    private GameObject midDeepInstance;
    private GameObject deepInstance;

    void Start()
    {
        SpawnFishIfNull(ref shallowInstance, fishShallow, -1f);
        SpawnFishIfNull(ref midShallowInstance, fishMidShallow, -2f);
        SpawnFishIfNull(ref midDeepInstance, fishMidDeep, -3f);
        SpawnFishIfNull(ref deepInstance, fishDeep, -4f);
    }

    void Update()
    {
        SpawnFishIfNull(ref shallowInstance, fishShallow, 1f);
        SpawnFishIfNull(ref midShallowInstance, fishMidShallow, 2f);
        SpawnFishIfNull(ref midDeepInstance, fishMidDeep, -3f);
        SpawnFishIfNull(ref deepInstance, fishDeep, -4f);
    }

    void SpawnFishIfNull(ref GameObject instance, GameObject prefab, float depth)
    {
        if (instance != null) return;

        float x = Random.Range(minX, maxX);
        Vector3 spawnPos = new Vector3(x, depth, 0f);

        instance = Instantiate(prefab, spawnPos, Quaternion.identity);

        Fish1_0 fishScript = instance.GetComponent<Fish1_0>();
        if (fishScript != null)
            fishScript.depth = depth;
    }
}
