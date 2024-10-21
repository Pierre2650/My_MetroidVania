using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingSpike : MonoBehaviour
{
    private Rigidbody2D myRb;


    private void Start()
    {
        myRb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        myRb.gravityScale = 1;
        
    }
}
