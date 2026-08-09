using System.Collections;
using TMPro;
using UnityEngine;

public class playerStats : MonoBehaviour
{
    public float playerHealth;
    public float playerStamina;

    public float hitStaminaCost;
    public float blockStaminaCost;
    public float staminaRefillCost;

    public TMP_Text healthText;
    public TMP_Text staminaText;
    Vector3 playerScreenPos;

    [SerializeField]private BoxCollider2D blockHitbox;

   

    private void Start()
    {
        playerHealth = 100f;
        playerStamina =100f;

        hitStaminaCost = 10f;
        blockStaminaCost= 5f;

        staminaRefillCost = 1f;
    }
    private void Update()
    {
        // Text coordinates and gameObject coordinates are different 
        // converting world coordinates to screen coordinates
        playerScreenPos = Camera.main.WorldToScreenPoint(gameObject.transform.position);


        healthText.text = playerHealth.ToString();
        staminaText.text = Mathf.RoundToInt(playerStamina).ToString();
        healthText.color = Color.red;
        //health on top of player
        healthText.transform.position = new Vector2(playerScreenPos.x + 100f, playerScreenPos.y + 40f);
        staminaText.transform.position = new Vector2(playerScreenPos.x + 100f, playerScreenPos.y + 20f);

        // stats must be greater than or equal to 0
        // and less than or equal to 100
        //health
        if (playerHealth <= 0)
        {
            playerHealth = 0;
        }

        //stamina
        if (playerStamina <= 0)
        {
            playerStamina = 0;
        }
        if (playerStamina >= 100)
        {
            playerStamina = 100;
        }


        RefillStamina();
    }


    void RefillStamina()
    {
        if (blockHitbox.enabled == false)
        {
            playerStamina += staminaRefillCost * Time.deltaTime;
        }
        else
        {
            playerStamina -= blockStaminaCost * Time.deltaTime;
        }

    }

}
