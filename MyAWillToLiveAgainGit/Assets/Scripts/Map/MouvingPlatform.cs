using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouvingPlatform : MonoBehaviour
{
    private Rigidbody2D myRb;

    private bool wait = true;
    private float waitTimer = 0f;
    public float waitDur;

    private char dir = 'W';
    public char nextdir = 'L';
    public float speed;


    private float cdTimer = 0f;
    private bool oncoldown = false;


    [SerializeField] private Vector2 leftLim, rightLim;
    public int limDistance = 0;

    // Start is called before the first frame update
    void Start()
    {
        myRb = GetComponent<Rigidbody2D>();

        leftLim = new Vector2(transform.position.x - limDistance, transform.position.y);
        rightLim = new Vector2(transform.position.x + limDistance, transform.position.y);

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
                dir = nextdir;

            }

        }
        else
        {
            if (!oncoldown)
            {


                reachedLimit();

            }
            else
            {
                cdTimer += Time.deltaTime;

                if (cdTimer > 0.5f)
                {

                    cdTimer = 0f;
                    oncoldown = false;

                }

            }

        }

        



    }


    private void reachedLimit()
    {
        
        if (Vector2.Distance(transform.position, leftLim) < 0.3f || Vector2.Distance(transform.position, rightLim) < 0.3f)
        {


            if (dir == 'L')
            {

                nextdir = 'R';
                myRb.velocity = Vector2.zero;
                wait = true;
            }
            else if (dir == 'R')
            {

                nextdir = 'L';
                myRb.velocity = Vector2.zero;
                wait = true;

            }

            dir = 'W';
        }

    }


    private void FixedUpdate()
    {

        if (!wait) {

            move();
            
        }


        
    }


    private void move()
    {
        if (dir == 'L')
        {
            myRb.velocity = Vector2.left * speed;

        }

        if (dir == 'R')
        {
            myRb.velocity = Vector2.right * speed;

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

            if (dir == 'L')
            {
                temp.transform.position += Vector3.left * speed * Time.deltaTime;

            }

            if(dir == 'R')
            {
                temp.transform.position += Vector3.right * speed * Time.deltaTime;

            }

        }
    }

}
