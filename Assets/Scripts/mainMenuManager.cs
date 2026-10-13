using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class mainMenuManager : MonoBehaviour
{
    [SerializeField]private Button playButton;
    [SerializeField]private Button creditsButton;
    [SerializeField]private Button quitButton;
    [SerializeField]private GameObject startScreen;
    [SerializeField]private GameObject creditScreen;
   
    [SerializeField]private Button rulesButton;
    [SerializeField]private GameObject rulesScreen;







    private const string GameScene = "Game";

    private void Start()
    {
        playButton.onClick.AddListener(PlayGame);
        creditsButton.onClick.AddListener(OpenCredits);
        quitButton.onClick.AddListener(QuitGame);
        
        rulesButton.onClick.AddListener(OpenRules);
    }

    void PlayGame()
    {
        SceneManager.LoadScene(GameScene);
    }

    void OpenCredits()
    {
        startScreen.SetActive(false);
        creditScreen.SetActive(true);

    }

    void OpenRules()
    {
        rulesScreen.SetActive(true);
        startScreen.SetActive(false);
    }

    

    void QuitGame() {
        
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
