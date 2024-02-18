using UnityEngine;

public class AudioFrequencyAnalyzer : MonoBehaviour
{
    public AudioSource audioSource;
    public float[] frequencyRanges; // Frequency ranges for each band

    void Start()
    {
        if (audioSource == null)
        {
            Debug.LogError("Audio source is not assigned!");
            return;
        }

        if (frequencyRanges.Length != 7)
        {
            Debug.LogError("Invalid number of frequency ranges. There should be 7 ranges.");
            return;
        }

        // Start playing the audio clip
        audioSource.Play();
    }

    void Update()
    {
        // Ensure the audio clip is playing
        if (!audioSource.isPlaying)
            return;

        // Get spectrum data from the audio source
        float[] spectrumData = new float[1024];
        audioSource.GetSpectrumData(spectrumData, 0, FFTWindow.BlackmanHarris);

        // Calculate amplitudes for each frequency band
        float subBassAmplitude = CalculateAmplitude(spectrumData, 20f, 60f);
        float bassAmplitude = CalculateAmplitude(spectrumData, 60f, 250f);
        float lowerMidrangeAmplitude = CalculateAmplitude(spectrumData, 250f, 500f);
        float midrangeAmplitude = CalculateAmplitude(spectrumData, 500f, 2000f);
        float higherMidrangeAmplitude = CalculateAmplitude(spectrumData, 2000f, 4000f);
        float presenceAmplitude = CalculateAmplitude(spectrumData, 4000f, 6000f);
        float brillianceAmplitude = CalculateAmplitude(spectrumData, 6000f, 20000f);

        // Log the amplitudes of each frequency band
        Debug.Log("Sub Bass: " + subBassAmplitude);
        Debug.Log("Bass: " + bassAmplitude);
        Debug.Log("Lower Midrange: " + lowerMidrangeAmplitude);
        Debug.Log("Midrange: " + midrangeAmplitude);
        Debug.Log("Higher Midrange: " + higherMidrangeAmplitude);
        Debug.Log("Presence: " + presenceAmplitude);
        Debug.Log("Brilliance: " + brillianceAmplitude);
    }

    float CalculateAmplitude(float[] spectrumData, float minFrequency, float maxFrequency)
    {
        int minIndex = (int)Mathf.Floor(minFrequency / (AudioSettings.outputSampleRate / spectrumData.Length));
        int maxIndex = (int)Mathf.Floor(maxFrequency / (AudioSettings.outputSampleRate / spectrumData.Length));

        float amplitude = 0f;
        for (int i = minIndex; i <= maxIndex; i++)
        {
            amplitude += spectrumData[i];
        }
        return amplitude;
    }
}
