using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;

public class Currency : MonoBehaviour
{
    // at start
    private Rigidbody2D myRb; // rigidBody
    private CircleCollider2D myCC;
    private Vector2 startDir;

    [Header("Player tracker")]
    private GameObject thePlayer = null;
    private Vector2 NearDirToPLayer; // Neares direction Vector from the enemy to the player
    public float speed;

    [Header("First Impulse")]
    private bool spawn = true;
    private bool impulseAction = true;
    [SerializeField] private float impulseCount = 0f;
    [SerializeField] private float impulseDuration = 1f;
    private bool push;

    // Start is called before the first frame update
    void Start()
    {
        myRb = GetComponent<Rigidbody2D>();
        myCC = GetComponent<CircleCollider2D>();
       startDir = randSpawnDir();
        
    }

    private void Update()
    {


        if (thePlayer)
        {
            if(Vector2.Distance(transform.position,thePlayer.transform.position) <= 0.5f)
            {
                Destroy(this.gameObject);

            }
        }
    }




    private void FixedUpdate()
    {
        if (spawn)
        {
            myRb.AddForce(startDir * 6, ForceMode2D.Impulse);
            spawn = false;
        }


        if (impulseAction) {
            impulseCount += Time.deltaTime;

            if (impulseCount > impulseDuration) {

                myRb.gravityScale = 0;
                impulseAction = false;
                myRb.velocity = Vector2.zero;
                myCC.enabled = true;
            }
        }


        if (thePlayer)
        {
            getDirNearestPlayer();
            //myRb.velocity = new Vector2(5f * NearDirToPLayer.x, 5f * NearDirToPLayer.y);

            if (myRb.velocity.x < 30f)
            {
                myRb.AddForce(NearDirToPLayer * speed);
            }
            else
            {

                myRb.velocity = new Vector2(20f * NearDirToPLayer.x, 30f * NearDirToPLayer.y);
            }

            breakSpeed();

        }



        
    }

   



    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" && !thePlayer  && !impulseAction)
        {
                thePlayer = collision.gameObject;

                myCC.radius = 1f;

            
        }
        

    }


    private void breakSpeed()
    {
        // check where the player is  , if its in a position opposed 
        float sideX = this.transform.position.x - thePlayer.transform.position.x;
        float sideY = this.transform.position.y - thePlayer.transform.position.y;

        if (myRb.velocity.x > 0 && sideX > 0 )
        {
            myRb.AddForce(new Vector2(-1,0) * speed * 4);
        }

        if (myRb.velocity.x < 0 && sideX < 0)
        {
            myRb.AddForce(new Vector2(1, 0) * speed * 4);
        }


        if (myRb.velocity.y > 0 && sideY > 0)
        {
            myRb.AddForce(new Vector2(0, -1) * speed * 4);
        }

        if (myRb.velocity.y < 0 && sideY < 0)
        {
            myRb.AddForce(new Vector2(0, +1) * speed * 4);
        }



    }


    private Vector2 randSpawnDir()
    {
        //generate a number between 0 and 1 
        // use cos and sin to generate the vector

        float i = UnityEngine.Random.Range(0.0f, Mathf.PI);
        return new Vector2( Mathf.Cos(i), Mathf.Sin(i));

    }




    private void getDirNearestPlayer()
    {
        /*calculate the nearest direction to the player,
         * 1. take current enemy pos
         * 2. add a unit vector from the possible direction to the enemy pos 
         * 3. compare the distance from this new vector to the player to the distance from the current "nearest" unit vector to the player
         */



        Vector2 posToTest, currentNpos;


        for (float i = 0; i < (Mathf.PI * 2); i = i + 0.1f)
        {
            posToTest = new Vector2(transform.position.x + Mathf.Cos(i), transform.position.y + Mathf.Sin(i));
            currentNpos = new Vector2(transform.position.x + NearDirToPLayer.x, transform.position.y + NearDirToPLayer.y);


            if (Vector2.Distance(posToTest, thePlayer.transform.position) < Vector2.Distance(currentNpos, thePlayer.transform.position))
            {
                NearDirToPLayer = new Vector2(Mathf.Cos(i), Mathf.Sin(i));
            }
        }


    }

}
