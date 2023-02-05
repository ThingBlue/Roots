using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public TMP_Text timerText;
    public TMP_Text runeCountText;

    public float gameTimer = 0;
    public float timeLimit = 180;

    public int runesCollected = 0;

    public BoxCollider2D doorCollider;
    public GameObject doorOpenSprite;
    public GameObject doorClosedSprite;

    private void Awake()
    {
        // Singleton
        if (instance == null) instance = this;
        else Destroy(this);
    }

    private void Start()
    {
        gameTimer = timeLimit;
        doorClosedSprite.SetActive(true);
        doorOpenSprite.SetActive(false);
        doorCollider.enabled = true;
    }

    private void FixedUpdate()
    {
        if (runesCollected < 5)
        {
            gameTimer -= Time.fixedDeltaTime;
            int displayTimer = (int)gameTimer;
            timerText.text = $"Time remaining: {displayTimer}";
        }
        else
        {
            timerText.text = $"Door has been opened";
        }
        runeCountText.text = $"Runes collected: {runesCollected}/5";

        if (runesCollected >= 5)
        {
            // Trigger win
            openDoor();
            return;
        }

        if (gameTimer > timeLimit)
        {
            // Trigger game over
        }
    }

    private void openDoor()
    {
        doorClosedSprite.SetActive(false);
        doorOpenSprite.SetActive(true);
        doorCollider.enabled = false;
    }
}
