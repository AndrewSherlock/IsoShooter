using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioEventScript : Event {

    bool isRadioPlaying;
    public GameObject radio;
    public AudioSource audioSrc;

    void OnTriggerStay()
    {
        if (isRadioPlaying)
        {
            if (Input.GetKey(KeyCode.I))
            {
                isRadioPlaying = false;
                audioSrc.Stop();
                MoveToNextEvent();
            }
        }
    }

    public override void EventController()
    {
        objectiveName = "Turn off the radio";
        instruction = "The Radio is playing loudly, i better turn it off before i leave.";
        isRadioPlaying = true;
        audioSrc = GetComponent<AudioSource>();
        audioSrc.Play();
    }
}
