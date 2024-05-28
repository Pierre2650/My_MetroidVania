using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_attack : MonoBehaviour
{
    public Player_scrpt PlMouvScript;
    private CircleCollider2D myCC;
    private BoxCollider2D myBX;

    private bool check = false;
    
    // Start is called before the first frame update
    void Start()
    {
       
        //myCC = GetComponent<CircleCollider2D>();
        //myCC.enabled = false;
        myBX = GetComponent<BoxCollider2D>();
        myBX.enabled = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        check = PlMouvScript.actAtk;


        if (check)
        {
            //myCC.enabled = true;
            myBX.enabled = true;
        }
        else
        {
           //myCC.enabled=false;
           myBX.enabled=false;
        }
        
    }


}
