using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Event : MonoBehaviour {

    protected string objectiveName;
    protected string instruction;

    public abstract void EventController();

    public void MoveToNextEvent()
    {
        GameManager gman = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        gman.MoveToNextEvent();
        gameObject.SetActive(false);
    }
}
