using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouvingPlatform : MonoBehaviour
{
    private Rigidbody2D myRb;

    private bool wait = true;
    private float waitTimer = 0f;
    public float waitDur;

    private char dir = 'L';
    public float speed;


    [SerializeField] private Vector2 leftLim, rightLim;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (wait)
        {
            waitTimer += Time.deltaTime;

            if (waitTimer > waitDur) {

                waitTimer = 0f;
                wait = false;

            }

        }



        if (Vector2.Distance(transform.position, leftLim) < 0.3f && dir == 'L')
        {

            if(dir == 'L')
            {

            }

            if (dir == 'R')
            {

            }

            wait = true;
            dir = 'R';
            myRb.velocity = Vector2.zero;

        }

        if (Vector2.Distance(transform.position, rightLim) > 0.3f && dir == 'R')
        {

            wait = true;
            dir = 'L';
            myRb.velocity = Vector2.zero;

        }



    }


    private void FixedUpdate()
    {

        if (!wait) {

            if(dir == 'L')
            {
                //myRb.velocity = Vector2.right * speed;

            }

            if (dir == 'R')
            {
               // myRb.velocity = Vector2.right * speed;

            }
            
        }


        
    }

    private void OnDrawGizmos()
    {
        //Gizmos.color = Color.green;
        //Gizmos.DrawWireCube(leftLim, new Vector2(1f, 1f));
        //Gizmos.DrawWireCube(rightLim, new Vector2(1f, 1f));
    }

}
