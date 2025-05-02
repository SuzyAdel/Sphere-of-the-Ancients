using UnityEngine;

public class ShardSpawner : MonoBehaviour
{
    
    public GameObject[] shardPrefabs;  // The array of shard prefabs to randomly spawn
    public Transform player; 
    public float spawnHeight = 40f;// Fixed Y spawn height for the shards (can be adjusted in the Inspector)
    public float spawnRateMin = 0.1f;  // Minimum time between spawns 
    public float spawnRateMax = 0.5f; // Maximum time between spawns 

    // Probability for shard spawning (25%)
    private float spawnProbability = 0.25f;
    public bool isSpawning = false;

    void Start()
    {
        // Start spawning after a short delay
        Invoke(nameof(StartSpawning), 5f);
    }

    void StartSpawning()
    {
        isSpawning = true;
        // Repeatedly attempt to put shards in the world every 5 seconds
        InvokeRepeating(nameof(AttemptSpawnShard), 0f,5f);
    }

    // Attempt to spawn a shard based on a random 25% chance
    void AttemptSpawnShard()
    {
        if (!isSpawning || shardPrefabs.Length == 0 || player == null)
            return;

        // Generate a random number (0-1) to determine spawn chance
        float randomValue = Random.value;

        // If random chance <= spawn probability, spawn the shard
        if (randomValue <= spawnProbability)
        {
            SpawnShard();
        }
    }

    // Handles the actual spawning of the shard at a random position
    void SpawnShard()
    {
        // Randomly select a shard prefab from the list
        GameObject shardPrefab = shardPrefabs[Random.Range(0, shardPrefabs.Length)];

        // Calculate random spawn position relative to the player
        Vector3 forward = player.forward.normalized;
        Vector3 spawnOffset = forward * Random.Range(30f, 40f);  // Z axis offset
        spawnOffset += player.right * Random.Range(-10f, 10f);   // X axis offset

        Vector3 spawnPosition = player.position + spawnOffset;  // Final position
        spawnPosition.y = spawnHeight; // Keep Y axis fixed at spawnHeight

        // Instantiate the shard at the calculated position with no rotation
        GameObject shard = Instantiate(shardPrefab, spawnPosition, Quaternion.identity);

        // Apply random torque again for realistic falling behavior (if the shard has a Rigidbody)
        Rigidbody rb = shard.GetComponent<Rigidbody>();
        if (rb != null)
        {
            Vector3 randomTorque = new Vector3(
                Random.Range(-200f, 200f),
                Random.Range(-200f, 200f),
                Random.Range(-200f, 200f)
            );
            rb.AddTorque(randomTorque);
        }

        // Debugging output for spawn position (you can remove this in production)
        //Debug.Log($"Spawning shard at position: {spawnPosition}");
    }
}
