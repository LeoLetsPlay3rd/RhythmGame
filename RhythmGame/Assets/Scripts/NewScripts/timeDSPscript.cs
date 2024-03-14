using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

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
    public int notesLeftBeforeLoss;
    public double watOfError = 1;

    public bool successfullHitS;
    public bool successfullHitD;
    public bool successfullHitK;
    public bool successfullHitL;

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
        notesLeftBeforeLoss = 3;

        successfullHitS = false;
        successfullHitD = false;
        successfullHitK = false;
        successfullHitL = false;
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

            StatusCheck();

            DetectNotes(elapsedTime);
        }

    }

    private void SpawnNotes()
    {
        foreach (var data in DataSheet.doubleArraySKey)
        {
            if (data.Length > 0)
            {
                float xCoordinate = -350;
                float yCoordinate = data[0] * noteSpacing;
                float zCoordinate = 0f;

                Vector3 newPosition = new Vector3(xCoordinate, yCoordinate, zCoordinate);

                // Instantiate the prefab
                GameObject instantiatedObject = Instantiate(nodePrefab, canvas.transform);

                // Set the position
                instantiatedObject.transform.localPosition = newPosition;

                // Find the child object to clone
                GameObject originalChild = instantiatedObject.transform.Find("Length").gameObject;

                if (originalChild != null)
                {
                    // Enable or disable the child object based on the condition
                    bool enableChild = data[1] > 0.220f;
                    originalChild.SetActive(enableChild);

                    if (enableChild)
                    {
                        // Set the scale of the child object based on data[1]
                        float heightScale = data[1]; // Assuming data[1] is the desired height scale
                        RectTransform childRectTransform = originalChild.GetComponent<RectTransform>();
                        if (childRectTransform != null)
                        {
                            // Calculate the new size based on the height scale
                            float newHeight = childRectTransform.sizeDelta.y * heightScale;

                            // Set the size and alignment
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

                // Add the instantiated object's transform to the list
                notes.Add((instantiatedObject.transform, data[0], data[1], KeyType.S));
            }
        }

        foreach (var data in DataSheet.doubleArrayDKey)
        {
            if (data.Length > 0)
            {
                float xCoordinate = -150;
                float yCoordinate = data[0] * noteSpacing;
                float zCoordinate = 0f;

                Vector3 newPosition = new Vector3(xCoordinate, yCoordinate, zCoordinate);

                GameObject instantiatedObject = Instantiate(nodePrefab, canvas.transform);
                instantiatedObject.transform.localPosition = newPosition;
                notes.Add((instantiatedObject.transform, data[0], data[1], KeyType.D));
            }
        }

        foreach (var data in DataSheet.doubleArrayKKey)
        {
            if (data.Length > 0)
            {
                float xCoordinate = 50;
                float yCoordinate = data[0] * noteSpacing;
                float zCoordinate = 0f;

                Vector3 newPosition = new Vector3(xCoordinate, yCoordinate, zCoordinate);

                GameObject instantiatedObject = Instantiate(nodePrefab, canvas.transform);
                instantiatedObject.transform.localPosition = newPosition;
                notes.Add((instantiatedObject.transform, data[0], data[1], KeyType.K));
            }
        }

        foreach (var data in DataSheet.doubleArrayLKey)
        {
            if (data.Length > 0)
            {
                float xCoordinate = 250;
                float yCoordinate = data[0] * noteSpacing;
                float zCoordinate = 0f;

                Vector3 newPosition = new Vector3(xCoordinate, yCoordinate, zCoordinate);

                GameObject instantiatedObject = Instantiate(nodePrefab, canvas.transform);
                instantiatedObject.transform.localPosition = newPosition;
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
                        Destroy(note.transform.gameObject);
                        notes.RemoveAt(i);

                        successfullHitS = true;
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

    private void NoteFailure()
    {
        notesLeftBeforeLoss--;
        Debug.Log("Notes left before loss: " + notesLeftBeforeLoss);
    }


    private void StatusCheck()
    {
        if(notesLeftBeforeLoss <= 0)
        {
            Application.Quit();
            Debug.Log("Application quit");
        }
    }
}
