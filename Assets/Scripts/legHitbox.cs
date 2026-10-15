using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class legHitbox : MonoBehaviour
{

    private Transform mask;
    private playerMovement pm;
    private Rigidbody2D rb;

    [HideInInspector] public bool fell;

    private float collisionObjectSizeX;

    private GameObject player;

    private playerAction pa;

    public event Action FellDownText;


    [SerializeField]private TextMeshProUGUI deathText;
    


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

        if (collision.gameObject.CompareTag("manhole") && pa.onKnockbackCooldown == false)
        {
            InfoUIManager.whoDied = gameObject.transform.parent.name;
            InfoUIManager.deathReason = " was injured!";
            deathText.text = InfoUIManager.whoDied + InfoUIManager.deathReason;
            FellDownText?.Invoke();

            mask = collision.gameObject.transform.Find("Mask");
            mask.gameObject.SetActive(true);
            fell = true;
            if (player.transform.position.x < collision.transform.position.x)
            {
                player.transform.position = new Vector3(player.transform.position.x + collisionObjectSizeX / 2, player.transform.position.y, player.transform.position.z); // so that player moves to the center of the manhole before falling down(makes falling look realistic)
            }
            else if (player.transform.position.x > collision.transform.position.x)
            {
                player.transform.position = new Vector3(player.transform.position.x - collisionObjectSizeX / 2, player.transform.position.y, player.transform.position.z); // so that player moves to the center of the manhole before falling down(makes falling look realistic)

            }

            GameManager.instance.pm1.enabled = false;
            GameManager.instance.pm2.enabled = false;
        }
        StartCoroutine(HealthZero(GameManager.instance.camMoveSpeed/2));
        
    }


    IEnumerator HealthZero(float duration)
    {
        yield return new WaitForSeconds(duration);

        if (player.name == "Player1")
        {
            playerStats.player1Health = 0;
        }
        else if (player.name == "Player2")
        {
            playerStats.player2Health = 0;
        }
    }

    private void Update()
    {


        if (fell && pa.onKnockbackCooldown == false)// onknockbackcooldown =false player should not fall to manhole when in knockbackcooldown
        {
            pm.enabled = false;


            // limits the falling to a boundary
            if (gameObject.transform.position.y >= -Mathf.Abs(Camera.main.orthographicSize * 2))
            {
                rb.linearVelocity = new Vector2(0, -2);


            }
            else
            {
                rb.linearVelocity = Vector2.zero;

            }





        }
    }

    void CallDeathText()
    {
        StartCoroutine(ShowAndHideText());
    }

    IEnumerator ShowAndHideText()
    {
        deathText.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        deathText.gameObject.SetActive(false);
        FellDownText -= CallDeathText;



    }


    private void OnEnable()
    {
        FellDownText += CallDeathText;
    }

    private void OnDisable()
    {
        FellDownText -= CallDeathText;
    }
}
