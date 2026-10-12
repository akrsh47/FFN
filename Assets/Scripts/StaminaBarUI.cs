using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBarUI : MonoBehaviour
{

    private float maxStamina;

    [SerializeField] private Image staminaBarImage;

    [SerializeField] private GameObject target;

    [SerializeField]private GameObject player1;
    [SerializeField]private GameObject player2;

    private playerStats ps1;
    private playerStats ps2;

    private void Awake()
    {
        ps1 = player1.GetComponent<playerStats>();
        ps2 = player2.GetComponent<playerStats>();
    }

    private void Start()
    {

        maxStamina = 100f;
    }

    private void Update()
    {
        if (target.name == "Player1")
        {
            staminaBarImage.fillAmount = ps1.playerStamina / maxStamina;
        }
        if (target.name == "Player2")
        {
            staminaBarImage.fillAmount = ps2.playerStamina/ maxStamina;

        }
    }
}
