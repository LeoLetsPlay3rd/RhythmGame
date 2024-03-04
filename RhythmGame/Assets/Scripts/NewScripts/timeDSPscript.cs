using UnityEngine;

public class timeDSPscript : MonoBehaviour
{
    public GameObject nodePrefab;
    public GameObject canvas; 
    public float movementSpeed = 1;
    public AudioSource audioSource;
    private double audioStartTime;
    private bool audioPlaying = false;

    private void Awake()
    {
        SpawnNodes();
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

            double elapsedTime = AudioSettings.dspTime - audioStartTime;

            MoveNodes(elapsedTime);
        }
    }

    private void SpawnNodes()
    {
        foreach (var data in DataSheet.doubleArraySKey)
        {
            if (data.Length > 0)
            {
                float xCoordinate = -350;
                float yCoordinate = data[0] * 1000;
                float zCoordinate = 0f;

                Vector3 newPosition = new Vector3(xCoordinate, yCoordinate, zCoordinate);

                GameObject instantiatedObject = Instantiate(nodePrefab, canvas.transform);
                instantiatedObject.transform.localPosition = newPosition;
            }
        }

        foreach (var data in DataSheet.doubleArrayDKey)
        {
            if (data.Length > 0)
            {
                float xCoordinate = -150;
                float yCoordinate = data[0] * 1000;
                float zCoordinate = 0f;

                Vector3 newPosition = new Vector3(xCoordinate, yCoordinate, zCoordinate);

                GameObject instantiatedObject = Instantiate(nodePrefab, canvas.transform);
                instantiatedObject.transform.localPosition = newPosition;
            }
        }

        foreach (var data in DataSheet.doubleArrayKKey)
        {
            if (data.Length > 0)
            {
                float xCoordinate = 150;
                float yCoordinate = data[0] * 1000;
                float zCoordinate = 0f;

                Vector3 newPosition = new Vector3(xCoordinate, yCoordinate, zCoordinate);

                GameObject instantiatedObject = Instantiate(nodePrefab, canvas.transform);
                instantiatedObject.transform.localPosition = newPosition;
            }
        }

        foreach (var data in DataSheet.doubleArrayLKey)
        {
            if (data.Length > 0)
            {
                float xCoordinate = 350;
                float yCoordinate = data[0] * 1000;
                float zCoordinate = 0f;

                Vector3 newPosition = new Vector3(xCoordinate, yCoordinate, zCoordinate);

                GameObject instantiatedObject = Instantiate(nodePrefab, canvas.transform);
                instantiatedObject.transform.localPosition = newPosition;
            }
        }

    }

    private void MoveNodes(double elapsedTime)
    {
        float downwardMovement = (float)elapsedTime * movementSpeed;

        foreach (Transform node in canvas.transform)
        {
            Vector3 currentPosition = node.localPosition;

            float newY = currentPosition.y - downwardMovement;

            node.localPosition = new Vector3(currentPosition.x, newY, currentPosition.z);
        }
    }
}
