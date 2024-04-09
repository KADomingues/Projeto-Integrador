using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonRiseControl : MonoBehaviour
{
    public CommonEnemyAtributes atributes;

    private CommonEnemiesMechanics CommonMechanics;
    private Animator animations;
    private Collider2D BoxCollider;
    private Rigidbody2D RB2D;
    private bool chasing = false;

    void Start()
    {
        animations = GetComponent<Animator>();
        CommonMechanics = GetComponent<CommonEnemiesMechanics>();
        BoxCollider = GetComponent<BoxCollider2D>();
        RB2D = GetComponent<Rigidbody2D>();

        BoxCollider.enabled = false;
        RB2D.isKinematic = true;
    }

    void Update()
    {
        if (animations.GetCurrentAnimatorClipInfo(0)[0].clip.name == "skeletonWalking" && chasing == false)
        {
            chasing = true;
            CommonMechanics.walking = true;
            BoxCollider.enabled = true;
            RB2D.isKinematic = false;
        }
    }

    void FixedUpdate()
    {
        if (CommonMechanics.walking)
        {
            transform.position =
                Vector2.MoveTowards(
                                     transform.position,
                                     new Vector2(CommonMechanics.targetPlayer.transform.position.x, transform.position.y),
                                     atributes.moveSpeed * Time.deltaTime
                                   );
        }
    }
}