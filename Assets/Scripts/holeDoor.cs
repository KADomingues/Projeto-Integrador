using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class holeDoor : MonoBehaviour
{
    public GameObject floorDoor;
    public AudioClip PickUpItem;

    private void Start()
    {
        floorDoor = GameObject.FindGameObjectWithTag("KeyFloor");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            floorDoor.GetComponent<Animation>().Play();
            Destroy(this.gameObject);
            AudioSource.PlayClipAtPoint(PickUpItem, this.transform.position, 0.25f);
        }
    }
}
