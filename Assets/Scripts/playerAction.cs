using System.Collections;
using UnityEngine;

public class playerAction : MonoBehaviour
{

    
    public float pushForce;
    public BoxCollider2D attackHitbox;

    private void Start()
    {
        pushForce = 4f;
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



            // *** CHANGE FROM ADDFORCE TO LINEARVELOCITY VICE VERSA WHATS BEST FOR KNOCKBACK ***

            //rb.AddForce(new Vector2(Mathf.Sign(transform.localScale.x) * pushForce,0f), ForceMode2D.Impulse);
            

            // if the first player attacks the second player, the second player should only knockback/4 
            if (attackHitbox.enabled == true && blockHitboxCollider.enabled)
            {
                rb.linearVelocity = new Vector2(transform.localScale.x, 0f) * (pushForce/4);
                pm.isKnockedBack = true;
            }
            // if the first player attacks the second player,the second player should knockback with full force
            else if (attackHitbox.enabled == true)
            {
                rb.linearVelocity = new Vector2(transform.localScale.x, 0f) * pushForce;
                pm.isKnockedBack = true;
            }
            






        }
    }


    


}
