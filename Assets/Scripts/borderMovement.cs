using UnityEngine;

public class borderMovement : MonoBehaviour
{
    private int[] positions = new int[2] { 1, 2 };
    [SerializeField] private int positionNum;//give value in inspector


    private void Update()
    {
        if (positions[0] == positionNum)
        {
            if (GameManager.instance.gameRound == 1)
            {
                transform.position = new Vector3(GameManager.instance.camLeftEdge, transform.position.y, transform.position.z);
            }
            else if (GameManager.instance.gameRound == 2)
            {
                transform.position = new Vector3(GameManager.instance.secondCamPos.x-GameManager.instance.halfHeight, transform.position.y, transform.position.z);
            }
            else if (GameManager.instance.gameRound == 3)
            {
                transform.position = new Vector3(GameManager.instance.thirdCamPos.x - GameManager.instance.halfHeight, transform.position.y, transform.position.z);
            }
        }
        else if (positions[1] == positionNum)
        {

            if (GameManager.instance.gameRound == 1)
            {
                transform.position = new Vector3(GameManager.instance.camRightEdge, transform.position.y, transform.position.z);
            }
            else if (GameManager.instance.gameRound == 2)
            {
                transform.position = new Vector3(GameManager.instance.secondCamPos.x + GameManager.instance.halfHeight, transform.position.y, transform.position.z);
            }
            else if (GameManager.instance.gameRound == 3)
            {
                transform.position = new Vector3(GameManager.instance.thirdCamPos.x + GameManager.instance.halfHeight, transform.position.y, transform.position.z);
            }

        }
        else
        {
            return;
        }
    }

}
