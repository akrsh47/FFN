using System.Collections;
using UnityEngine;

public class playerEffect : MonoBehaviour
{
    private SpriteRenderer sp;



    private playerAction pa;



    private float blinkingTimer;

    private void Awake()
    {
        sp = GetComponent<SpriteRenderer>();
        pa = GetComponent<playerAction>();
    }

    private void Start()
    {

        blinkingTimer = 0f;
    }



    private void Update()
    {
        PlayerHitBlink();
    }

    private void PlayerHitBlink()
    {
        if (!pa.onKnockbackCooldown)
        { return; }


        blinkingTimer += Time.deltaTime;

        if (blinkingTimer >= 0.4f)
        {
            sp.enabled = !sp.enabled;
            blinkingTimer = 0f;
        }
    }
}
