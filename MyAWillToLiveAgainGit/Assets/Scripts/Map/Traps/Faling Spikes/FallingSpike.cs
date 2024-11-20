using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingSpike : MonoBehaviour
{
    private Rigidbody2D myRb;
    private BoxCollider2D mybx;

    public float gravityModifier = 1;

    private Vector2 initPos;

    private bool toReset;
    private float resetTimer;

    private void Start()
    {
        initPos = transform.position;
        myRb = GetComponent<Rigidbody2D>();
        mybx = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {

        if (toReset)
        {
            resetTimer += Time.deltaTime;

            if (resetTimer > 3f)
            {

                reset();

                resetTimer = 0;


            }
        }

        
    }

    private void reset() {
        transform.position = initPos;
        myRb.velocity = Vector2.zero;
        myRb.gravityScale = 0;
        toReset = false;
        mybx.enabled = true;

    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        myRb.gravityScale = gravityModifier;
        toReset = true;

        mybx.enabled = false;
        
        
    }
}
