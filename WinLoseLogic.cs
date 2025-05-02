using System.Collections;
using UnityEngine;
using UnityEditor;


public class GameManager : MonoBehaviour
{
    public GameObject magicSphere; // Reference to the MagicSphere script
    //public GameObject player; // Reference to the Player script

    public GameObject Spawner;
    private ShardSpawner shardSpawner; // Reference to the ShardSpawner
    private RockSpawner rockSpawner; // Reference to the RockSpawner
    public Transform portal; // Reference to the Portal transform
    public float winDistance = 100f; // Distance to portal to win the game

    private bool gameIsOver = false; // Flag to prevent multiple checks
    private MagicSphereMovement magicSphereMovement; // Reference to the MagicSphereMovement script

    public Material coreMaterial; // Reference to the core material of the Magic Sphere


    private void Start()
    {
        magicSphereMovement = magicSphere.GetComponent<MagicSphereMovement>(); // Get the MagicSphereMovement component
        shardSpawner = Spawner.GetComponent<ShardSpawner>(); // Get the ShardSpawner component
        rockSpawner = Spawner.GetComponent<RockSpawner>(); // Get the RockSpawner component 
        coreMaterial.color = new Color32(255, 198, 25, 99);

    }

    void Update()
    {
        // If the game is already over, we don’t check further
      /*  if (gameIsOver)
            return;
        
        

        // Check if the player has won
        if (HasWon())
        {
            HandleWin();
        }
        // Check if the player has lost
        else if (HasLost())
        {
            HandleLoss();
        }*/
    }

    private void FixedUpdate()
    {
        // Check if the game is over
        if (gameIsOver)
            return;
        // Check if the player has won
        if (HasWon())
        {
            HandleWin();
        }
        // Check if the player has lost
        else if (HasLost())
        {
            HandleLoss();
        }
    }

    bool HasWon()
    {
        // Check if the player is within short unit of the portal 
        return Vector3.Distance(magicSphere.transform.position, portal.position) <= winDistance;
    }

    bool HasLost()
    {
        // Check if the player has no shields left
        return magicSphereMovement.shieldCounter <= 0;
    }

    void HandleWin()
    {
        gameIsOver = true; // Set game as over

        // Set all remaining shields to green
        foreach (var shield in magicSphereMovement.shields) // Assuming you have a list of shields in your player script
        {
            shield.GetComponent<Renderer>().material.color = Color.green;
        }
        // Freeze the Magic Sphere
        magicSphere.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        // Make the Magic Sphere ascend indefinitely and rotate
        StartCoroutine(AscendAndRotateMagicSphere());

        // Stop spawning rocks and shards
        if (rockSpawner != null) rockSpawner.isSpawning = false;
        if (shardSpawner != null) shardSpawner.isSpawning = false;

        // Disable player input
        magicSphereMovement.DisableInput();
    }

    void HandleLoss()
    {
        gameIsOver = true; // Set game as over

        // Turn Magic Sphere’s core to black
        coreMaterial.color = Color.black;

        // Freeze the Magic Sphere
        magicSphere.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;

        // Stop spawning rocks and shards
        if (rockSpawner != null) rockSpawner.enabled = false;
        if (shardSpawner != null) shardSpawner.enabled = false;

        // Disable player input
        magicSphereMovement.DisableInput();
    }

        // Coroutine to handle the ascension and rotation of the Magic Sphere
        IEnumerator AscendAndRotateMagicSphere()
    {
        while (true) // This will make it ascend and rotate indefinitely
        {
            magicSphere.transform.Translate(Vector3.up * 1f * Time.deltaTime, Space.World); // Ascend
            magicSphere.transform.Rotate(Vector3.up * 30f * Time.deltaTime); // Rotate on Y axis
            magicSphere.transform.Rotate(Vector3.right * 20f * Time.deltaTime); // Rotate on X axis
            magicSphere.transform.Rotate(Vector3.forward * 10f * Time.deltaTime); // Rotate on Z axis
            yield return null; // Wait for the next frame
        }
    }
}