using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class healthBar : MonoBehaviour
{
    
    private float maxHealth;

    [SerializeField]private Image healthBarImage;

    [SerializeField]private GameObject target;

    private void Start()
    {
        
        maxHealth = 100f;
    }

    private void Update()
    {
        if (target.name == "Player1")
        {
            healthBarImage.fillAmount = playerStats.player1Health / maxHealth;
        }
        if(target.name == "Player2")
        {
            healthBarImage.fillAmount = playerStats.player2Health / maxHealth;

        }
    }
}
