using JetBrains.Annotations;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerMovement : MonoBehaviour
{

    Rigidbody2D rb;
    SpriteRenderer sp;
    public float playerSpeed = 5f;
    private bool moveLeft = false, moveRight = false;
    private bool moveUp = false, moveDown = false;
    Vector2 direction;

    [SerializeField] private Animator animator;

    private static readonly int isRunXHash = Animator.StringToHash("isRunX");
    private static readonly int isRunXYHash = Animator.StringToHash("isRunXY");
    private static readonly int isHitHash = Animator.StringToHash("isHit");
    private static readonly int isBlockHash = Animator.StringToHash("isBlock");

    [SerializeField]private BoxCollider2D attackHitbox;
    [SerializeField]private BoxCollider2D blockHitbox;

    Vector3 scale;

    // control settings
    



    private void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        sp = gameObject.GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        scale =transform.localScale;
        scale.x = Mathf.Abs(scale.x);
    }

    void Update()
    {
        //player moves right
        if (Keyboard.current.rightArrowKey.isPressed)
        {
            //sp.flipX = false;
            scale.x = Mathf.Abs(scale.x);
            transform.localScale = scale;
            moveRight = true;

        }
        else if (!Keyboard.current.rightArrowKey.isPressed)
        {
            moveRight = false;


        }
        //player moves left
        if (Keyboard.current.leftArrowKey.isPressed)
        {
            //sp.flipX = true;
            //transform.localScale = new Vector3(-1, 1, 1);
            scale.x = -Mathf.Abs(scale.x);

            transform.localScale = scale;
            moveLeft = true;


        }
        else if (!Keyboard.current.leftArrowKey.isPressed)
        {
            moveLeft = false;

        }
        //player moves up
        if (Keyboard.current.upArrowKey.isPressed)
        {
            moveUp = true;
        }
        else if (!Keyboard.current.upArrowKey.isPressed)
        {
            moveUp = false;
        }
        //player moves down
        if (Keyboard.current.downArrowKey.isPressed)
        {
            moveDown = true;
        }
        else if (!Keyboard.current.downArrowKey.isPressed)
        {
            moveDown = false;
        }


    }

    private void FixedUpdate()
    {
        direction = Vector2.zero;
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


        rb.linearVelocity = direction.normalized * playerSpeed;

        //attack
        if (Keyboard.current.spaceKey.isPressed)
        {

            StartCoroutine(PlayerHitting());
        }
        // block mechanics
        if (Keyboard.current.leftCtrlKey.isPressed)
        {
            blockHitbox.enabled = true;
            rb.linearVelocity = Vector2.zero;
        }
        else
        {
            blockHitbox.enabled = false;
        }

        

        //animation function
        handleAnimation();
    }

    void handleAnimation()
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


        if (Keyboard.current.spaceKey.isPressed)
        {
            animator.SetBool(isHitHash, true);
        }

        if (Keyboard.current.leftCtrlKey.isPressed)
        {
            animator.SetBool(isBlockHash, true) ;
        }
        else
        {
            animator.SetBool(isBlockHash, false);

        }


    }


    IEnumerator PlayerHitting()
    {
        float animationTime = 0f;

        animator.SetBool(isHitHash, true);
        animationTime = animator.GetCurrentAnimatorStateInfo(0).length;

        attackHitbox.enabled = true;


        yield return new WaitForSeconds(animationTime);
        animator.SetBool(isHitHash, false);
        attackHitbox.enabled = false;
    }
    

}

