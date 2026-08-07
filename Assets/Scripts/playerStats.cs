using TMPro;
using UnityEngine;

public class playerStats : MonoBehaviour
{
    public float playerHealth;
    public float playerStamina;

    public TMP_Text healthText;
    public TMP_Text staminaText;
    Vector3 playerScreenPos;

    private void Start()
    {
        playerHealth = 100f;
        playerStamina = 100f;
    }
    private void Update()
    {
        // Text coordinates and gameObject coordinates are different 
        // converting world coordinates to screen coordinates
        playerScreenPos=Camera.main.WorldToScreenPoint(gameObject.transform.position);
        

        healthText.text =playerHealth.ToString();
        staminaText.text=playerStamina.ToString();
        healthText.color = Color.red;
        //health on top of player
        healthText.transform.position=new Vector2(playerScreenPos.x+100f,playerScreenPos.y+40f);
        staminaText.transform.position=new Vector2(playerScreenPos.x+100f,playerScreenPos.y+20f);
        if(playerHealth <= 0)
        {
            playerHealth = 0;
        }
    }


}
