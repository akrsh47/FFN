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

    private float[] leftEdge;
    private float[] rightEdge;
    private float padding = 20f;

    private Vector2 direction = new Vector2(0, 0);
    private float randomDirection;

    private SpriteRenderer sp;

    [SerializeField] private GameObject redLight;
    [SerializeField] private GameObject blueLight;

    private float lightTimer;

    private GameObject moManager;
    private moveobjmanager moMangerScript;

    private GameManager gm;

    private bool resettingObj;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sp = GetComponent<SpriteRenderer>();
        moManager = transform.parent.gameObject;
        moMangerScript = moManager.GetComponent<moveobjmanager>();
        gm = GameManager.instance;
    }
    private void Start()
    {
        leftEdge = new float[3]
        {
            gm.camLeftEdge-padding,
            gm.secondCamPos.x-gm.halfHeight-padding,
            gm.thirdCamPos.x-gm.halfHeight-padding
        };
        rightEdge = new float[3]
        {
            gm.camRightEdge+padding,
            gm.secondCamPos.x+gm.halfHeight+padding,
            gm.thirdCamPos.x+gm.halfHeight+padding
        };

        //interval = Random.Range(5f, 10f);
        interval = Random.Range(0f, 1f);





        isMoving = false;
        objectSpeed = 10f;



        randomDirection = Random.Range(0, 2);

        if (randomDirection == 0)
        {

            transform.position = new Vector3(leftEdge[gm.gameRound - 1], transform.position.y, transform.position.z);
            direction = Vector2.right;
            sp.flipX = false;

        }
        else
        {
            transform.position = new Vector3(rightEdge[gm.gameRound - 1], transform.position.y, transform.position.z);
            direction = Vector2.left;
            sp.flipX = true;



        }

        lightTimer = 0f;
        redLight.SetActive(true);
        blueLight.SetActive(false);

        resettingObj = false;
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

            if (transform.position.x > rightEdge[gm.gameRound - 1])
            {
                isMoving = false;
                ResetObject();
            }
        }
        else
        {
            if (transform.position.x < leftEdge[gm.gameRound - 1])
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
        resettingObj = false;


        //interval = Random.Range(5f, 10f);
        interval = Random.Range(0f, 1f);
        timer = 1f;
        randomDirection = Random.Range(0, 2);

        if (randomDirection == 0)
        {
            transform.position = new Vector3(leftEdge[gm.gameRound - 1], transform.position.y, transform.position.z);
            direction = Vector2.right;
            sp.flipX = false;
        }
        else
        {
            transform.position = new Vector3(rightEdge[gm.gameRound - 1], transform.position.y, transform.position.z);
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

    void pauseMovement()
    {
        if (resettingObj)
        {
            ResetObject();
        }
    }


    private void OnEnable()
    {
        GameManager.resetObjects += pauseMovement;
    }

    private void OnDisable()
    {
        GameManager.resetObjects -= pauseMovement;
    }

}
