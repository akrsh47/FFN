using System.Collections;
using TMPro;
using UnityEngine;

public class InfoUIManager : MonoBehaviour
{
    [SerializeField]private TextMeshProUGUI roundBoardText;
   
    public static TextMeshProUGUI deathText;

    public static string whoDied;
    public static string deathReason;


    

    private void Update()
    {
        roundBoardText.text = "Round " + GameManager.instance.gameRound;
        
    }




    
}
