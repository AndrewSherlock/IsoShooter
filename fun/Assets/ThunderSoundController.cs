using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderSoundController : MonoBehaviour {

    AudioSource audioSrc;

	// Use this for initialization
	void OnEnable () {
        audioSrc = GetComponent<AudioSource>();
        audioSrc.Play();
	}
	
}
