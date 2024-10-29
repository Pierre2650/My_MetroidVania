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


    private float waitTimer2 = 0f;
    private bool oncoldown = false;


    [SerializeField] private Vector2 leftLim, rightLim;

    // Start is called before the first frame update
    void Start()
    {
        myRb = GetComponent<Rigidbody2D>();
        leftLim = new Vector2(transform.position.x - 10, transform.position.y);


        rightLim = new Vector2(transform.position.x + 10, transform.position.y);

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
                oncoldown = true;

            }

        }
        else
        {
            if (!oncoldown)
            {


                if (Vector2.Distance(transform.position, leftLim) < 0.3f || Vector2.Distance(transform.position, rightLim) < 0.3f)
                {

                    if (dir == 'L')
                    {

                        dir = 'R';
                        myRb.velocity = Vector2.zero;
                        wait = true;
                    }
                    else if (dir == 'R')
                    {

                        dir = 'L';
                        myRb.velocity = Vector2.zero;
                        wait = true;

                    }
                }

            }
            else
            {
                waitTimer2 += Time.deltaTime;

                if (waitTimer2 > 1f)
                {

                    waitTimer2 = 0f;
                    oncoldown = false;

                }

            }

        }

        



    }


    private void FixedUpdate()
    {

        if (!wait) {

            if(dir == 'L')
            {
                myRb.velocity = Vector2.left * speed;

            }

            if (dir == 'R')
            {
               myRb.velocity = Vector2.right * speed;

            }
            
        }


        
    }

    private void OnDrawGizmos()
    {
        if (leftLim != Vector2.zero)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(leftLim, new Vector2(1f, 1f));
            Gizmos.DrawWireCube(rightLim, new Vector2(1f, 1f));
        }
    }


    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            GameObject temp = collision.gameObject;
            //temp.tras

            //temp

        }
    }

}
