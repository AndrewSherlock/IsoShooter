using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISystem : MonoBehaviour {

    public Image[] lifeImages;
    public Sprite[] heartImages;
    public Sprite emptyHeart;

    public Text livesText;

    public Text gunName;
    public Image gunImage;
    public Text currentClip;
    public Text currentAmmo;

    public Text timerText;
    public Text playerPromptsText;
    public Text playerPromptsTextB;
    public Image playerPromptsImage;

    public Image targetImage;

    PlayerEntity p;
    GameManager gameManager;
    PlayerAttack playerAttack;
	
	// Update is called once per frame
	void Start () {
        p = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerEntity>();
        playerAttack = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PlayerAttack>();
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        DrawLifeHearts(p.currentHealth,p.maxHealth);
        PrintNumberOfLives(gameManager.GetLives());
        UpdateTimer(gameManager.levelTime);

        if (playerAttack.gunEntity != null)
        {
            PrintGunDetails(playerAttack.gunEntity);
        }
        else {
            ResetGunDetails();
        }
	}

    public void UpdateTimer(float timer)
    {
        int displayTime = Mathf.FloorToInt(timer);
        timerText.text = displayTime.ToString();
    }

    void PrintGunDetails(Gun g)
    {
        gunName.text = g.gunName;
        gunImage.sprite = g.gunImage;
        currentClip.text = g.numberOfBulletsInChamber.ToString();
        currentAmmo.text = "/" + g.currentAmmo.ToString();
    }

    void ResetGunDetails()
    {
        gunName.text = "None";
        //gunImage.sprite = ;
        currentClip.text = "0";
        currentAmmo.text = "/0";
    }

    void DrawLifeHearts(float currentHealth, float maxHealth)
    {
        float step = 6.25f;
        int currentHeart = 0;
        int currentCheck = 0;

        for (float x = step; x <= maxHealth; x += step)
        {
            if (x <= currentHealth)
            {
                lifeImages[currentHeart].sprite = heartImages[currentCheck];
                currentCheck++;
            }
            else {
                currentHeart++;
                DrawRestOfHeartsEmpty(currentHeart);
                return;
            }
            
            if (x % 25 == 0 && x != 0)
            {
               
                currentHeart++;
                currentCheck = 0;
            }
        }
    }

    void DrawRestOfHeartsEmpty(int currentCheck)
    {
        for (int x = currentCheck; x < lifeImages.Length; x++)
        {
            lifeImages[x].sprite = emptyHeart;
        }
    }

    void PrintNumberOfLives(int lives)
    {
        livesText.text = "Lives : " + lives;
    }

    public void playerPrompts(string messageA, string messageB = null, Sprite image = null)
    {
        if (image == null)
        {
            playerPromptsText.text = messageA;
        }
        else
        {
            playerPromptsText.text = messageA;
            playerPromptsImage.sprite = image;
            playerPromptsTextB.text = messageB;
        }
    }

    public void DrawTargetOnObject(Vector3 targetPosition)
    {
        targetImage.enabled = true;

        RectTransform image_rect = targetImage.rectTransform;

        RectTransform canvasRect = GameObject.Find("Canvas").GetComponent<RectTransform>();
      
        Vector2 screenPos = Camera.main.WorldToViewportPoint(targetPosition);
       // screenPos.y = -screenPos.y;

        Vector2 worldPointCanvasConversion = new Vector2((screenPos.x * canvasRect.sizeDelta.x) - (canvasRect.sizeDelta.x * 0.5f),
            (screenPos.y * canvasRect.sizeDelta.y) - (canvasRect.sizeDelta.y * 0.5f));
      
        image_rect.anchoredPosition = worldPointCanvasConversion;
    }
}

