using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int gameRound = 1;
    public int whoWon;

    [HideInInspector] public float camLeftEdge;
    [HideInInspector] public float camRightEdge;

    private float padding = 2f;


    private float timer = 0f;

    [HideInInspector] public Vector3 secondCamPos;
    [HideInInspector] public Vector3 thirdCamPos;

    private Vector3 currentCamPos;

    private float camMoveSpeed = 4f;

    [SerializeField] private GameObject player1;
    [SerializeField] private GameObject player2;
    [SerializeField] private GameObject player1Actor;
    [SerializeField] private GameObject player2Actor;




    private Vector3 p1LevelPos;
    private Vector3 p2LevelPos;

    private playerMovement pm1;
    private playerMovement pm2;

    private playerAction pa1;
    private playerAction pa2;

    [HideInInspector] public float halfHeight;


    public static event Action resetObjects;

    private void Awake()
    {



        // no duplicate gameManagers
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        pm1 = player1.GetComponent<playerMovement>();
        pm2 = player2.GetComponent<playerMovement>();
        pa1 = player1.GetComponent<playerAction>();
        pa2 = player2.GetComponent<playerAction>();

        halfHeight = Camera.main.orthographicSize * Camera.main.aspect;

        camLeftEdge = Camera.main.transform.position.x - halfHeight;
        camRightEdge = Camera.main.transform.position.x + halfHeight;


        secondCamPos = new Vector3(camRightEdge * 2 + padding, Camera.main.transform.position.y, Camera.main.transform.position.z);
        thirdCamPos = new Vector3(secondCamPos.x * 2 + padding, Camera.main.transform.position.y, Camera.main.transform.position.z);


    }

    private void Update()
    {

        if (gameRound < 4)
        {
            UpdateRound();
        }


    }

    public IEnumerator MoveCamera(float duration, Vector3 startPos, Vector3 endPos)
    {
        yield return new WaitForSeconds(1f); // Wait a time then move the camera
        timer = 0f;
        while (timer < duration)
        {
            timer += Time.deltaTime;
            float t = timer / duration;

            Camera.main.transform.position = Vector3.Lerp(startPos, endPos, t);

            yield return null;
        }
        Camera.main.transform.position = endPos;

        player1.SetActive(true);
        player1.GetComponent<SpriteRenderer>().enabled = true;
        player2.SetActive(true);
        player2.GetComponent<SpriteRenderer>().enabled = true;
        p1LevelPos = new Vector3(currentCamPos.x - 3 * padding, -1.5f, 0);
        p2LevelPos = new Vector3(currentCamPos.x + 3 * padding, -1.5f, 0);
        player1.transform.position = p1LevelPos;
        player2.transform.position = p2LevelPos;

        player1.transform.localScale = player1Actor.transform.localScale;
        player2.transform.localScale = player2Actor.transform.localScale;

        pm1.onKnockback = false;
        pa1.onKnockbackCooldown = false;
        pa2.onKnockbackCooldown = false;
        pm2.onKnockback = false;
        pm1.playerCanMove = true;
        pm2.playerCanMove = true;
        //scale.x = -Mathf.Abs(scale.x);

        //transform.localScale = scale;

        building.pauseGarbage = false;
        resetObjects?.Invoke();


    }


    private void UpdateRound()
    {
        if (gameRound == 3)
        {
            return;
        }

        if (playerStats.player1Health <= 0)
        {
            StartCoroutine(EnableActors());


            gameRound++;
            UpdateCurrentCam();
            whoWon += -1;
            playerStats.player1Health = 100f;
            building.pauseGarbage = true;
            StartCoroutine(MoveCamera(camMoveSpeed, Camera.main.transform.position, currentCamPos));



        }
        if (playerStats.player2Health <= 0)
        {
            StartCoroutine(EnableActors());




            gameRound++;
            UpdateCurrentCam();
            whoWon += 1;
            playerStats.player2Health = 100f;
            building.pauseGarbage = true;
            StartCoroutine(MoveCamera(camMoveSpeed, Camera.main.transform.position, currentCamPos));

        }


    }

    private void UpdateCurrentCam()
    {
        if (gameRound == 2)
        {
            currentCamPos = secondCamPos;
        }
        if (gameRound == 3)
        {
            currentCamPos = thirdCamPos;
        }
    }


    IEnumerator EnableActors()
    {
        yield return new WaitForSeconds(1f);
        player2Actor.transform.position = player2.transform.position;

        player1Actor.transform.position = player1.transform.position;
        player2Actor.SetActive(true);
        player1Actor.SetActive(true);
        player1.SetActive(false);
        player2.SetActive(false);
    }


}
