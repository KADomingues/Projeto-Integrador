using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostRespawn : MonoBehaviour
{
    public GameObject ObjectToSpawn;
    public float SpawnRate = 2;

    private bool touching = false;
    private float nextSpawn = 0;

    void Update()
    {
        if (Time.time > nextSpawn && touching == true)
        {
            nextSpawn = Time.time + SpawnRate;

            Vector2 randomPos;
            randomPos = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
            randomPos = transform.TransformPoint(randomPos * 0.5f);
            Instantiate(ObjectToSpawn, randomPos, transform.rotation);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && collision.gameObject.tag != "Enemy")
        {
            touching = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && collision.gameObject.tag != "Enemy")
        {
            touching = false;
        }
    }
}