using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
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

    [HideInInspector] public float camMoveSpeed = 4f;

    [SerializeField] private GameObject player1;
    [SerializeField] private GameObject player2;
    [SerializeField] private GameObject player1Actor;
    [SerializeField] private GameObject player2Actor;




    private Vector3 p1LevelPos;
    private Vector3 p2LevelPos;

    [HideInInspector] public playerMovement pm1;
    [HideInInspector] public playerMovement pm2;

    private playerAction pa1;
    private playerAction pa2;

    [HideInInspector] public float halfHeight;


    public static event Action resetObjects;

    [SerializeField] private GameObject trophy;
    [SerializeField] private GameObject trophyParent1;
    [SerializeField] private GameObject trophyParent2;


    [SerializeField] private GameObject pauseButton;


    private const string WINSCENE = "WinScene";
    private void Awake()
    {



        // no duplicate gameManagers
        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(gameObject);
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


    private void Start()
    {


        pm1.enabled = true;
        pm2.enabled = true;
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
        Debug.Log("play again");
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

        pm1.enabled = true;
        pm2.enabled = true;

        player1.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
        player2.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;

        legHitbox lh1 = player1.GetComponentInChildren<legHitbox>();
        legHitbox lh2 = player2.GetComponentInChildren<legHitbox>();

        lh1.fell = false;
        lh2.fell = false;

        lh1.GetComponent<BoxCollider2D>().enabled = true;
        lh2.GetComponent<BoxCollider2D>().enabled = true;

        player1.GetComponent<BoxCollider2D>().enabled = true;
        player2.GetComponent<BoxCollider2D>().enabled = true;

        building.pauseGarbage = false;
        resetObjects?.Invoke();
        pauseButton.SetActive(true);


    }


    public void UpdateRound()
    {



        if (gameRound == 3)
        {

            if (playerStats.player1Health <= 0)
            {
                float trophyChildCount = 0;
                trophyChildCount = trophyParent2.transform.childCount;
                Instantiate(trophy, new Vector3(trophyParent2.transform.position.x + 20 * trophyChildCount, trophyParent2.transform.position.y, trophyParent2.transform.position.z), Quaternion.identity, trophyParent2.transform);
                playerStats.player1Health = 1f;
                playerStats.player2Health = 1f;
                whoWon += -1;
                PlayerPrefs.SetInt("Winner", whoWon);
                StartCoroutine(ShowWinScreen());
            }
            if (playerStats.player2Health <= 0)
            {
                float trophyChildCount = 0;
                trophyChildCount = trophyParent1.transform.childCount;
                Instantiate(trophy, new Vector3(trophyParent1.transform.position.x + 20 * trophyChildCount, trophyParent1.transform.position.y, trophyParent1.transform.position.z), Quaternion.identity, trophyParent1.transform);
                playerStats.player1Health = 1f;
                playerStats.player2Health = 1f;
                whoWon += 1;
                PlayerPrefs.SetInt("Winner", whoWon);
                StartCoroutine(ShowWinScreen());


            }

            return;
        }

        if (playerStats.player1Health <= 0)
        {

            float trophyChildCount = 0;
            trophyChildCount = trophyParent2.transform.childCount;
            Instantiate(trophy, new Vector3(trophyParent2.transform.position.x + 10 * trophyChildCount, trophyParent2.transform.position.y, trophyParent2.transform.position.z), Quaternion.identity, trophyParent2.transform);

            StartCoroutine(EnableActors());
            pm1.enabled = false;
            pm2.enabled = false;

            gameRound++;
            UpdateCurrentCam();
            whoWon += -1;
            playerStats.player1Health = 100f;
            building.pauseGarbage = true;
            StartCoroutine(MoveCamera(camMoveSpeed, Camera.main.transform.position, currentCamPos));
            pauseButton.SetActive(false);


        }
        if (playerStats.player2Health <= 0)
        {

            float trophyChildCount = 0;
            trophyChildCount = trophyParent1.transform.childCount;
            Instantiate(trophy, new Vector3(trophyParent1.transform.position.x + 10 * trophyChildCount, trophyParent1.transform.position.y, trophyParent1.transform.position.z), Quaternion.identity, trophyParent1.transform);
            //Instantiate(trophy, trophyParent1.transform.position, Quaternion.identity, trophyParent1.transform);

            StartCoroutine(EnableActors());


            pm1.enabled = false;
            pm2.enabled = false;

            gameRound++;
            UpdateCurrentCam();
            whoWon += 1;
            playerStats.player2Health = 100f;
            building.pauseGarbage = true;
            StartCoroutine(MoveCamera(camMoveSpeed, Camera.main.transform.position, currentCamPos));
            pauseButton.SetActive(false);

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


    IEnumerator ShowWinScreen()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(WINSCENE);
    }



}
