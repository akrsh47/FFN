using UnityEngine;

public class garbageinteraction : MonoBehaviour
{

    [SerializeField]private Animator animator;
    private static readonly int isHitGround = Animator.StringToHash("isHitGround");

    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.CompareTag("footpath") || collision.gameObject.CompareTag("Player"))
        {
            animator.SetTrigger(isHitGround);
            transform.rotation = Quaternion.identity;

        }
    }
}
