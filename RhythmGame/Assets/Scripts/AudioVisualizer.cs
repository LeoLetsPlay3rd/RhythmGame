using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class AudioVisualizer : MonoBehaviour
{
    public AudioSource audioSource;
    public VisualEffect visualEffect;
    public float threshold = 0.5f; // Adjust this value to set the threshold for beat detection
    public float beatCooldown = 0.2f; // Adjust this value to set the cooldown between beats

    private float timer = 0f;
    private bool isBeat = false;

    void Start()
    {
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }

        if (visualEffect == null)
        {
            visualEffect = GetComponent<VisualEffect>();
        }

        // Ensure the audio source is set to loop
        audioSource.loop = true;
    }

    void Update()
    {
        if (!audioSource.isPlaying)
            return;

        timer += Time.deltaTime;

        if (timer >= beatCooldown)
        {
            float[] spectrum = new float[256];
            audioSource.GetSpectrumData(spectrum, 0, FFTWindow.BlackmanHarris);

            float sum = 0f;
            for (int i = 0; i < spectrum.Length; i++)
            {
                sum += spectrum[i];
            }

            float average = sum / spectrum.Length;

            if (average > threshold && !isBeat)
            {
                // Trigger beat event
                isBeat = true;
                visualEffect.SendEvent("OnBeat"); // Send event to the VFX Graph
            }

            if (average <= threshold && isBeat)
            {
                isBeat = false;
            }

            timer = 0f; // Reset timer
        }
    }
}
