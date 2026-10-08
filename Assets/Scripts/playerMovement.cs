using JetBrains.Annotations;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerMovement : MonoBehaviour
{

    Rigidbody2D rb;
    SpriteRenderer sp;
    public float playerSpeed = 5f;
    private bool moveLeft = false, moveRight = false;
    private bool moveUp = false, moveDown = false;
    [HideInInspector] public Vector2 direction;

    [SerializeField] private Animator animator;

    private static readonly int isRunXHash = Animator.StringToHash("isRunX");
    private static readonly int isRunXYHash = Animator.StringToHash("isRunXY");
    private static readonly int isHitHash = Animator.StringToHash("isHit");
    private static readonly int isBlockHash = Animator.StringToHash("isBlock");
    private static readonly int isHurtHash = Animator.StringToHash("isHurt");
    private static readonly int isTiredHash = Animator.StringToHash("isTired");


    [SerializeField] private BoxCollider2D attackHitbox;
    [SerializeField] private BoxCollider2D blockHitbox;

    Vector3 scale;

    // control settings
    public Key[] movements;
    public Key[] actions;


    [HideInInspector] public bool isKnockedBack;
    private bool isEndingKnockback = false; // guard condition for knockback so that it doesnt run co routine every frame
    playerStats stats;

    public bool playerCanMove;

    private garbageinteraction garbageScript;

    public bool onKnockback;
    private void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        sp = gameObject.GetComponent<SpriteRenderer>();
        stats = gameObject.GetComponent<playerStats>();
    }

    private void Start()
    {
        scale = transform.localScale;
        scale.x = Mathf.Abs(scale.x);

        if (gameObject.name == "Player1")
        {
            //initialize controls
            movements = new Key[]
            {
            Key.D,
            Key.A,
            Key.W,
            Key.S,
            };

            actions = new Key[] {
                Key.LeftCtrl,
                Key.LeftShift

            };
        }

        if (gameObject.name == "Player2")
        {

            scale.x = -Mathf.Abs(scale.x);

            transform.localScale = scale;
            //initialize controls
            movements = new Key[]
            {
            Key.RightArrow,
            Key.LeftArrow,
            Key.UpArrow,
            Key.DownArrow,
            };

            actions = new Key[] {
                Key.NumpadPlus,
                Key.NumpadEnter

            };
        }



        isKnockedBack = false;
        isEndingKnockback = false;

        playerCanMove = true;

        onKnockback = false;

    }

    void Update()
    {

        //animation function
        HandleAnimation();


        if (onKnockback == false)
        {
            direction = Vector2.zero;
        }
        else if (onKnockback == true)
        {
            return;
        }
        // moving right
        if (moveRight)
        {
            direction.x = 1;
        }
        // moving left
        if (moveLeft)
        {
            direction.x = -1;
        }

        // moving up
        if (moveUp)
        {
            direction.y = 1;

        }

        // moving down
        if (moveDown)
        {
            direction.y = -1;
        }

        //player moves right
        if (Keyboard.current[movements[0]].isPressed)
        {
            //sp.flipX = false;
            scale.x = Mathf.Abs(scale.x);
            transform.localScale = scale;
            moveRight = true;

        }
        else if (!Keyboard.current[movements[0]].isPressed)
        {
            moveRight = false;


        }
        //player moves left
        if (Keyboard.current[movements[1]].isPressed)
        {

            scale.x = -Mathf.Abs(scale.x);

            transform.localScale = scale;
            moveLeft = true;


        }
        else if (!Keyboard.current[movements[1]].isPressed)
        {
            moveLeft = false;

        }
        //player moves up
        if (Keyboard.current[movements[2]].isPressed)
        {
            moveUp = true;
        }
        else if (!Keyboard.current[movements[2]].isPressed)
        {
            moveUp = false;
        }
        //player moves down
        if (Keyboard.current[movements[3]].isPressed)
        {
            moveDown = true;
        }
        else if (!Keyboard.current[movements[3]].isPressed)
        {
            moveDown = false;
        }

        //attack
        if (Keyboard.current[actions[0]].wasPressedThisFrame)
        {
            // only make the player use stamina if its above 10f
            // dont make player use attack and block at same
            if (stats.playerStamina >= stats.hitStaminaCost && blockHitbox.enabled == false)
            {

                stats.playerStamina -= stats.hitStaminaCost;
                StartCoroutine(PlayerHitting());

            }

        }
        // block mechanics
        if (Keyboard.current[actions[1]].isPressed)
        {
            //stats.playerStamina-=20f*Time.deltaTime;
            blockHitbox.enabled = true;

            // checking if player is not being knockedback then in block state the player must not move
            if (isKnockedBack == false)
            {

                rb.linearVelocity = Vector2.zero;


            }

        }
        else
        {
            blockHitbox.enabled = false;
        }




    }

    private void FixedUpdate()
    {
        if (onKnockback)
        {
            return;
        }

        // checking if player is not being knockedback and also not in block state, player can move freely
        if (isKnockedBack == false && blockHitbox.enabled == false && playerCanMove)
        {

            rb.linearVelocity = direction.normalized * playerSpeed;


        }
        else if (isKnockedBack == true && !isEndingKnockback)
        {

            StartCoroutine(EndKnockBack());

        }


    }

    void HandleAnimation()
    {
        // running animations
        if (direction.x == 1 || direction.x == -1 || direction.y == -1)
        {
            animator.SetBool(isRunXHash, true);

        }
        else
        {
            animator.SetBool(isRunXHash, false);

        }
        if ((direction.y == 1) && (direction.x == 1 || direction.x == -1))
        {
            animator.SetBool(isRunXYHash, true);
        }
        else if (direction.y == 1)
        {
            animator.SetBool(isRunXYHash, true);

        }
        else
        {
            animator.SetBool(isRunXYHash, false);
        }

        // actions animations

        if (Keyboard.current[actions[1]].isPressed)
        {
            animator.SetBool(isBlockHash, true);
        }
        else
        {
            animator.SetBool(isBlockHash, false);

        }

        // stats animations
        if (stats.refillCooldown == true && isKnockedBack == false)
        {

            animator.SetBool(isTiredHash, true);

        }
        else if (stats.refillCooldown == false)
        {
            animator.SetBool(isTiredHash, false);

        }

        if (onKnockback)
        {
            animator.SetTrigger(isHurtHash);
        }


    }


    IEnumerator PlayerHitting()
    {
        animator.SetTrigger(isHitHash);
        attackHitbox.enabled = true;



        yield return new WaitForSeconds(0.1f);
        attackHitbox.enabled = false;
    }


    public IEnumerator EndKnockBack()
    {
        isEndingKnockback = true;
        animator.SetTrigger(isHurtHash);
        yield return new WaitForSeconds(0.2f);

        isKnockedBack = false;
        isEndingKnockback = false;


    }

    public void PlayerStunAfterDamage()
    {
        animator.SetTrigger(isHurtHash);
        playerCanMove = false;
    }
    public void RemoveStun()
    {
        rb.linearDamping = 0f;
        if (stats.playerStamina > 0)
        {
            playerCanMove = true;

        }
    }

    IEnumerator DelayAfterGarbageStun()
    {
        yield return new WaitForSeconds(1f);
        if (stats.playerStamina > 0)
        {
            RemoveStun();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("enemy"))
        {
            rb.linearDamping = 10f;

            garbageScript = collision.gameObject.GetComponent<garbageinteraction>();
            garbageScript.endingHitPlayer = true;

            StartCoroutine(DelayAfterGarbageStun());

            garbageScript.endingHitPlayer = false;
            garbageScript.hitPlayer = true;



        }
    }



}

