using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class playerAction : MonoBehaviour
{


    public float pushForce;
    public BoxCollider2D attackHitbox;
    private playerStats ps;

    private Rigidbody2D rb;


    private float hitDamage;

    private float autoKnockbackAmount;
    private float autoKnockbackDelay;

    public bool onKnockbackCooldown;

    private playerMovement pm;

    private BoxCollider2D playerCollider;

    private SpriteRenderer sp;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        ps = GetComponent<playerStats>();
        pm = GetComponent<playerMovement>();
        playerCollider = GetComponent<BoxCollider2D>();
        sp = GetComponent<SpriteRenderer>();
    }
    private void Start()
    {
        pushForce = 4f;
        hitDamage = 10f;

        autoKnockbackDelay = 0.2f;
        autoKnockbackAmount = 20f;

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {

            Rigidbody2D rb = collision.attachedRigidbody;
            playerMovement pm = rb.GetComponent<playerMovement>();
            Transform child = collision.transform.Find("BlockHitbox");
            GameObject blockHitbox = child.gameObject;
            BoxCollider2D blockHitboxCollider = blockHitbox.GetComponent<BoxCollider2D>();

            playerStats ps = rb.GetComponent<playerStats>();



            // *** CHANGE FROM ADDFORCE TO LINEARVELOCITY VICE VERSA WHATS BEST FOR KNOCKBACK ***

            //rb.AddForce(new Vector2(Mathf.Sign(transform.localScale.x) * pushForce,0f), ForceMode2D.Impulse);


            // if the first player attacks the second player with block, the second player should only knockback/4 
            if (attackHitbox.enabled == true && blockHitboxCollider.enabled)
            {
                rb.linearVelocity = new Vector2(transform.localScale.x, 0f) * (pushForce / 4);
                pm.isKnockedBack = true;
            }
            // if damaged player is being attacked and his stamina 0 then dont move him
            else if (attackHitbox.enabled == true && ps.playerStamina == 0)
            {

                //rb.linearVelocity = Vector2.zero;
                rb.linearVelocity = new Vector2(transform.localScale.x, 0f) * (pushForce / 8);
                StartCoroutine(pm.EndKnockBack());
                ps.playerHealth -= hitDamage;



            }
            // if damaged player not blocking then he should knockback with full force if his stamina not ==0
            else if (attackHitbox.enabled == true && ps.playerStamina != 0)
            {


                rb.linearVelocity = new Vector2(transform.localScale.x, 0f) * pushForce;

                pm.isKnockedBack = true;


                ps.playerHealth -= hitDamage;
            }









        }






    }


    private void OnCollisionEnter2D(Collision2D collision)
    {


        if (collision.gameObject.CompareTag("knockbackEnemy"))
        {
            BoxCollider2D[] objectCollider = collision.gameObject.GetComponents<BoxCollider2D>();


            SpriteRenderer sp = collision.gameObject.GetComponent<SpriteRenderer>();
            // after hitting the player there should be a cooldown for the player to be hit again
            if (onKnockbackCooldown == false)
            {
                ps.playerHealth -= 10;

                if (sp.flipX == false)
                {
                    pm.onKnockback = true;
                    rb.AddForce(Vector2.right * autoKnockbackAmount, ForceMode2D.Impulse);
                    StartCoroutine(stopKnockback());
                    StartCoroutine(knockbackCooldown(objectCollider));
                }
                else
                {
                    pm.onKnockback = true;
                    rb.AddForce(Vector2.left * autoKnockbackAmount, ForceMode2D.Impulse);
                    StartCoroutine(stopKnockback());
                    StartCoroutine(knockbackCooldown(objectCollider));

                }
            }

        }
    }

    IEnumerator stopKnockback()
    {

        yield return new WaitForSeconds(autoKnockbackDelay);
        pm.onKnockback = false;



    }
    IEnumerator knockbackCooldown(BoxCollider2D[] col)
    {
        onKnockbackCooldown = true;
        foreach (BoxCollider2D c in col)
        {
            Physics2D.IgnoreCollision(playerCollider, c, true);
        }
        yield return new WaitForSeconds(3f);
        sp.enabled = true;

        foreach (BoxCollider2D c in col)
        {

            Physics2D.IgnoreCollision(playerCollider, c, false);
        }
        onKnockbackCooldown = false;

    }



    private void Update()
    {
        // if the player is in stamina refillcooldown then dont move the body unless the other player pushes it
        if (ps.refillCooldown == true)
        {
            rb.linearVelocity = Vector2.zero;

        }
    }

}
