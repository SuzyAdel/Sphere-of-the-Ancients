using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Slider shieldBar;  // Reference to the shield bar slider
    public Text shardCountText;  // Reference to the shard count text
    public GameObject player;  // Reference to the player script for health/shield data
    private MagicSphereMovement magicSphereMovement; // Reference to the MagicSphereMovement script
    private float shieldPercent; // Percentage of shield remaining
    private GUIStyle style;

    private void Start()
    {
        magicSphereMovement = player.GetComponent<MagicSphereMovement>(); // Get the MagicSphereMovement component from the player
        style = new GUIStyle() { fontSize = 30 };
        style.normal.textColor = Color.yellow;
    }

    private void OnGUI()
    {
        string txt = "Shields: " + (int)shieldPercent;
        GUI.Label(new Rect(10, 10, 200, 100), txt, style); // Display the shield count as a label
    }
    void Update()
    {
        shieldPercent = (magicSphereMovement.shieldCounter / 29f) * 100;
        // Update shield bar based on player's shield count
        /*shieldPercent = magicSphereMovement.shields.Length / 29f;  // Assuming you have max shield count defined
        shieldBar.value = shieldPercent;  // Update the slider value    

        // Update shard count UI
        shardCountText.text = "Shards: " + shieldPercent.ToString();*/
    }
}
