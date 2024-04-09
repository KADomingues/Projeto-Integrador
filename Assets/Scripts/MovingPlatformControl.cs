using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformControl : MonoBehaviour
{
    public float MovingDistance;
    public float MovingSpeed = 0;

    private float OriginalYPos;
    private float EndYPos;
    private bool MovingUp = false;
    private bool MovingDown = false;
    

    // Start is called before the first frame update
    void Start()
    {
        OriginalYPos = this.transform.position.y;
        EndYPos = OriginalYPos - MovingDistance;
    }

    // Update is called once per frame
    void Update()
    {
        if ( this.transform.position.y >= OriginalYPos )
        {
            MovingDown = true;
            MovingUp = false;
        }

        if ( this.transform.position.y <= EndYPos )
        {
            MovingDown = false;
            MovingUp = true;
        }

        if ( MovingDown )
        {
            transform.Translate(Vector2.down * MovingSpeed * Time.deltaTime);
        }

        if ( MovingUp )
        {
            transform.Translate(Vector2.up * MovingSpeed * Time.deltaTime);
        }
    }
}
