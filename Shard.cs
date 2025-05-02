using UnityEngine;
using UnityEngine.Audio;

public class Shard : MonoBehaviour
{
    public AudioClip pickupSound;  // Shard pickup sound effect
    private AudioSource audioSource; // AudioSource to play sound

    private void OnTriggerEnter(Collider other)
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("MagicSphere"))
        {
            MagicSphereMovement magicSphere = collision.collider.GetComponent<MagicSphereMovement>();
            audioSource = collision.collider.GetComponent<AudioSource>();
            if (magicSphere != null)
            {
                magicSphere.RestoreShield(1f); // Give 1 shield
                // play sound 
                if (pickupSound != null && audioSource != null)
                {
                    audioSource.PlayOneShot(pickupSound);
                }
            }

            Destroy(gameObject); // Disappear after collection
        }
        // Ignore collisions with rocks
        if (collision.gameObject.CompareTag("Rock"))
        {
            Physics.IgnoreCollision(collision.collider, GetComponent<Collider>());
            
        }
    }
}
