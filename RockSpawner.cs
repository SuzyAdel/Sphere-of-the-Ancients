
using UnityEngine;

public class RockSpawner : MonoBehaviour
{
    public GameObject[] rockPrefabs; // 3 rock prefabs assigned in Inspector
    public Transform player;         // Reference to the player object
    public float spawnHeight = 40f;  // Fixed Y spawn height
    public bool isSpawning = false;
    public Transform cam;
    void Start()
    {
        Invoke(nameof(StartSpawning), 5f); // Start spawning after 5 seconds
    }

    void StartSpawning()
    {
        isSpawning = true;
        InvokeRepeating(nameof(SpawnRock), 0f, Random.Range(0.1f, 0.5f)); // fixed frequency from 0.1 to 0.5 seconds
    }

    void SpawnRock()
    {
        if (!isSpawning || rockPrefabs.Length == 0 || player == null)
            return;

        // Randomly select a rock prefab
        GameObject rockPrefab = rockPrefabs[Random.Range(0, rockPrefabs.Length)];

        // Calculate spawn position relative to player
        //Vector3 forward = player.forward.normalized;
        Vector3 spawnOffset = new Vector3(0,0,Random.Range(30f, 40f)); // Random Z
        spawnOffset += Vector3.right * Random.Range(-10f, 10f); // Random X
        spawnOffset = Quaternion.AngleAxis(cam.eulerAngles.y, Vector3.up) * spawnOffset; // Rotate spawn position based on camera angle
        Vector3 spawnPosition = player.position + spawnOffset; 
        spawnPosition.y = spawnHeight; // Fixed Y 40 or can be changed from the inspector

        // Instantiate rock with no rotation but with random torque 
        GameObject rock = Instantiate(rockPrefab, spawnPosition, Quaternion.identity);

        // Add random torque for natural falling behavior
        Rigidbody rb = rock.GetComponent<Rigidbody>();
        if (rb != null)
        {
            Vector3 randomTorque = new Vector3(
                Random.Range(-200f, 200f),
                Random.Range(-200f, 200f),
                Random.Range(-200f, 200f)
            );
            // direct rotation sets the object's orientation immediately without the need for forces, this keeps natural falling behavior
            rb.AddTorque(randomTorque);
        }
    }
}


