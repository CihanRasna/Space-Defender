using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplayer : MonoBehaviour
{
    private Text healthText;
    private Player player;

    
    
    
    void Start()
    {
        healthText = GetComponent<Text>();
        player = FindObjectOfType<Player>();
    }

    
    void Update()
    {
        healthText.text = player.GetHealth().ToString();
    }
}
