using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpening : MonoBehaviour
{
    public Transform Body;
    public float MovingDistance;

    private bool leverOn = false;
    private float OriginalYPos;

    // Start is called before the first frame update
    void Start()
    {
        OriginalYPos = Body.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if ( leverOn == true &&  Body.transform.position.y <= (OriginalYPos + MovingDistance) )
        {
            Body.transform.Translate(Vector2.up * 2 * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Weapon") && leverOn == false)
        {
            this.GetComponent<Animator>().SetBool("Active", true);
            this.GetComponent<AudioSource>().Play();
            leverOn = true;
        }
    }
}