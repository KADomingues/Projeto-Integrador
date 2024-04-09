using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodySkeletonControl : MonoBehaviour
{
    public CommonEnemyAtributes atributes;

    private CommonEnemiesMechanics CommonMechanics;
    private Animator animations;
    //private bool walking = false;

    void Start()
    {
        animations = GetComponent<Animator>();
        CommonMechanics = GetComponent<CommonEnemiesMechanics>();
    }

    void FixedUpdate()
    {
        if ( CommonMechanics.walking)
        {
            transform.position =
                Vector2.MoveTowards(
                                     transform.position,
                                     new Vector2(CommonMechanics.targetPlayer.transform.position.x, transform.position.y ),
                                     atributes.moveSpeed * Time.deltaTime
                                   );
        }
    }

    private void OnBecameVisible()
    {
        CommonMechanics.walking = true;
        animations.SetBool("walking", true);
    }

    private void OnBecameInvisible()
    {
        CommonMechanics.walking = false;
    }

    /*private void OnDestroy()
    {
        if (this.gameObject.name == "BloodySkeleton 2")
        {
            Instantiate(CommonMechanics.UniqueDropTable[0], this.transform.position, this.transform.rotation);
        }
    }*/
}