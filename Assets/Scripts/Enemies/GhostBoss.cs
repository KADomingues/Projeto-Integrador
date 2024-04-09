using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostBoss : MonoBehaviour
{
    public CommonEnemyAtributes atributes;

    [HideInInspector] public bool walking = false;

    private CommonEnemiesMechanics CommonMechanics;
    
    // Start is called before the first frame update
    void Start()
    {
        CommonMechanics = GetComponent<CommonEnemiesMechanics>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void FixedUpdate()
    {
        if (walking)
        {
            transform.position =
                Vector2.MoveTowards(
                    transform.position,
                    new Vector2(CommonMechanics.targetPlayer.transform.position.x, CommonMechanics.targetPlayer.transform.position.y),
                    atributes.moveSpeed * Time.deltaTime
                    );
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        /*if (collision.gameObject.CompareTag("Weapon") && !CommonMechanics.WasDamaged)
        {
            int tempDirection;
            if (!CommonMechanics.viradoDireita)
            {
                tempDirection = -1;
            }
            else
            {
                tempDirection = 1;
            }

            GetComponent<Rigidbody2D>().AddForce(new Vector3(
                                                             this.transform.position.x + -300 * tempDirection,
                                                             this.transform.position.y + 100,
                                                             0
                                                             )
                                                 );
        }*/
    }
}