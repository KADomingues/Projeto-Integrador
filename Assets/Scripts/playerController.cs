using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class playerController : MonoBehaviour
{
    public GameObject AttackCollider;
    public Transform groundCheck;
    public AudioClip ItemPickUpSound;
    public float velocidadeHorizontal = 10f;
    public float forcaPulo { get; set; } = 1f;

    public float HPMaxvalue { get; set; }
    private float HPValue { get; set; }
    private string currentState;

    private bool viradoDireita = true;
    private bool tocaChao = true;
    private bool WasDamaged = false;
    private bool GameOverState = false;

    public Image HPCicle;
    public Text ScoreText;
    public Text GameOverText;

    [HideInInspector] public float score = 0;
    private Animator animations;
    private AudioSource SourceAudio;

    void Start()
    {
        Time.timeScale = 1.0f;

        GameOverText.gameObject.SetActive(false);
        animations = GetComponent<Animator>();
        //animations.SetTrigger("idle");
        SourceAudio = GetComponent<AudioSource>();

        HPValue = HPMaxvalue;

        StartCoroutine(DamageResolver());
        StartCoroutine(PlayerFlicker());
    }

    void Update()
    {
        if (animations.GetCurrentAnimatorStateInfo(0).IsName("attackAnimation"))
        {
            AttackCollider.gameObject.SetActive(true);
        }
        else
        {
            AttackCollider.gameObject.SetActive(false);
        }

        tocaChao = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Tilemap"));

        if (WasDamaged)
        {
            gameObject.layer = 9;
        }
        else
        {
            gameObject.layer = 0;
        }

        ScoreText.text = "" + score;

        float HPPercent = (100 * HPValue) / HPMaxvalue;
        HPCicle.fillAmount = HPPercent / 100f;

        if ( HPValue >= HPMaxvalue)
        {
            HPValue = HPMaxvalue;
        }

        if ( HPValue <= 0)
        {
            HPValue = 0;
            GameOverState = true;
        }

        if (GameOverState)
        {
            Time.timeScale = 0;
            GameOverText.gameObject.SetActive(true);
        }

        /*if (GameOverState && Input.GetKeyDown("r"))
        {
            //Time.timeScale = 1.0f;
            SceneManager.LoadScene("Main");
        }*/

        //Anima();
    }

    void FixedUpdate()
    {
        float moverHorizontal = Input.GetAxis("Horizontal") * velocidadeHorizontal * Time.deltaTime;
        transform.Translate(new Vector2(moverHorizontal, 0.0f));

        if ( GetComponent<Rigidbody2D>().velocity.y == 0 )
        {
            tocaChao = true;
        }
        else
        {
            tocaChao = false;
        }

        if ( Input.GetAxis("Vertical") > 0 && tocaChao == true )
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.up * forcaPulo;
            tocaChao = false;
        }

        if (moverHorizontal > 0 && !viradoDireita)
        {
            Flip();
        }
        else if (moverHorizontal < 0 && viradoDireita)
        {
            Flip();
        }
        
        /*if (moverHorizontal != 0)
        {
            ChangeAnimationState("runningAnimation");
        }
        else if (Input.GetAxis("Vertical") > 0)
        {
            ChangeAnimationState("jumpAnimation");
        }
        else
        {
            ChangeAnimationState("idleAnimation");
        }*/

        
        if (!Input.anyKey)
        {
            ChangeAnimationState("idleAnimation");
        }
        else
        {
            if (Input.GetAxis("Vertical") > 0)
            {
                ChangeAnimationState("jumpAnimation");
            }
            if (Input.GetAxis("Vertical") < 0)
            {
                ChangeAnimationState("crouchAnimation");
            }
            if (Input.GetButtonDown("Fire1"))
            {
                ChangeAnimationState("attackAnimation");
            }
            if (Input.GetAxis("Horizontal") != 0)
            {
                ChangeAnimationState("runningAnimation");
            }
        }

    }

    void Flip()
    {
        viradoDireita = !viradoDireita;
        Vector3 escala = transform.localScale;
        escala.x *= -1;
        transform.localScale = escala;
    }

    void ChangeAnimationState(string newState)
    {
        //stop the same animation from interrupting itself
        if (currentState == newState)
        {
            return;
        }

        //play the animation
        animations.Play(newState);

        //reassign the current state
        currentState = newState;
    }

    /*void Anima()
    {
        if (!Input.anyKey)
        {
            ChangeAnimationState("idleAnimation");
        }
        else
        {
            if (Input.GetAxis("Vertical") > 0)
            {
                ChangeAnimationState("jumpAnimation");
            }
            if (Input.GetAxis("Vertical") < 0)
            {
                ChangeAnimationState("crouchAnimation");
            }
            if (Input.GetButtonDown("Fire1"))
            {
                ChangeAnimationState("attackAnimation");
            }
            if (Input.GetAxis("Horizontal") != 0)
            {
                ChangeAnimationState("runningAnimation");
            }
        }
    }*/

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ( collision.gameObject.CompareTag("Enemy") && WasDamaged == false )
        {
            int tempDirection;
            if (!viradoDireita)
            {
                tempDirection = -1;
            }
            else
            {
                tempDirection = 1;
            }

            GetComponent<Rigidbody2D>().AddForce(new Vector3(
                                                             this.transform.position.x + -100 * tempDirection,
                                                             this.transform.position.y + 300,
                                                             0
                                                             )
                                                 );

            HPValue -= collision.gameObject.GetComponent<CommonEnemiesMechanics>().atributes.playerDamage;
            WasDamaged = true;
        }
        
        /*if ( collision.gameObject.CompareTag("Finish") )
        {
            Time.timeScale = 0;
            GameOverState = true;
            GameOverText.text = "You win!\nPress R to restart";
            GameOverText.gameObject.SetActive(true);
        }*/
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("HPItem"))
        {
            if ( HPValue == HPMaxvalue )
            {
                score += 25;
            }
            else
            {
                HPValue += 20;
            }
            this.SourceAudio.PlayOneShot(ItemPickUpSound, 0.25f);
            Destroy(collision.gameObject);
        }
    }

    private void OnGUI()
    {
        const int buttonWidth = 125;
        const int buttonHeight = 40;

        if (GameOverState)
        {
            if (GUI.Button(
                new Rect( (Screen.width / 2.5f) - (buttonWidth / 2),
                          (Screen.height / 2f) + 50 - (buttonWidth / 2),
                          buttonWidth,
                          buttonHeight),
                    "Jogar Novamente"))
            {
                SceneManager.LoadScene("Main");
            }

            if (GUI.Button(
                new Rect((Screen.width / 1.66f) - (buttonWidth / 2),
                          (Screen.height / 2f) + 50 - (buttonWidth / 2),
                          buttonWidth,
                          buttonHeight),
                    "Sair"))
            {
                Application.Quit();
            }
        }
    }

    IEnumerator DamageResolver()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.01f);
            if (WasDamaged)
            {
                yield return new WaitForSeconds(2);
                WasDamaged = false;
            }
        }
    }

    IEnumerator PlayerFlicker()
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