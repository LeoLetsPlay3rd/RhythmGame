using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class OpacityController : MonoBehaviour
{
    public Image image;
    public float fadeDuration = 2f;
    public float delayBeforeFadeIn = 2f;
    public float timeBeforeReset = 10f;

    private bool fadingIn = false;
    private float currentFadeTime = 0f;

    void Start()
    {
        // Start the fade-in process
        fadingIn = true;
        Invoke("ResetFadeProcess", timeBeforeReset);
    }

    void Update()
    {
        if (fadingIn)
        {
            currentFadeTime += Time.deltaTime;

            if (currentFadeTime >= fadeDuration)
            {
                fadingIn = false;
                currentFadeTime = fadeDuration;

                // Start the delay before fading in again
                Invoke("StartFadeIn", delayBeforeFadeIn);
            }

            float alpha = currentFadeTime / fadeDuration;
            SetImageOpacity(alpha);
        }

        // Check for player input to trigger instant disappearance
        if (Input.GetKeyDown(KeyCode.S))
        {
            SetImageOpacity(0f); // Set alpha to 0 to make the image disappear instantly
            CancelInvoke(); // Cancel the delayed fade-in
            Invoke("ResetFadeProcess", timeBeforeReset); // Reset the fading process after a delay
        }
    }

    void StartFadeIn()
    {
        // Reset variables and start fading in again
        currentFadeTime = 0f;
        fadingIn = true;
    }

    void ResetFadeProcess()
    {
        // Reset the fading process
        currentFadeTime = 0f;
        fadingIn = true;
        Invoke("StartFadeIn", delayBeforeFadeIn);
        Invoke("ResetFadeProcess", timeBeforeReset);
    }

    void SetImageOpacity(float alpha)
    {
        Color color = image.color;
        color.a = alpha;
        image.color = color;
    }
}



