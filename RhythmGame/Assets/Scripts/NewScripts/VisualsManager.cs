using UnityEngine;
using System.Collections.Generic;

public class VisualsManager : MonoBehaviour
{
    public timeDSPscript timeScript;
    public GameObject vfx1;
    public GameObject vfx2;

    public class AudioEvent
    {
        public double startTime;
        public double duration;
        public System.Action action;

        public AudioEvent(double startTime, double duration, System.Action action)
        {
            this.startTime = startTime;
            this.duration = duration;
            this.action = action;
        }
    }

    public List<AudioEvent> events = new List<AudioEvent>();

    void Start()
    {
        events.Add(new AudioEvent(5.0, 5.0, Event1Action));
        events.Add(new AudioEvent(20.0, 10.0, Event2Action));
    }

    void Update()
    {
        double elapsedTime = timeScript.audioSource.time;

        foreach (AudioEvent audioEvent in events)
        {
            if (elapsedTime >= audioEvent.startTime && elapsedTime < audioEvent.startTime + audioEvent.duration)
            {
                audioEvent.action.Invoke();
            }
        }
    }

    void Event1Action()
    {
        if (vfx1 != null)
        {
            vfx1.SetActive(true);
        }

        Debug.Log("Event 1 Action");
    }

    void Event2Action()
    {
        if (vfx1 != null)
        {
            vfx1.SetActive(false);
        }
        if (vfx2 != null)
        {
            vfx2.SetActive(true);
        }

        Debug.Log("Event 2 Action");
    }

}
