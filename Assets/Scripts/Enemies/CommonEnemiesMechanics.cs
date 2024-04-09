using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonEnemiesMechanics : MonoBehaviour
{
    public CommonEnemyAtributes atributes;
    public GameObject targetPlayer;
    public GameObject[] UniqueDropTable;
    public AudioClip SpawnSound;
    public AudioClip DeathSound;

    private SpriteRenderer RenderSprite;
    private BoxCollider2D Box2D;
    private AudioSource SourceAudio;
    private float posicaoAnterior;
    [HideInInspector] public bool viradoDireita;
    [HideInInspector] public bool WasDamaged = false;
    [HideInInspector] public bool walking = false;
    private bool DeathState = false;
    private Animator animations;
    private Rigidbody2D RB2D;
    private int tempHP;

    void Start()
    {
        tempHP = atributes.HP;
        posicaoAnterior = transform.position.x;

        RenderSprite = GetComponent<SpriteRenderer>();
        Box2D = GetComponent<BoxCollider2D>();
        RB2D = GetComponent<Rigidbody2D>();
        SourceAudio = GetComponent<AudioSource>();
        animations = GetComponent<Animator>();

        if ( targetPlayer  == null )
        {
            targetPlayer = GameObject.FindWithTag("Player");
        }

        if (SpawnSound)
        {
            this.SourceAudio.PlayOneShot(SpawnSound, 0.2f);
        }

        StartCoroutine(DamageResolver());
        StartCoroutine(CharFlicker());
    }

    void Update()
    {
        if (tempHP <= 0 && DeathState == false)
        {
            tempHP = 0;
            animations.SetTrigger("death");
            OnZeroHP();
        }

        if ( posicaoAnterior < transform.position.x && !viradoDireita)
        {
            Flip();
        }

        if ( posicaoAnterior > transform.position.x && viradoDireita)
        {
            Flip();
        }

        posicaoAnterior = transform.position.x;
    }

    void Flip()
    {
        viradoDireita = !viradoDireita;
        Vector3 escala = transform.localScale;
        escala.x *= -1;
        transform.localScale = escala;
    }

    public void OnZeroHP()
    {
        DeathState = true;
        //SourceAudio.PlayOneShot(DeathSound, 0.2f);
        if ( DeathSound != null )
        {
            AudioSource.PlayClipAtPoint(DeathSound, this.transform.position, 0.2f);

        }
        //RenderSprite.gameObject.SetActive(false);
        //Box2D.gameObject.SetActive(false);
        walking = false;
        WasDamaged = false;
        StopCoroutine(CharFlicker());
        RB2D.isKinematic = true;
        Box2D.enabled = !Box2D.enabled;
        targetPlayer.GetComponent<playerController>().score += this.atributes.playerScore;
        Destroy(gameObject, 1);

        foreach ( GameObject loot in UniqueDropTable )
        {
            Instantiate(loot, this.transform.position, this.transform.rotation);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ( collision.gameObject.CompareTag("Weapon") && WasDamaged == false )
        {
            tempHP--;
            WasDamaged = true;
        }
    }

    IEnumerator DamageResolver()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.01f);
            if (WasDamaged)
            {
                yield return new WaitForSeconds(2f);
                WasDamaged = false;
            }
        }
    }

    IEnumerator CharFlicker()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.01f);
            if (WasDamaged)
            {
                this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                yield return new WaitForSeconds(0.1f);
                this.gameObject.GetComponent<SpriteRenderer>().enabled = true;
            }
        }
    }
}