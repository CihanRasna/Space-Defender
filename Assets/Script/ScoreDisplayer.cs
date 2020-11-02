using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplayer : MonoBehaviour
{
    private Text scoreText;

    private GameManager gameManager;
    
    
    void Start()
    {
        scoreText = GetComponent<Text>();
        gameManager = FindObjectOfType<GameManager>();
    }

    
    void Update()
    {
        scoreText.text = gameManager.GetScore().ToString();
    }
}
