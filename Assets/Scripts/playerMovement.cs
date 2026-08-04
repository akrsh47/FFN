using UnityEngine;
using UnityEngine.InputSystem;

public class playerMovement : MonoBehaviour
{

    Rigidbody2D rb;
    public float playerSpeed = 5f;
    private bool moveLeft = false, moveRight = false;
    private bool moveUp = false, moveDown = false;
    Vector2 direction;



    private void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //player moves right
        if (Keyboard.current.rightArrowKey.isPressed)
        {

            moveRight = true;
        }
        else if (!Keyboard.current.rightArrowKey.isPressed)
        {
            moveRight = false;
        }
        //player moves left
        if (Keyboard.current.leftArrowKey.isPressed)
        {
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
    }
}
