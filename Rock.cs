using UnityEngine;

public class Rock : MonoBehaviour
{
    public AudioClip impactSound;  // Rock impact sound effect
    private AudioSource audioSource; // AudioSource to play sound
    private Rigidbody rb;

    [Header("Rock Settings")]
    public float velocityThreshold = 0.1f; // Rock is destroyed if it stops moving

    [Header("Player Reference")]
    public MagicSphereMovement magicSphere; // Drag & drop your player (MagicSphere) GameObject here in Inspector

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Optional: Auto-find if not set in Inspector
        if (magicSphere == null)
        {
            GameObject playerObj = GameObject.FindWithTag("MagicSphere");
            if (playerObj != null)
                magicSphere = playerObj.GetComponent<MagicSphereMovement>();
        }
    }

    void Update()
    {
        // Destroy rock if it has stopped moving
        if (rb.linearVelocity.magnitude < velocityThreshold)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Shield"))
        {
           /* // Destroy the shield it hits, this will make it permenantly lost 
            // we can replace it with setActive(false) if we want to make it temporary
            collision.gameObject.SetActive(false);
            magicSphere.shieldCounter = 0;
            foreach (var shield in magicSphere.shields)
            {
                if (shield.activeSelf)
                {
                    magicSphere.shieldCounter++;
                }
            }
            // Play impact sound
            if (impactSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(impactSound);
            }*/
        }
        else if (collision.gameObject.CompareTag("MagicSphere") && rb.linearVelocity.magnitude > velocityThreshold)
        {
            // Rock hits player while moving — deal damage
            if (magicSphere != null)
            {
                audioSource = collision.collider.GetComponent<AudioSource>();
                magicSphere.TakeDamage();
                // Play impact sound
                if (impactSound != null && audioSource != null)
                {
                    audioSource.PlayOneShot(impactSound);
                }
            }
        }
    }
}
