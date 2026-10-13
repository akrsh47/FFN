using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinnerManager : MonoBehaviour
{

    [SerializeField]private TextMeshProUGUI playerName;

    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button playAgainButton;
    private int winner;

    private const string MAINMENUSCENE = "MainMenu";
    private const string GAMESCENE = "Game";

    private void Awake()
    {
        winner = PlayerPrefs.GetInt("Winner", 0);
    }

    private void Start()

    {
        if (winner > 0)
        {
            playerName.text = "Player 1";
        }
        else if (winner < 0)
        {
            playerName.text = "Player 2";
        }
        else
        {
            playerName.text = "Noone";
        }

        mainMenuButton.onClick.AddListener(ShowMainMenu);
        playAgainButton.onClick.AddListener(PlayGame);
    }

    void ShowMainMenu()
    {
        SceneManager.LoadScene(MAINMENUSCENE);
    }

    void PlayGame()
    {
        SceneManager.LoadScene(GAMESCENE);
    }

}
