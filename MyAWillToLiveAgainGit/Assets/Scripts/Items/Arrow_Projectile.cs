using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow_Projectile : MonoBehaviour
{
    private Rigidbody2D myRb;
    private PolygonCollider2D myPC;
    private bool destroy = false;


    [SerializeField] private float speed;
    public int dir = 1;

    private float timer2 = 0f;
    private float timer = 0.0f;


    // Start is called before the first frame update
    void Start()
    {
        myRb = GetComponent<Rigidbody2D>();
        myPC = GetComponent<PolygonCollider2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        wcScenario();

        if (destroy) {
            toDestroy();
        }
        
    }

    private void FixedUpdate()
    {
       // myRb.AddForce(Vector2.right*speed);
        myRb.velocity = new Vector2 (speed * dir, 0);
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Destroy(this.gameObject);
        destroy = true;

    }


    private void toDestroy()
    {
        timer += Time.deltaTime;

        if (timer > 0.05f)
        {

            Destroy(this.gameObject);

            timer = 0.0f;

        }

    }

    private void wcScenario()
    {

        timer2 += Time.deltaTime;

        if (timer2 > 10f)
        {

            Destroy(this.gameObject);

            timer2 = 0.0f;

        }

    }

 
}
