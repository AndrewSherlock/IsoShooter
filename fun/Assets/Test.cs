﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 thisBounds = GetComponent<Renderer>().bounds.center;

        Debug.DrawRay(thisBounds, Vector3.up * 10f, Color.cyan);
	}
}
