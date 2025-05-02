using UnityEngine;

public class PortalLightBlinking : MonoBehaviour
{
    [Header("Light Settings")]
    public float minIntensity = 0.5f;    // Minimum light brightness
    public float maxIntensity = 2.0f;    // Maximum light brightness
    public float blinkSpeed = 1.0f;      // How fast the light blinks

    private Light portalLight;           // Reference to the Point Light

    void Start()
    {
        // Get the Point Light component (must be attached to the same GameObject)
        portalLight = GetComponent<Light>();

        if (portalLight == null)
        {
            Debug.LogError("No Light component found on the Portal!");
            enabled = false; // Disable script if no light exists
        }
    }

    void Update()
    {
        if (portalLight != null)
        {
            // Calculate a smooth oscillation between min and max intensity
            float lerpFactor = Mathf.PingPong(Time.time * blinkSpeed, 1f);
            portalLight.intensity = Mathf.Lerp(minIntensity, maxIntensity, lerpFactor);
        }
    }
}