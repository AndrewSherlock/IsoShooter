using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenTextute : MonoBehaviour {


    MovieTexture movie;

	// Use this for initialization
	void Start () {
        Renderer screenRender = GetComponent<Renderer>();
        movie = (MovieTexture)screenRender.material.mainTexture;
        movie.loop = true;
        movie.Play();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
