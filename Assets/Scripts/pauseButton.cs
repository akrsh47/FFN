using UnityEngine;
using UnityEngine.UI;

public class pauseButton : MonoBehaviour
{
    private Button buttonPause;

    [SerializeField]private GameObject pauseMenu;
    private void Awake()
    {
        buttonPause = gameObject.GetComponent<Button>();
    }


    private void Start()
    {

        buttonPause.onClick.AddListener(PauseGame);


    }

    void PauseGame()
    {

        Debug.Log("Game paused");
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);


    }


}
