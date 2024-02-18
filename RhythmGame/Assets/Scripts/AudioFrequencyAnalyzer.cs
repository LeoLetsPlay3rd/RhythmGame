using UnityEngine;

public class AudioFrequencyAnalyzer : MonoBehaviour
{
    public AudioSource audioSource;

    [SerializeField]
    private float[] frequencyRanges = new float[7]; // Frequency ranges for each band

    // Public float properties for amplitude of each frequency band
    public float SubBassAmplitude { get; private set; }
    public float BassAmplitude { get; private set; }
    public float LowerMidrangeAmplitude { get; private set; }
    public float MidrangeAmplitude { get; private set; }
    public float HigherMidrangeAmplitude { get; private set; }
    public float PresenceAmplitude { get; private set; }
    public float BrillianceAmplitude { get; private set; }

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
        SubBassAmplitude = CalculateAmplitude(spectrumData, frequencyRanges[0], frequencyRanges[1]);
        BassAmplitude = CalculateAmplitude(spectrumData, frequencyRanges[1], frequencyRanges[2]);
        LowerMidrangeAmplitude = CalculateAmplitude(spectrumData, frequencyRanges[2], frequencyRanges[3]);
        MidrangeAmplitude = CalculateAmplitude(spectrumData, frequencyRanges[3], frequencyRanges[4]);
        HigherMidrangeAmplitude = CalculateAmplitude(spectrumData, frequencyRanges[4], frequencyRanges[5]);
        PresenceAmplitude = CalculateAmplitude(spectrumData, frequencyRanges[5], frequencyRanges[6]);
        BrillianceAmplitude = CalculateAmplitude(spectrumData, frequencyRanges[6], 20000f);

        // Log the amplitudes of each frequency band
        Debug.Log("Sub Bass: " + SubBassAmplitude);
        Debug.Log("Bass: " + BassAmplitude);
        Debug.Log("Lower Midrange: " + LowerMidrangeAmplitude);
        Debug.Log("Midrange: " + MidrangeAmplitude);
        Debug.Log("Higher Midrange: " + HigherMidrangeAmplitude);
        Debug.Log("Presence: " + PresenceAmplitude);
        Debug.Log("Brilliance: " + BrillianceAmplitude);
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
