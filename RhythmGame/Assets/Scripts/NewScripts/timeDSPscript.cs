using Melanchall.DryWetMidi.Interaction;
using Melanchall.DryWetMidi.MusicTheory;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    private List<(Transform transform, float time)> notes = new List<(Transform transform, float time)>();
    public int notesLeftBeforeLoss;
    public double marginOfError = 1;

    private void Awake()
    {
        audioStartTime = AudioSettings.dspTime;
        SpawnNotes();
        notesLeftBeforeLoss = 3;
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

        if (Input.anyKeyDown)
        {
            Debug.Log("A key was pressed");
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

                GameObject instantiatedObject = Instantiate(nodePrefab, canvas.transform);
                instantiatedObject.transform.localPosition = newPosition;
                notes.Add((instantiatedObject.transform, data[0]));
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
                notes.Add((instantiatedObject.transform, data[0]));
            }
        }

        foreach (var data in DataSheet.doubleArrayKKey)
        {
            if (data.Length > 0)
            {
                float xCoordinate = 150;
                float yCoordinate = data[0] * noteSpacing;
                float zCoordinate = 0f;

                Vector3 newPosition = new Vector3(xCoordinate, yCoordinate, zCoordinate);

                GameObject instantiatedObject = Instantiate(nodePrefab, canvas.transform);
                instantiatedObject.transform.localPosition = newPosition;
                notes.Add((instantiatedObject.transform, data[0]));
            }
        }

        foreach (var data in DataSheet.doubleArrayLKey)
        {
            if (data.Length > 0)
            {
                float xCoordinate = 350;
                float yCoordinate = data[0] * noteSpacing;
                float zCoordinate = 0f;

                Vector3 newPosition = new Vector3(xCoordinate, yCoordinate, zCoordinate);

                GameObject instantiatedObject = Instantiate(nodePrefab, canvas.transform);
                instantiatedObject.transform.localPosition = newPosition;
                notes.Add((instantiatedObject.transform, data[0]));
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
        foreach (var noteData in DataSheet.doubleArraySKey)
        {
            if ((noteData[0] - elapsedTime) < marginOfError && Input.GetKeyDown(KeyCode.S))
            {
                var note = notes.Find(n => Mathf.Approximately(n.time, noteData[0]));

                if (note.transform != null && note.transform.gameObject != null)
                {
                    DestroyImmediate(note.transform.gameObject);
                    notes.Remove(note);
                    break;
                }
            }
        }
    }

    //if (Mathf.Abs((float)(noteData[0] - elapsedTime)) < marginOfError && Input.GetKeyDown(KeyCode.S))

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
