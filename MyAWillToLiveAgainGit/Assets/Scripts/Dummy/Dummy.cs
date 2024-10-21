using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dummy : MonoBehaviour
{
    private BoxCollider2D myBx;
    private Rigidbody2D myRb;

    private bool destroy = false;
    private SpriteRenderer Barrel;
    private int currentBarrel;
    public Sprite[] animationSprites;
    public float animSpeed;

    private float destroyTimer = 0f;



    // Start is called before the first frame update
    void Start()
    {
        myBx = GetComponent<BoxCollider2D>();
        Barrel = GetComponent<SpriteRenderer>();
        myRb = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {

        if (destroy)
        {
            toDestroy();
        }

    }

    private void toDestroy()
    {
        destroyTimer += Time.deltaTime;
        transform.rotation = Quaternion.identity;
        myRb.velocity = Vector3.zero;

        if (destroyTimer > 5f)
        {
            destroyTimer = 0.0f;

            Destroy(this.gameObject);

             

        }

    }

    private IEnumerator dummyAnim(SpriteRenderer Barrel)
    {
        bool end = false;

        yield return new WaitForSeconds(animSpeed);

        if (currentBarrel > 5)
        {

            currentBarrel = 5;
            end = true;


        }


        Barrel.sprite = animationSprites[currentBarrel];
        currentBarrel += 1;

        if (!end)
        {
            currentBarrel += 1;
            StartCoroutine(dummyAnim(Barrel));

        }

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        

        if (collision.collider.gameObject.CompareTag("chAtk"))
        {
            destroy = true;
            myBx.isTrigger = true;
            StartCoroutine(dummyAnim(Barrel));
            this.gameObject.layer = 0;
            myRb.gravityScale = 0;


        }
        
        
    }
}
