using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformControl : MonoBehaviour
{
    private PlatformEffector2D Effector;

    //private float WaitTime;
    //public float CurrentWaitTime;

    private void Start()
    {
        Effector = GetComponent<PlatformEffector2D>();
    }

    private void Update()
    {
        /*if ( Input.GetKeyUp(KeyCode.DownArrow) )
        {
            CurrentWaitTime = WaitTime;
        }*/

        if ( Input.GetAxis("Vertical") < 0 )
        {
            Effector.rotationalOffset = 180f;
            /*if (CurrentWaitTime <= 0)
            {
                Effector.rotationalOffset = 180f;
                CurrentWaitTime = WaitTime;
            }
            else
            {
                CurrentWaitTime -= Time.deltaTime;
            }*/
        }

        if ( Input.GetAxis("Vertical") >= 0 )
        {
            Effector.rotationalOffset = 0f;
        }
    }
}