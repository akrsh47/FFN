using System;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using Random = UnityEngine.Random;

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
    private int randomDirection;

    private SpriteRenderer sp;

    [SerializeField] private GameObject redLight;
    [SerializeField] private GameObject blueLight;

    private float lightTimer;

    private GameObject moManager;
    private moveobjmanager moMangerScript;

    private GameManager gm;

    private bool resettingObj;

    private bool stopppedThisTime;
    private bool stoppedMoving;

    [SerializeField] private float stopDuration;

    public static bool checkingViolence;

    [SerializeField] private GameObject cop;

    [SerializeField]private GameObject cautionText;

    public event Action<string> copCaught;

    [SerializeField]private TextMeshProUGUI caughtText;
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

        stopppedThisTime = false;
        stoppedMoving = false;

        stopDuration = Random.Range(2f, 5f);

        checkingViolence = false;




        // checking the flip of cop
        if (sp.flipX)
        {

            cop.transform.localScale = new Vector3(-Mathf.Abs(cop.transform.localScale.x), cop.transform.localScale.y, cop.transform.localScale.z);
            cop.transform.position = new Vector3(gameObject.transform.position.x - 4, gameObject.transform.position.y + 1, gameObject.transform.position.z);
        }
        else
        {
            Debug.Log("right");
            cop.transform.localScale = new Vector3(Mathf.Abs(cop.transform.localScale.x), cop.transform.localScale.y, cop.transform.localScale.z);
            cop.transform.position = new Vector3(gameObject.transform.position.x + 4, gameObject.transform.position.y + 1, gameObject.transform.position.z);


        }

        
    }

    private void Update()
    {



        StartTimer();
        BlinkLight();

        CopChecking();

        if (timer > interval && !stoppedMoving)
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

        stopDuration = Random.Range(2f, 5f);


        //interval = Random.Range(5f, 10f);
        interval = Random.Range(0f, 1f);
        timer = 1f;
        randomDirection = Random.Range(0, 2);

        if (randomDirection == 0)
        {

            transform.position = new Vector3(leftEdge[gm.gameRound - 1], transform.position.y, transform.position.z);
            direction = Vector2.right;
            sp.flipX = false;
            stopppedThisTime = false;
        }
        else
        {
            transform.position = new Vector3(rightEdge[gm.gameRound - 1], transform.position.y, transform.position.z);
            direction = Vector2.left;
            sp.flipX = true;
            stopppedThisTime = false;


        }

        // checking the flip of cop
        if (sp.flipX)
        {

            cop.transform.localScale = new Vector3(-Mathf.Abs(cop.transform.localScale.x), cop.transform.localScale.y, cop.transform.localScale.z);
            cop.transform.position = new Vector3(gameObject.transform.position.x - 4, gameObject.transform.position.y + 1, gameObject.transform.position.z);
        }
        else
        {
            Debug.Log("right");
            cop.transform.localScale = new Vector3(Mathf.Abs(cop.transform.localScale.x), cop.transform.localScale.y, cop.transform.localScale.z);
            cop.transform.position = new Vector3(gameObject.transform.position.x + 4, gameObject.transform.position.y + 1, gameObject.transform.position.z);


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

        copCaught += HandleShowCaughtText;
    }

    private void OnDisable()
    {
        GameManager.resetObjects -= pauseMovement;
        copCaught -= HandleShowCaughtText;

    }




    // cop car property
    void CopChecking()
    {
        

        if (stopppedThisTime == false)
        {

            if (gameObject.transform.position.x >= Camera.main.transform.position.x && randomDirection == 0 && !stopppedThisTime)
            {


                stoppedMoving = true;
                isMoving = false;
                rb.linearVelocity = Vector2.zero;
                stopppedThisTime = true;
                if (gameObject.activeSelf)
                {
                    StartCoroutine(WaitingForMovement(stopDuration));
                }



            }

            if (gameObject.transform.position.x <= Camera.main.transform.position.x && randomDirection == 1 && !stopppedThisTime)
            {


                stoppedMoving = true;
                rb.linearVelocity = Vector2.zero;
                isMoving = false;
                stopppedThisTime = true;
                if (gameObject.activeSelf)
                {
                    StartCoroutine(WaitingForMovement(stopDuration));
                }


            }


            if (checkingViolence)
            {
                playerMovement.OnViolence -= CheckWhoViolence;
                playerMovement.OnViolence += CheckWhoViolence;
            }


        }


    }

    void CheckWhoViolence(string name)
    {
        if (name == "Player1")
        {
            playerStats.player1Health = 0;
            copCaught?.Invoke(name);
        }
        else if (name == "Player2")
        {
            playerStats.player2Health = 0;
            copCaught?.Invoke(name);

        }


    }

    IEnumerator WaitingForMovement(float duration)
    {
        checkingViolence = true;
        cop.SetActive(true);
        cautionText.SetActive(true);
        yield return new WaitForSeconds(duration);
        cautionText.SetActive(false);
        cop.SetActive(false);
        checkingViolence = false;
        rb.linearVelocity = objectSpeed * direction;
        isMoving = true;
        stoppedMoving = false;
    }


    IEnumerator ShowAndHideCaughtText(float duration)
    {
        caughtText.gameObject.SetActive(true);
        yield return new WaitForSeconds(duration);
        caughtText.gameObject.SetActive(false);

    }

    void HandleShowCaughtText(string name)
    {
        InfoUIManager.whoDied = name;
        InfoUIManager.deathReason = " was Caught by Cops";

        caughtText.text = name + InfoUIManager.deathReason;
        StartCoroutine(ShowAndHideCaughtText(2f));

    }
    


}
