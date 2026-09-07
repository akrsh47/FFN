using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class copmovement : MonoBehaviour
{
    private float interval;
    private float timer;

    [SerializeField] private Camera cam;

    private bool isMoving;

    private Rigidbody2D rb;

    [SerializeField] private float objectSpeed;

    private float leftEdge;
    private float rightEdge;
    private float padding = 20f;

    private Vector2 direction = new Vector2(0, 0);
    private float randomDirection;

    private SpriteRenderer sp;

    [SerializeField] private GameObject redLight;
    [SerializeField] private GameObject blueLight;

    private float lightTimer;

    private GameObject moManager;
    private moveobjmanager moMangerScript;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sp = GetComponent<SpriteRenderer>();
        moManager = transform.parent.gameObject;
        moMangerScript = moManager.GetComponent<moveobjmanager>();

    }
    private void Start()
    {   // cam.orthographicsize = height of camera
        // cam.aspect = aspect ratio = width/height
        leftEdge = cam.transform.position.x - cam.orthographicSize * cam.aspect; // to get the leftedge of the camera => camera at the center position - height of camera * camera width/height
        rightEdge = cam.transform.position.x + cam.orthographicSize * cam.aspect;

        //interval = Random.Range(5f, 10f);
        interval = Random.Range(0f, 1f);





        isMoving = false;
        objectSpeed = 10f;



        randomDirection = Random.Range(0, 2);

        if (randomDirection == 0)
        {

            transform.position = new Vector3(leftEdge - padding, transform.position.y, transform.position.z);
            direction = Vector2.right;
            sp.flipX = false;

        }
        else
        {
            transform.position = new Vector3(rightEdge + padding, transform.position.y, transform.position.z);
            direction = Vector2.left;
            sp.flipX = true;



        }

        lightTimer = 0f;
        redLight.SetActive(true);
        blueLight.SetActive(false);
    }

    private void Update()
    {

        StartTimer();
        BlinkLight();

        if (timer > interval)
        {

            isMoving = true;
        }

        if (randomDirection == 0)
        {

            if (transform.position.x > rightEdge + padding)
            {
                isMoving = false;
                ResetObject();
            }
        }
        else
        {
            if (transform.position.x < leftEdge - padding)
            {
                isMoving = false;
                ResetObject();
            }
        }




    }

    private void FixedUpdate()
    {
        if (isMoving)
        {

            rb.linearVelocity = objectSpeed * direction;
        }
        else
        {
            rb.linearVelocity = Vector2.zero;

        }
    }

    private void StartTimer()
    {
        timer += Time.deltaTime;
        lightTimer += Time.deltaTime;
    }

    private void ResetObject()
    {
        //interval = Random.Range(5f, 10f);
        interval = Random.Range(0f, 1f);
        timer = 1f;
        randomDirection = Random.Range(0, 2);

        if (randomDirection == 0)
        {
            transform.position = new Vector3(leftEdge - padding, transform.position.y, transform.position.z);
            direction = Vector2.right;
            sp.flipX = false;
        }
        else
        {
            transform.position = new Vector3(rightEdge + padding, transform.position.y, transform.position.z);
            direction = Vector2.left;
            sp.flipX = true;

        }

        moMangerScript.ResetMovingObject();

    }

    void BlinkLight()
    {
        if (lightTimer >= .5f)
        {
            redLight.SetActive(!redLight.activeSelf);
            blueLight.SetActive(!blueLight.activeSelf);
            lightTimer = 0;
        }


    }
}
