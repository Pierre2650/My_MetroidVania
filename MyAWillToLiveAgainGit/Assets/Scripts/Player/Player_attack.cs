using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_attack : MonoBehaviour
{
    public Player_scrpt PlMouvScript;
    public Player_Equipement PlEqScript;
    private CircleCollider2D myCC;
    private BoxCollider2D myBX;
    public GameObject ProjectilePrefab;

    private bool shoot = false;

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
            switch (PlEqScript.wSlots[0])
            {
                case 'F':
                    myBX.enabled = true;
                    break;

                case 'S':
                    CircleCollider2D cc = GetComponent<CircleCollider2D>();
                    cc.enabled = true;
                    break;
                case 'B':

                    
                    shoot = true;

                    break;

            }
            //myCC.enabled = true;
            
        }
        else
        {
            myBX.enabled=false;

            CircleCollider2D cc = GetComponent<CircleCollider2D>();
            if (cc != null){
                cc.enabled = false;
            }

            if (shoot)
            {
                PlEqScript.removeArrow();
                GameObject arrow = Instantiate(ProjectilePrefab, this.transform.position + new Vector3(0.78f, -0.07f, 0), ProjectilePrefab.transform.rotation);

                if (PlMouvScript.currentDir == - 1)
                {
                    arrow.GetComponent<Arrow_Projectile>().dir = -1;
                    arrow.transform.rotation = Quaternion.Euler(0f, 180f, -45);

                }
                
                
                shoot = false;

            }

        }


        if (Input.GetKeyDown(KeyCode.T))
        {
           //createSlashHBox();
        }

        if (Input.GetKeyDown(KeyCode.Y))
        {
            //testDestroy();
        }


    }


    public void createSlashHBox(Vector2 offset, float radius)
    {
        // add this to scriptable obj
        //type of colider
        //size of collider
        //location of collider
        //radius


        CircleCollider2D cc = gameObject.AddComponent(typeof(CircleCollider2D)) as CircleCollider2D;
        cc.offset = offset;
        cc.radius = radius;
    }
    

    public void destroyCollider(char toDestroy)
    {
        switch (toDestroy)
        {
            case 'S':
                Destroy(GetComponent<CircleCollider2D>());
                break;
        }
    }

    private void rangeAtk()
    {

    }

}
