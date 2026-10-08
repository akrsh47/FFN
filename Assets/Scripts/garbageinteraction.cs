using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class garbageinteraction : MonoBehaviour
{

    [SerializeField] private Animator animator;
    private static readonly int isHitGround = Animator.StringToHash("isHitGround");
    private Vector3 startingPos;
    [SerializeField] private Sprite startingSprite;

    private SpriteRenderer sp;

    private building buildingScript;
    private playerStats ps;

    public static readonly float garbageDamage = 20f;

    private bool garbageOnGround;

    [HideInInspector] public bool hitPlayer;
    [HideInInspector] public bool endingHitPlayer;// guard condition

    private playerMovement pm;

    private PolygonCollider2D garbageCollider;

    private void Awake()
    {
        startingPos = gameObject.transform.position;
        sp = gameObject.GetComponent<SpriteRenderer>();
        buildingScript = transform.parent.gameObject.GetComponent<building>();
        garbageCollider = GetComponent<PolygonCollider2D>();
    }

    private void Start()
    {
        garbageOnGround = false;
        hitPlayer = false;
        endingHitPlayer = false;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("footpath") || collision.gameObject.CompareTag("Player"))
        {
            if (collision.gameObject.CompareTag("footpath"))
            {
                garbageOnGround = true;
            }
            garbageFell(collision);

        }

    }

    

    IEnumerator ResetPositon()
    {
        yield return new WaitForSeconds(1f);
        garbageCollider.enabled = true;
        garbageOnGround = false;
        sp.sprite = startingSprite;
        gameObject.SetActive(false);
        gameObject.transform.position = startingPos;
        transform.rotation = Quaternion.identity;
        buildingScript.randomGarbage = UnityEngine.Random.Range(0, buildingScript.garbagePerBuilding);

    }

    void garbageFell(Collision2D col)
    {

        animator.SetTrigger(isHitGround);
        transform.rotation = Quaternion.identity;
        if (col.gameObject.CompareTag("Player"))
        {
            hitPlayer = true;
            ps = col.gameObject.GetComponent<playerStats>();

            pm = col.gameObject.GetComponent<playerMovement>();

            if (garbageOnGround == false)
            {
                if (hitPlayer && !endingHitPlayer)
                {
                    pm.PlayerStunAfterDamage();

                    // DISABLE COLLIDER AFTER HITTING IF THE COLLISION NOT WORKING AS EXPECTED 

                    //garbageCollider.enabled = false; 

                    ps.TakeDamage(garbageDamage);
                }
            }






        }
        StartCoroutine(ResetPositon());
       

    }





}
