using System.Collections;
using UnityEngine;

public class playerEffect : MonoBehaviour
{
    private SpriteRenderer sp;



    private playerAction pa;



    private float blinkingTimer;

    [SerializeField]private ParticleSystem blood1;
    [SerializeField]private ParticleSystem blood2;

    private void Awake()
    {
        sp = GetComponent<SpriteRenderer>();
        pa = GetComponent<playerAction>();
    }

    private void Start()
    {
        if (gameObject.name == "Player1")
        {
            blood1.transform.position = gameObject.transform.position;

        }
        else if (gameObject.name == "Player2")
        {
            blood2.transform.position = gameObject.transform.position;

        }
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

    void TakeDamageEffect(string name)
    {
        if (gameObject.name == "Player1")
        {
            blood1.transform.position = gameObject.transform.position;

        }
        else if (gameObject.name == "Player2")
        {
            blood2.transform.position = gameObject.transform.position;

        }

        if (name == "Player1")
        {
            blood1.Play();
        }
        else if (name == "Player2")
        {
            blood2.Play();
        }
    }

    private void OnEnable()
    {
        playerStats.DamageEffect += TakeDamageEffect;
    }
    private void OnDisable()
    {
        playerStats.DamageEffect -= TakeDamageEffect;
    }

}
