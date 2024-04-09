using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverController : MonoBehaviour
{
    public GameObject MovingBridge;

    private bool leverOn = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Weapon") && leverOn == false)
        {
            MovingBridge.GetComponent<Animation>().Play();
            this.GetComponent<Animator>().SetBool("Active", true);
            this.GetComponent<AudioSource>().Play();
            //this.GetComponent<Animator>().Play("LeverAnimation", 0, 1f);
            leverOn = true;
        }
    }
}