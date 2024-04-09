using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoomControl : MonoBehaviour
{
    public GameObject[] Doors;
    public GhostBoss Boss;
    public GameObject FinishDoor;

    private bool BossEnabled = false;
    private bool BossDefeated = false;
    
    // Update is called once per frame
    void Update()
    {
        if ( Boss == null && BossDefeated == false)
        {
            BossDefeated = true;
            FinishDoor.GetComponent<FinalDoorControl>().DoorOpen = true;

            foreach (GameObject Door in Doors)
            {
                Door.gameObject.SetActive(false);
            }

            FinishDoor.GetComponent<Animator>().SetBool("open", true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && BossEnabled == false)
        {
            Boss.walking = true;
            BossEnabled = true;

            foreach (GameObject Door in Doors)
            {
                Door.gameObject.SetActive(true);
            }
        }
    }
}