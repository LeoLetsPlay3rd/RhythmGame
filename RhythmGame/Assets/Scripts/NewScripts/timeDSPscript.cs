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

                        StartCoroutine(DestroyNoteAfterDelay(noteObject));

                        notes.RemoveAt(i);

                        successfullHitS = true;

                        Image imageComponent = note.transform.GetComponent<Image>();
                        if (imageComponent != null)
                        {
                            imageComponent.material = colouredNoteMat;
                        }
                        else
                        {
                            Debug.LogError("Image component not found in the prefab.");
                        }
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
                        Destroy(note.transform.gameObject);
                        notes.RemoveAt(i);

                        successfullHitD = true;
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
                        Destroy(note.transform.gameObject);
                        notes.RemoveAt(i);

                        successfullHitK = true;
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
                        Destroy(note.transform.gameObject);
                        notes.RemoveAt(i);

                        successfullHitL = true;
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
        yield return new WaitForSeconds(0.1f);
        Destroy(noteObject);
    }
}