using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    public CommonEnemyAtributes Atributes;

    private bool walking = false;
    private bool initialRespawn = true;
    private CommonEnemiesMechanics CommonMechanics;
    private Rigidbody2D RB2D;
    private Collider2D BoxCollider;
    
    // Start is called before the first frame update
    void Start()
    {
        CommonMechanics = GetComponent<CommonEnemiesMechanics>();
        BoxCollider = GetComponent<BoxCollider2D>();
        RB2D = GetComponent<Rigidbody2D>();

        BoxCollider.enabled = false;
        CommonMechanics.WasDamaged = true;
    }

    private void Update()
    {
        if ( initialRespawn == true && CommonMechanics.WasDamaged == false)
        {
            initialRespawn = false;
            BoxCollider.enabled = true;
            walking = true;
        }
    }

    void FixedUpdate()
    {
        if (walking)
        {
            transform.position =
                Vector2.MoveTowards(
                    transform.position,
                    new Vector2(CommonMechanics.targetPlayer.transform.position.x, CommonMechanics.targetPlayer.transform.position.y),
                    Atributes.moveSpeed * Time.deltaTime
                    );
        }
    }
}
