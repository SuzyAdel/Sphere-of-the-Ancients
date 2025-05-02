using UnityEngine;
using System.Collections;

public class MagicSphereMovement : MonoBehaviour
{
    // Movement settings for normal walking and sprinting
    [Header("Movement Settings")]
    public float walkSpeed = 50f;                // Speed applied when walking
    public float sprintSpeed = 100f;             // Speed applied when sprinting
    public float maxAngularVelocity = 100f;     // Max angular velocity to limit extreme spinning

    // Jump settings: forces for jumping while walking or sprinting
    [Header("Jump Settings")]
    public float jumpForce = 50f;               // Jump force when walking
    public float sprintJumpForce = 100f;        // Jump force when sprinting
    public LayerMask groundMask;                // Layer mask to detect ground objects
    private bool isGrounded = false;            // Boolean to check if the sphere is grounded

    // Shield-related settings
    [Header("Shield Settings")]
    public GameObject[] shields;               // An array of shield GameObjects attached to the player
    public Material shieldMaterial;            // Material to change the shield color (red/blue)
    private Material[] shieldMaterials;        // Stores materials of shields for color change
    private bool isImmune = false;              // Track whether the player is immune to damage
    public float shieldImmunityDuration = 5f;   // Duration for immunity after a hit
    public float shieldStrength = 3f;
    public float maxShieldStrength = 5f;
    
    private Rigidbody rb;                       // Reference to the Rigidbody for applying forces
    private Camera mainCamera;                  // Reference to the main camera for movement direction

    //flag of disabbled 
    private bool inputDisabled = false;
    public int shieldCounter = 29;


    // Called when the script is first initialized
    void Start()
    {
        // Get the Rigidbody component from the GameObject
        rb = GetComponent<Rigidbody>();

        // Get the main camera in the scene
        mainCamera = Camera.main;

        // Check if the camera is not found and log an error if so
        if (mainCamera == null)
            Debug.LogError("No main camera found. Please tag your camera as 'MainCamera'");

        // Set the maximum angular velocity to prevent unrealistic spinning
        rb.maxAngularVelocity = maxAngularVelocity;

        // Initialize shield materials for color change
        shieldMaterials = new Material[shields.Length];
        for (int i = 0; i < shields.Length; i++)
        {
            shieldMaterials[i] = shields[i].GetComponent<Renderer>().material;
        }
        
    }

    // Called once per frame to handle player input and jumping
    void Update()
    {
        // Check if the player pressed the Jump button and if they are grounded
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            // Decide which jump force to apply based on whether LeftShift is held (sprint)
            float force = Input.GetKey(KeyCode.LeftShift) ? sprintJumpForce : jumpForce;

            // Apply an upward force to the Rigidbody to simulate the jump
            rb.AddForce(Vector3.up * force, ForceMode.Impulse);

            // Set the grounded status to false until the player lands
            isGrounded = false;
        }
        
    }

    // Called at a fixed interval to handle physics-based movement
    void FixedUpdate()
    {
        // Call the method to apply movement torque to the Rigidbody
        if(!inputDisabled)      
            ApplyMovementTorque();
    }

    public void DisableInput()
    {
        inputDisabled = true;
    }

    // Method that applies movement force (torque) based on user input
    void ApplyMovementTorque()
    {
        // Get input values for horizontal and vertical movement (WASD or Arrow keys)
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // If there's no movement input, skip applying any torque
        if (horizontal == 0 && vertical == 0) return;

        // Create a normalized direction vector based on input values (no diagonal speed boost)
        Vector3 inputDir = new Vector3(horizontal, 0, vertical).normalized;

        // Get the camera's forward and right directions, ignoring vertical axis (y)
        Vector3 camForward = mainCamera.transform.forward;
        Vector3 camRight = mainCamera.transform.right;

        // Normalize the camera directions to avoid non-unit vectors
        camForward.y = 0;
        camRight.y = 0;
        camForward.Normalize();
        camRight.Normalize();

        // Calculate the final movement direction based on the camera's orientation
        Vector3 moveDir = (camForward * vertical + camRight * horizontal).normalized;

        // Calculate the axis of torque (we swap X and Z axes for proper rolling effect)
        Vector3 torqueAxis = new Vector3(moveDir.z, 0, -moveDir.x);

        // Choose the movement speed based on whether LeftShift is held (for sprinting)
        float speed = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : walkSpeed;

        // Apply torque to the Rigidbody to simulate rolling in the movement direction
        rb.AddTorque(torqueAxis * speed);
    }

    // Called when the sphere stays in contact with any colliders
    private void OnCollisionStay(Collision collision)
    {
        // Check if the collision object is part of the ground layer
        if (((3 << collision.gameObject.layer) & groundMask) != 0) // reassured that the terrian layer is ground and set it to 3 
        {
            // Iterate through the collision contacts to check if any are pointing upwards (indicating ground)
            foreach (ContactPoint contact in collision.contacts)
            {
                if (contact.normal.y > 0.5f)
                {
                    // Set the sphere as grounded if the collision normal is mostly upwards
                    isGrounded = true;
                    break; // Exit the loop once we confirm that the sphere is grounded
                }
            }
        }
    }

    // Called when the sphere exits contact with a collider
    private void OnCollisionExit(Collision collision)
    {
        // If the sphere stops touching the ground layer, mark it as not grounded
        if (((3 << collision.gameObject.layer) & groundMask) != 0)
        {
            isGrounded = false;
        }
    }

    // Method to handle shield damage and immunity
    public void TakeDamage()
    {
        if (isImmune) return; // If immune, ignore damage

        // Remove the outermost shield
        if (shields.Length > 0)
        {
            GameObject shieldToRemove = shields[0];
            foreach (var shield in shields)
            {
                if (shield.activeSelf)
                {
                    shieldToRemove = shield;
                    break;
                }
            }
            shieldToRemove.SetActive(false);
            shieldCounter = 0;// Reset shield counter to count activated shields
            foreach (var shield in shields)
            {
                if (shield.activeSelf)
                {
                    shieldCounter++;
                }
            }
            //shields = System.Array.FindAll(shields, shield => shield != shieldToRemove); // Update shields array
        }

        // Turn the remaining shields red and activate immunity
        StartCoroutine(ActivateImmunity());
    }

    // Coroutine for immunity and visual shield feedback
    private IEnumerator ActivateImmunity()
    {
        isImmune = true;

        // Turn remaining shields red
        foreach (var shield in shields)
        {
            shieldMaterials[System.Array.IndexOf(shields, shield)].color = Color.red;
        }

        yield return new WaitForSeconds(shieldImmunityDuration); // Wait for immunity duration

        // After 5 seconds, revert shields to blue
        foreach (var shield in shields)
        {
            shieldMaterials[System.Array.IndexOf(shields, shield)].color = Color.blue;
        }

        isImmune = false; // Disable immunity after 5 seconds
    }
    public void RestoreShield(float amount)
    {
        shieldStrength = Mathf.Min(shieldStrength + amount, maxShieldStrength);
        foreach (var shield in shields)
        {
            if (!shield.activeSelf)
            {
                shield.SetActive(true); // Activate the shield if it's not already active
                break;// to return only one shield  
            }
        }
        shieldCounter = 0;
        foreach (var shield in shields)
        {
            if (shield.activeSelf)
            {
                shieldCounter++;
            }
        }
        Debug.Log("Shard collected! Shield: " + shieldStrength);
    }

}
