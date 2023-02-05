using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public TMP_Text timerText;

    public float gameTimer = 0;
    public float timeLimit = 180;

    public int runesCollected = 0;

    private void Awake()
    {
        // Singleton
        if (instance == null) instance = this;
        else Destroy(this);
    }

    private void Start()
    {
        gameTimer = timeLimit;
    }

    private void FixedUpdate()
    {
        gameTimer -= Time.fixedDeltaTime;
        timerText.text = $"{gameTimer}";

        if (runesCollected >= 5)
        {
            // Trigger win
            return;
        }

        if (gameTimer > timeLimit)
        {
            // Trigger game over
            return;
        }
    }
}
