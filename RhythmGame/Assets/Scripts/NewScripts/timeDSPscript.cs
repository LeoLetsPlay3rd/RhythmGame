using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class timeDSPscript : MonoBehaviour
{
    public GameObject nodePrefab;
    public GameObject canvas;
    public float movementSpeed = 1;
    public AudioSource audioSource;
    private double audioStartTime;
    private bool audioPlaying = false;
    public float noteSpacing = 10;
    public double offSet = 0;
    private List<(Transform transform, float time, float length, KeyType keyType)> notes = new List<(Transform transform, float time, float length, KeyType keyType)>();
    public float scaleFactor = 2;

    public int notesLeftBeforeLoss;
    public double watOfError = 1;
    public float missLineYPosition = -420f;
    private Dictionary<Transform, bool> notesCheckedForMiss = new Dictionary<Transform, bool>();
    public Material materialMiss;
    public Material colouredNoteMat;

    public bool successfullHitS;
    public bool successfullHitD;
    public bool successfullHitK;
    public bool successfullHitL;

    public GameObject SNoteImage;
    public GameObject DNoteImage;
    public GameObject KNoteImage;
    public GameObject LNoteImage;


    private float missAlpha = 0f;
    private float fadeDuration = 0.5f;

    public enum KeyType
    {
        S,
        D,
        K,
        L
    }

    private void Awake()
    {
        audioStartTime = AudioSettings.dspTime;
        SpawnNotes();
        notesLeftBeforeLoss = 1;

        successfullHitS = false;
        successfullHitD = false;
        successfullHitK = false;
        successfullHitL = false;

        materialMiss.SetFloat("_OnMissAlpha", missAlpha);

        SNoteImage.SetActive(false);
        DNoteImage.SetActive(false);
        KNoteImage.SetActive(false);
        LNoteImage.SetActive(false);
    }

    private void Update()
    {
        if (audioSource.isPlaying)
        {
            if (!audioPlaying)
            {
                audioStartTime = AudioSettings.dspTime;
                audioPlaying = true;
            }

            double elapsedTime = AudioSettings.dspTime - audioStartTime + offSet;

            MoveNotes(elapsedTime);

            DetectNotes(elapsedTime);
        }

    }

    private void SpawnNotes()
    {
        foreach (var data in DataSheet.doubleArraySKey)
        {
            if (data.Length > 0)
            {
                float xCoordinate = -190;
                float yCoordinate = data[0] * noteSpacing;
                float zCoordinate = 0f;

                Vector3 newPosition = new Vector3(xCoordinate, yCoordinate, zCoordinate);

                GameObject instantiatedObject = Instantiate(nodePrefab, canvas.transform);

                instantiatedObject.transform.localPosition = newPosition;

                GameObject originalChild = instantiatedObject.transform.Find("Length").gameObject;

                if (originalChild != null)
                {
                    bool enableChild = data[1] > 0.220f;
                    originalChild.SetActive(enableChild);

                    if (enableChild)
                    {
                        float heightScale = data[1] > 0.220f ? 1 + (data[1] - 0.220f) * scaleFactor : 1;
                        RectTransform childRectTransform = originalChild.GetComponent<RectTransform>();
                        if (childRectTransform != null)
                        {
                            float newHeight = childRectTransform.sizeDelta.y * heightScale;

                            childRectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Bottom, 0, newHeight);
                        }
                        else
                        {
                            Debug.LogError("Child object does not have a RectTransform component.");
                        }
                    }
                }
                else
                {
                    Debug.LogError("Child object not found in prefab.");
                }

                notes.Add((instantiatedObject.transform, data[0], data[1], KeyType.S));
            }
        }

        foreach (var data in DataSheet.doubleArrayDKey)
        {
            if (data.Length > 0)
            {
                float xCoordinate = -65;
                float yCoordinate = data[0] * noteSpacing;
                float zCoordinate = 0f;

                Vector3 newPosition = new Vector3(xCoordinate, yCoordinate, zCoordinate);

                GameObject instantiatedObject = Instantiate(nodePrefab, canvas.transform);

                instantiatedObject.transform.localPosition = newPosition;

                GameObject originalChild = instantiatedObject.transform.Find("Length").gameObject;

                if (originalChild != null)
                {
                    bool enableChild = data[1] > 0.220f;
                    originalChild.SetActive(enableChild);

                    if (enableChild)
                    {
                        float heightScale = data[1] > 0.220f ? 1 + (data[1] - 0.220f) * scaleFactor : 1;
                        RectTransform childRectTransform = originalChild.GetComponent<RectTransform>();
                        if (childRectTransform != null)
                        {
                            float newHeight = childRectTransform.sizeDelta.y * heightScale;

                            childRectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Bottom, 0, newHeight);
                        }
                        else
                        {
                            Debug.LogError("Child object does not have a RectTransform component.");
                        }
                    }
                }
                else
                {
                    Debug.LogError("Child object not found in prefab.");
                }

                notes.Add((instantiatedObject.transform, data[0], data[1], KeyType.D));
            }
        }

        foreach (var data in DataSheet.doubleArrayKKey)
        {
            if (data.Length > 0)
            {
                float xCoordinate = 65;
                float yCoordinate = data[0] * noteSpacing;
                float zCoordinate = 0f;

                Vector3 newPosition = new Vector3(xCoordinate, yCoordinate, zCoordinate);

                GameObject instantiatedObject = Instantiate(nodePrefab, canvas.transform);

                instantiatedObject.transform.localPosition = newPosition;

                GameObject originalChild = instantiatedObject.transform.Find("Length").gameObject;

                if (originalChild != null)
                {
                    bool enableChild = data[1] > 0.220f;
                    originalChild.SetActive(enableChild);

                    if (enableChild)
                    {
                        float heightScale = data[1] > 0.220f ? 1 + (data[1] - 0.220f) * scaleFactor : 1;
                        RectTransform childRectTransform = originalChild.GetComponent<RectTransform>();
                        if (childRectTransform != null)
                        {
                            float newHeight = childRectTransform.sizeDelta.y * heightScale;

                            childRectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Bottom, 0, newHeight);
                        }
                        else
                        {
                            Debug.LogError("Child object does not have a RectTransform component.");
                        }
                    }
                }
                else
                {
                    Debug.LogError("Child object not found in prefab.");
                }

                notes.Add((instantiatedObject.transform, data[0], data[1], KeyType.K));
            }
        }

        foreach (var data in DataSheet.doubleArrayLKey)
        {
            if (data.Length > 0)
            {
                float xCoordinate = 190;
                float yCoordinate = data[0] * noteSpacing;
                float zCoordinate = 0f;

                Vector3 newPosition = new Vector3(xCoordinate, yCoordinate, zCoordinate);

                GameObject instantiatedObject = Instantiate(nodePrefab, canvas.transform);

                instantiatedObject.transform.localPosition = newPosition;

                GameObject originalChild = instantiatedObject.transform.Find("Length").gameObject;

                if (originalChild != null)
                {
                    bool enableChild = data[1] > 0.220f;
                    originalChild.SetActive(enableChild);

                    if (enableChild)
                    {
                        float heightScale = data[1] > 0.220f ? 1 + (data[1] - 0.220f) * scaleFactor : 1;
                        RectTransform childRectTransform = originalChild.GetComponent<RectTransform>();
                        if (childRectTransform != null)
                        {
                            float newHeight = childRectTransform.sizeDelta.y * heightScale;

                            childRectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Bottom, 0, newHeight);
                        }
                        else
                        {
                            Debug.LogError("Child object does not have a RectTransform component.");
                        }
                    }
                }
                else
                {
                    Debug.LogError("Child object not found in prefab.");
                }

                notes.Add((instantiatedObject.transform, data[0], data[1], KeyType.L));
            }
        }
    }

    private void MoveNotes(double elapsedTime)
    {
        for (int i = 0; i < notes.Count; i++)
        {
            var note = notes[i];

            Vector3 position = note.transform.localPosition;
            position.y = (float)((note.time - elapsedTime) * noteSpacing * movementSpeed);

            note.transform.localPosition = position;
        }
    }

    private IEnumerator FadeOutAndDestroy(GameObject noteObject)
    {
        Image noteImage = noteObject.GetComponent<Image>();
        if (noteImage != null)
        {
            float elapsedTime = 0f;
            float fadeDuration = 0.3f;

            // Gradually decrease alpha over 0.3 seconds
            while (elapsedTime < fadeDuration)
            {
                float newAlpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
                Color newColor = noteImage.color;
                newColor.a = newAlpha;
                noteImage.color = newColor;
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            // Ensure alpha is 0 after fading
            Color finalColor = noteImage.color;
            finalColor.a = 0f;
            noteImage.color = finalColor;

            // Destroy the note object
            Destroy(noteObject);
        }
        else
        {
            Debug.LogError("Image component not found in the note prefab.");
        }
    }

    private IEnumerator HandleSuccessfullHit(GameObject noteImageObject)
    {
        Image noteImage = noteImageObject.GetComponent<Image>();
        if (noteImage != null)
        {
            // Enable the note image
            noteImageObject.SetActive(true);

            // Store initial properties
            Color initialColor = noteImage.color;
            Vector3 initialScale = noteImageObject.transform.localScale;

            // Set initial alpha value
            float initialAlpha = 0.3f;
            float finalAlpha = 0f;
            float fadeDuration = 0.1f; // 0.1 seconds fade duration
            float startTime = Time.time;

            // Set the initial alpha
            Color startColor = noteImage.color;
            startColor.a = initialAlpha;
            noteImage.color = startColor;

            // Set initial properties for scaling up
            float scaleDuration = 0.1f; // 0.1 seconds for scaling up
            Vector3 targetScale = Vector3.one * 1.875f;
            float scaleStartTime = Time.time;

            // Scale up gradually to 1.875 over scaleDuration seconds
            while (Time.time < scaleStartTime + scaleDuration)
            {
                float t = (Time.time - scaleStartTime) / scaleDuration;
                noteImageObject.transform.localScale = Vector3.Lerp(initialScale, targetScale, t);

                // Update alpha value for fading
                float alphaT = (Time.time - startTime) / fadeDuration;
                float newAlpha = Mathf.Lerp(initialAlpha, finalAlpha, alphaT);
                noteImage.material.SetFloat("_BasedIndicator", newAlpha);

                yield return null;
            }

            // Ensure final properties are set
            noteImageObject.transform.localScale = targetScale;

            // Fade out the note gradually over fadeDuration seconds
            while (Time.time < startTime + fadeDuration)
            {
                float t = (Time.time - startTime) / fadeDuration;
                Color newColor = noteImage.color;
                newColor.a = Mathf.Lerp(initialAlpha, finalAlpha, t);
                noteImage.color = newColor;

                // Update alpha value for fading
                float newAlpha = Mathf.Lerp(initialAlpha, finalAlpha, t);
                noteImage.material.SetFloat("_BasedIndicator", newAlpha);

                yield return null;
            }

            // Ensure final properties are set
            Color finalColor = noteImage.color;
            finalColor.a = finalAlpha;
            noteImage.color = finalColor;

            // Restore initial properties
            noteImage.color = initialColor;
            noteImageObject.transform.localScale = initialScale;

            noteImageObject.SetActive(false); // Disable the note image after fading
        }
        else
        {
            Debug.LogError("Image component not found in the note image object.");
        }
    }







    private void DetectNotes(double elapsedTime)
    {
        foreach (var note in notes)
        {
            if (!notesCheckedForMiss.ContainsKey(note.transform))
            {
                if (note.transform.position.y < missLineYPosition)
                {
                    Debug.Log("Missed note detected!");

                    StartCoroutine(FadeOutMissAlpha(fadeDuration));

                    notesCheckedForMiss[note.transform] = true;
                }
            }
        }



        if (Input.GetKeyDown(KeyCode.S))
        {
            for (int i = notes.Count - 1; i >= 0; i--)
            {
                var note = notes[i];
                if (note.keyType == KeyType.S)
                {
                    double Difference = math.abs(note.time - elapsedTime + offSet);

                    if (Difference < watOfError)
                    {
                        GameObject noteObject = note.transform.gameObject;
                        notes.RemoveAt(i);

                        successfullHitS = true;

                        // Change material to colouredNoteMat
                        Image image = note.transform.GetComponent<Image>();
                        if (image != null)
                        {
                            image.material = colouredNoteMat;
                            StartCoroutine(HandleSuccessfullHit(SNoteImage));
                        }
                        else
                        {
                            Debug.LogError("Image component not found in the prefab.");
                        }

                        // Start coroutine to fade out and destroy note
                        StartCoroutine(FadeOutAndDestroy(noteObject));
                    }
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            for (int i = notes.Count - 1; i >= 0; i--)
            {
                var note = notes[i];
                if (note.keyType == KeyType.D)
                {
                    double Difference = math.abs(note.time - elapsedTime + offSet);

                    if (Difference < watOfError)
                    {
                        GameObject noteObject = note.transform.gameObject;
                        notes.RemoveAt(i);

                        successfullHitS = true;

                        // Change material to colouredNoteMat
                        Image image = note.transform.GetComponent<Image>();
                        if (image != null)
                        {
                            image.material = colouredNoteMat;
                            StartCoroutine(HandleSuccessfullHit(DNoteImage));
                        }
                        else
                        {
                            Debug.LogError("Image component not found in the prefab.");
                        }

                        // Start coroutine to fade out and destroy note
                        StartCoroutine(FadeOutAndDestroy(noteObject));
                    }
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            for (int i = notes.Count - 1; i >= 0; i--)
            {
                var note = notes[i];
                if (note.keyType == KeyType.K)
                {
                    double Difference = math.abs(note.time - elapsedTime + offSet);

                    if (Difference < watOfError)
                    {
                        GameObject noteObject = note.transform.gameObject;
                        notes.RemoveAt(i);

                        successfullHitS = true;

                        // Change material to colouredNoteMat
                        Image image = note.transform.GetComponent<Image>();
                        if (image != null)
                        {
                            image.material = colouredNoteMat;
                            StartCoroutine(HandleSuccessfullHit(KNoteImage));
                        }
                        else
                        {
                            Debug.LogError("Image component not found in the prefab.");
                        }

                        // Start coroutine to fade out and destroy note
                        StartCoroutine(FadeOutAndDestroy(noteObject));
                    }
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            for (int i = notes.Count - 1; i >= 0; i--)
            {
                var note = notes[i];
                if (note.keyType == KeyType.L)
                {
                    double Difference = math.abs(note.time - elapsedTime + offSet);

                    if (Difference < watOfError)
                    {
                        GameObject noteObject = note.transform.gameObject;
                        notes.RemoveAt(i);

                        successfullHitS = true;

                        // Change material to colouredNoteMat
                        Image image = note.transform.GetComponent<Image>();
                        if (image != null)
                        {
                            image.material = colouredNoteMat;
                            StartCoroutine(HandleSuccessfullHit(LNoteImage));
                        }
                        else
                        {
                            Debug.LogError("Image component not found in the prefab.");
                        }

                        // Start coroutine to fade out and destroy note
                        StartCoroutine(FadeOutAndDestroy(noteObject));
                    }
                }
            }
        }
    }

    private IEnumerator FadeOutMissAlpha(float duration)
    {
        float elapsedTime = 0f;
        float startAlpha = 1f;

        while (elapsedTime < duration)
        {
            float alpha = Mathf.Lerp(startAlpha, 0f, elapsedTime / duration);
            materialMiss.SetFloat("_OnMissAlpha", alpha);

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        materialMiss.SetFloat("_OnMissAlpha", 0f);
    }

    // Coroutine to wait for 1 second before destroying the note
    private IEnumerator DestroyNoteAfterDelay(GameObject noteObject)
    {
        yield return new WaitForSeconds(1f);
        Destroy(noteObject);
    }
}