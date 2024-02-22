using UnityEngine;
using UnityEngine.VFX;

public class VFXController : MonoBehaviour
{
    [SerializeField]
    private VisualEffect visualEffect;

    [SerializeField]
    private AudioFrequencyAnalyzer audioFrequencyAnalyzer;

    private void Start()
    {
        if (visualEffect == null)
        {
            Debug.LogError("Visual Effect is not assigned!");
            return;
        }

        if (audioFrequencyAnalyzer == null)
        {
            Debug.LogError("Audio Frequency Analyzer is not assigned!");
            return;
        }
    }

    private void Update()
    {
        // Access the amplitude of a particular frequency band, for example, bass amplitude
        float bassAmplitude = audioFrequencyAnalyzer.BassAmplitude;

        // Scale the amplitude value to control VFXIntensity (you might need to tweak the scale)
        float vfxIntensity = bassAmplitude * 5f; // You can adjust the scaling factor as per your requirement

        // Set the VFXIntensity parameter in the Visual Effect
        visualEffect.SetFloat("VFXIntensity", vfxIntensity);
    }
}
