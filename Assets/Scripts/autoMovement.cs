using UnityEngine;

public class autoMovement : MonoBehaviour
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

    private GameObject moManager;
    private moveobjmanager moMangerScript;

    private GameManager gm;

    private bool resettingObj;

    private MovingObjectAudio movingAudio;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sp = GetComponent<SpriteRenderer>();
        moManager = transform.parent.gameObject;
        moMangerScript = moManager.GetComponent<moveobjmanager>();
        gm = GameManager.instance;

        movingAudio = GetComponent<MovingObjectAudio>();
    }
    private void Start()
    {   // cam.orthographicsize = height of camera
        // cam.aspect = aspect ratio = width/height
        //leftEdge = cam.transform.position.x - cam.orthographicSize * cam.aspect; // to get the leftedge of the camera => camera at the center position - height of camera * camera width/height
        //rightEdge = cam.transform.position.x + cam.orthographicSize * cam.aspect;

        //interval = Random.Range(5f, 10f);
        interval = Random.Range(3f, 6f);

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

        resettingObj = false;

        //movingAudio.PlayAudio(movingAudio.auto,1,0.8f);

    }

    private void Update()
    {

        

        StartTimer();

        if (timer > interval)
        {

            isMoving = true;
        }

        if (!isMoving)
        {
            movingAudio.PlayAudio(movingAudio.auto, 1, 0.8f);
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


        if (transform.position.x < leftEdge[gm.gameRound - 1] || transform.position.x > rightEdge[gm.gameRound - 1])
        {
            resettingObj = true;
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
    }

    private void ResetObject()
    {
        //movingAudio.PlayAudio(movingAudio.auto, 1, 0.8f);

        resettingObj = false;


        //interval = Random.Range(5f, 10f);
        interval = Random.Range(3f, 6f);
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
