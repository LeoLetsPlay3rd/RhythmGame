using UnityEngine;

public class NodePressHandler : MonoBehaviour
{
    public float startTime;
    public float length;
    public AudioClip audioClip;

    private bool activated = false;

    void Update()
    {
        // Check if the node needs to be activated based on the audio clip time
        if (!activated && AudioSettings.dspTime >= startTime)
        {
            activated = true;
            // Activate the node
            // You can add any visual effects or audio cues here
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Check if the player pressed the key within the node's duration
            if (AudioSettings.dspTime >= startTime && AudioSettings.dspTime <= startTime + length)
            {
                // Node was hit within the correct time window
                // Perform scoring or other game logic here
            }
        }
    }
}
