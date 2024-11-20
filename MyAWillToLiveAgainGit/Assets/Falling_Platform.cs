using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Falling_Platform : MonoBehaviour
{

    private Rigidbody2D myRb;

    private bool toFall
    // Start is called before the first frame update
    void Start()
    {
        myRb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        //Make a timer of 0.5 then fall
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        toFall = true;
    }
}
