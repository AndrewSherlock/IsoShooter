using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldController : MonoBehaviour {


    public Animator thunderItem;
 
	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        LighteningController();
       
	}

    bool hasTimer = false;
    bool hasFlashed = false;
    float timer;
    float delay;

    void LighteningController()
    {
        if (delay > 0)
        {
            delay -= Time.deltaTime;
        }
       
        if (!hasTimer && delay <= 0)
        {
            thunderItem.SetBool("shouldFlash", false);
            timer = Random.Range(9, 20);
            hasTimer = true;
        }

        if (hasTimer && !hasFlashed)
        {
            if (timer < 0)
            {
                hasFlashed = true;
            }
            else
            {
                timer -= Time.deltaTime;
            }
        }

        if (hasTimer && hasFlashed)
        {
            thunderItem.SetBool("shouldFlash", true);
            hasTimer = hasFlashed = false;
            delay = 6f;
        }
    }
}
