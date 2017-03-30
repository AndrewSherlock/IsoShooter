using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    // level one events should be turn off radio, get gun and aim the gun, get mystery letter under door
    // then walk down hall, see a neighbor that says something weird, when hit a certain point, lightening flashes and the message is displayied breifly on the wall
    // figure? maybe  i dont know

    public List<Event> eventObjects;
    int currentObj = 0;

	// Use this for initialization
	void Start () {
        GetNextEventLinedUp(currentObj);

    }
	
	// Update is called once per frame
	void GetNextEventLinedUp (int eventId) {
       // eventObjects[eventId].EventController();


    }

    public void MoveToNextEvent()
    {
        currentObj++;
        eventObjects[currentObj].EventController();
    }
}
