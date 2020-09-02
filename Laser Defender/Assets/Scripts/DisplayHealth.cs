using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayHealth : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI healthText;

    Player player;

    void Start()
    {
        player = FindObjectOfType<Player>();
    }
    
    void Update()
    {
        ShowPlayerHealth();
    }

    private void ShowPlayerHealth()
    {
        healthText.text = player.GetHealth().ToString();
    }
}
