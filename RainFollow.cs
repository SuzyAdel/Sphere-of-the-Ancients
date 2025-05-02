using UnityEngine;

public class RainFollow : MonoBehaviour
{
    public Transform target;     // The character to follow
    public Vector3 offset = new Vector3(0, 10, 0); // Position offset for the rain

    void LateUpdate()
    {
        if (target != null)
        {
            transform.position = target.position + offset;
        }
    }
}
