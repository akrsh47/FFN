using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class pauseMenuManager : MonoBehaviour
{
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private GameObject player1;
    [SerializeField] private GameObject player2;


    private const string GameScene = "Game";
    private const string MainMenuScene = "MainMenu";

    private void Start()
    {
        resumeButton.onClick.AddListener(ResumeGame);
        restartButton.onClick.AddListener(RestartGame);
        exitButton.onClick.AddListener(ExitGame);
    }

    void ResumeGame()
    {
        Time.timeScale = 1f;
        gameObject.SetActive(false);

    }


    void RestartGame()
    {


        Debug.Log("game restarted");
        Time.timeScale = 1f;



        gameObject.SetActive(false);
        SceneManager.LoadScene(GameScene);


    }

    void ExitGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(MainMenuScene);

    }
}
