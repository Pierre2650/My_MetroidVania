using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class oneWayPlatform : MonoBehaviour
{
    private BoxCollider2D myBx;

    private GameObject thePlayer;
    // Start is called before the first frame update
    void Start()
    {
        myBx = GetComponent<BoxCollider2D>();

        
    }

    // Update is called once per frame
    void Update()
    {

        // enought distance to set the layer to ground, and when far, set it back do normal

        if (thePlayer != null)
        {
            if (thePlayer.transform.position.y - 0.78f > this.transform.position.y + 0.5f) {
                this.gameObject.layer = 6;
            }

            if (thePlayer.transform.position.y < this.transform.position.y - 0.5f)
            {
                this.gameObject.layer = 0;
            }

            if(Vector2.Distance(thePlayer.transform.position, this.transform.position) > 5)
            {
                thePlayer = null;
            }


        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && thePlayer == null)
        {
            thePlayer = collision.gameObject;

            if (thePlayer.transform.position.y - 0.78f > this.transform.position.y)
            {
                this.gameObject.layer = 6;
            }

        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(new Vector3(this.transform.position.x, this.transform.position.y + 0.5f), new Vector3(0.3f, 0.3f));

        
    }
}
