using UnityEngine;

public class legHitbox : MonoBehaviour
{

    private Transform mask;
    private playerMovement pm;
    private Rigidbody2D rb;

    [HideInInspector]public bool fell;

    private float collisionObjectSizeX;

    private GameObject player;

    
    private void Awake()
    {
        pm = transform.parent.gameObject.GetComponent<playerMovement>();
        rb = transform.parent.gameObject.GetComponent<Rigidbody2D>();
    
    }

    private void Start()
    {
        player = transform.parent.gameObject;
        fell = false;
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        collisionObjectSizeX = collision.bounds.size.x;

        if (collision.gameObject.CompareTag("manhole"))
        {
            mask= collision.gameObject.transform.Find("Mask");
            mask.gameObject.SetActive(true);
            fell = true;
            player.transform.position=new Vector3(player.transform.position.x+collisionObjectSizeX/2,player.transform.position.y,player.transform.position.z); // so that player moves to the center of the manhole before falling down(makes falling look realistic)
        }

    }

    private void Update()
    {
        if (fell)
        {
            pm.enabled = false;
            rb.linearVelocity = new Vector2(0, -2);
            
        }
    }

}
