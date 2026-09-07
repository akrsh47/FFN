using UnityEngine;

public class legHitbox : MonoBehaviour
{

    private Transform mask;
    private playerMovement pm;
    private Rigidbody2D rb;

    [HideInInspector]public bool fell;

    private float collisionObjectSizeX;

    private GameObject player;

    private playerAction pa;
    
    private void Awake()
    {
        pm = transform.parent.gameObject.GetComponent<playerMovement>();
        rb = transform.parent.gameObject.GetComponent<Rigidbody2D>();
        player = transform.parent.gameObject;
        pa = player.GetComponent<playerAction>();


    }

    private void Start()
    {
        fell = false;
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        collisionObjectSizeX = collision.bounds.size.x;
       
        if (collision.gameObject.CompareTag("manhole") && pa.onKnockbackCooldown==false)
        {
            mask= collision.gameObject.transform.Find("Mask");
            mask.gameObject.SetActive(true);
            fell = true;
            if (player.transform.position.x < collision.transform.position.x)
            {
                player.transform.position = new Vector3(player.transform.position.x + collisionObjectSizeX / 2, player.transform.position.y, player.transform.position.z); // so that player moves to the center of the manhole before falling down(makes falling look realistic)
            }
            else if(player.transform.position.x > collision.transform.position.x)
            {
                player.transform.position = new Vector3(player.transform.position.x - collisionObjectSizeX / 2, player.transform.position.y, player.transform.position.z); // so that player moves to the center of the manhole before falling down(makes falling look realistic)

            }
        }

    }

    private void Update()
    {


        if (fell && pa.onKnockbackCooldown==false)// onknockbackcooldown =false player should not fall to manhole when in knockbackcooldown
        {
            pm.enabled = false;
            rb.linearVelocity = new Vector2(0, -2);
            
        }
    }

}
