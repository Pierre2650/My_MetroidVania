using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Falling_Platform : MonoBehaviour
{

    private Rigidbody2D myRb;

    private Vector3 startPos;

    private bool toFall;
    public float delay;
    [SerializeField]
    private float timerToFall;

    private bool toRespawn;
    public float respawn;
    private float timerToRespawn;

    // Start is called before the first frame update
    void Start()
    {
        myRb = GetComponent<Rigidbody2D>();
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        //Make a timer of 0.5 then fall

        if (toFall)
        {
            timerToFall += Time.deltaTime;
            if (timerToFall > delay) { 

                myRb.isKinematic = false;

            }
        }

        if (toRespawn)
        {
            timerToRespawn += Time.deltaTime;

            if (timerToRespawn > respawn) {

                toRespawn = false;
                toFall = false;
                myRb.isKinematic = true;
                transform.position = startPos;
                timerToFall = timerToRespawn = 0;
                myRb.velocity = Vector3.zero;
            }
            
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        toFall = true;
        toRespawn = true;
    }
}
