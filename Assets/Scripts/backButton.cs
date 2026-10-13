using UnityEngine;
using UnityEngine.UI;

public class backButton : MonoBehaviour
{
    private Button button;

    [SerializeField] private GameObject deactivate;
    [SerializeField] private GameObject activate;


    private void Awake()
    {
        button = gameObject.GetComponent<Button>();
    }

    private void Start()
    {
        button.onClick.AddListener(()=>BackButtonAction(deactivate, activate));
    }

    void BackButtonAction(GameObject deactivate,GameObject activate)
    {
        deactivate.SetActive(false);
        activate.SetActive(true);
    }
}
