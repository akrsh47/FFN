using JetBrains.Annotations;
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



    private void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        sp = gameObject.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        //player moves right
        if (Keyboard.current.rightArrowKey.isPressed)
        {
            sp.flipX = false;
            moveRight = true;

        }
        else if (!Keyboard.current.rightArrowKey.isPressed)
        {
            moveRight = false;


        }
        //player moves left
        if (Keyboard.current.leftArrowKey.isPressed)
        {
            sp.flipX = true;
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
        if ((direction.y==1)&&(direction.x==1||direction.x==-1))
        {
            animator.SetBool(isRunXYHash, true);
        }
        else if(direction.y==1){
            animator.SetBool(isRunXYHash, true);

        }
        else
        {
            animator.SetBool(isRunXYHash, false);
        }
    }
}
