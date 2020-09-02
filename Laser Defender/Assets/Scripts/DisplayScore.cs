using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayScore : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;

    GameSession gameSession;

    void Start()
    {
        gameSession = FindObjectOfType<GameSession>();
    }
    
    void Update()
    {
        ShowCurrentScore();
    }

    private void ShowCurrentScore()
    {
        scoreText.text = gameSession.GetScore().ToString();
    }
}
