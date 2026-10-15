using System;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class playerStats : MonoBehaviour
{
    public static float player1Health;
    public static float player2Health;

    private float maxHealth;
    public float playerStamina;

    public float hitStaminaCost;
    public float blockStaminaCost;
    public float staminaRefillCost;

   

    [SerializeField] private BoxCollider2D blockHitbox;

    [HideInInspector] public bool refillCooldown;
    private float refillCooldownTime;

    private playerMovement pm;
    private playerAction pa;
    private Rigidbody2D rb;

    public static event Action<string> DamageEffect;

    private void Awake()
    {
        pm = GetComponent<playerMovement>();
        rb = GetComponent<Rigidbody2D>();

        player1Health = 100f;
        player2Health = 100f;
        maxHealth = 100f;
    }

    private void Start()
    {
        playerStamina = 100f;

        hitStaminaCost = 10f;
        blockStaminaCost = 5f;

        staminaRefillCost = 1f;

        refillCooldown = false;
        refillCooldownTime = 3f;
    }
    private void Update()
    {
        

        //stamina
        if (playerStamina <= 0 && refillCooldown == false)
        {
            playerStamina = 0;
            pm.playerCanMove = false;
            rb.linearVelocity = Vector2.zero;
            StartCoroutine(RefillCooldownTimer());

        }
        


        RefillStamina();
       


    }


    void RefillStamina()
    {
        if (blockHitbox.enabled == false && refillCooldown == false)
        {
            playerStamina += staminaRefillCost * Time.deltaTime;
        }
        else if (blockHitbox.enabled == true && refillCooldown == false)
        {

            playerStamina -= blockStaminaCost * Time.deltaTime;
        }

    }

    IEnumerator RefillCooldownTimer()
    {
        refillCooldown = true;
        yield return new WaitForSeconds(refillCooldownTime);
        refillCooldown = false;
        pm.playerCanMove = true;
        playerStamina = 1f;


    }

    public void  TakeDamage(float damage)
    {
        //Debug.Log(GameManager.instance.gameRound+" gameround");
        //Debug.Log(GameManager.instance.whoWon+" whowon");

        if (gameObject.name == "Player1")
        {
            player1Health -= damage;
            player1Health = Mathf.Clamp(player1Health, 0, maxHealth);
            DamageEffect?.Invoke(gameObject.name);
        }
        if (gameObject.name == "Player2")
        {
            player2Health -= damage;
            player2Health = Mathf.Clamp(player2Health, 0, maxHealth);
            DamageEffect?.Invoke(gameObject.name);
        }
    }


}
