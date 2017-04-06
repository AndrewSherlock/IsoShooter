using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {


    public GameObject[] respawnPoints;
    public float levelTime;
    public float currentLevelTime;
    int currentRespawn = 1;
    int currentLives;

    UISystem gameUI;

    public List<Event> eventObjects;
    int currentObj = 0;

    bool playerNotDead = true;

	// Use this for initialization
	void Awake () {
        gameUI = Camera.main.GetComponent<UISystem>();
        currentLevelTime = levelTime;
        GetNextEventLinedUp(currentObj);
        currentLives = 3;
    }

    private void LateUpdate()
    {
        if (playerNotDead)
        {
            if (currentLevelTime > 0)
            {
                DecrementTimer();
                gameUI.UpdateTimer(currentLevelTime);
            }
            else
            {
                playerNotDead = false;
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                KillPlayer(player);
            }
        }
    }

    void DecrementTimer()
    {
        currentLevelTime -= Time.deltaTime;
        if (currentLevelTime < 0)
        {
            currentLevelTime = 0;
        }
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

    public Vector3 GetCurrentRespawnPoint()
    {
        return respawnPoints[currentRespawn].transform.position;
    }

    public void KillPlayer(GameObject player)
    {
        if (currentLives > 0)
        {
            player.SetActive(false);
            StartCoroutine(RespawnPlayerDelay(player));
            currentLives--;
        }
    }

    IEnumerator RespawnPlayerDelay(GameObject player)
    {
        yield return new WaitForSeconds(4f);
        player.transform.position = GetCurrentRespawnPoint();
        currentLevelTime = levelTime;
        gameUI.UpdateTimer(currentLevelTime);
        gameUI.livesText.text = "Lives : " + currentLives;
        player.SetActive(true);
        playerNotDead = true;
    }

    public int GetLives()
    {
        return currentLives;
    }
}
