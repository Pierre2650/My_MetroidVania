using System;
using System.Diagnostics;
using System.Globalization;
using UnityEngine;
using UnityEngine.SceneManagement;
using Debug = UnityEngine.Debug;

public class Player_scrpt : MonoBehaviour
{
    
    [Header("To Init")]
    private Rigidbody2D myRb;
    private Animator myAni;
    public BoxCollider2D myBxC;
    private LayerMask theGroundMask;
    private AnimationClip[] Clips;
    private float attackDur;
    public Pl_GUI_Manager guiManager;
    private Player_Equipement equipement;

    [Header("Parametrage")]
    public float speed = 60.0f;
    public float airSpeed = 1.0f;
    public float JumpForce = 175.0f;
    public float jumpTime = 0.15f;
    public float fallGravity = 13.0f;
    public float dashForce = 15.0f;
    public int health ;

    [Header("Mouvement Conditions")]
    private float H_Mouvement = 0.0f;
    public bool gCheck = false;
    private bool dash = false;
    public bool jump;

    [Header("Attack Management")]
    public bool actAtk = false;

    [Header("Jump management")]
    private float countJTime = 0;
   
    public float onAirGScale = 5f;
    private int nbjumps = 1;

    [Header("Dash Management")]
    private float countDTime = 0f;
    public float dashTime = 0.4f;


    
    [Header("Actions")]
    public bool isHit = false;
    private bool isAttacking =false;
    private bool isDashing = false;
    private bool isDead = false;
    //Direction to push back
    private int pushBackDir = 0;

    
    [Header("Coldowns")]
    private float timer1 = 0.0f;

    public float currentDir = 0;



    // Start is called before the first frame update
    void Start()
    {
        myRb = GetComponent<Rigidbody2D>();
        myAni = GetComponent<Animator>();
        myBxC = GetComponent<BoxCollider2D>();
        theGroundMask = LayerMask.GetMask("Ground");
        equipement = GetComponent<Player_Equipement>();
        getAttackDur();
        
    }

    // Update is called once per frame
    void Update()
    {

        animationManager();

        currentDir = transform.rotation.y;

            //Reload scene
        if (Input.GetKeyDown(KeyCode.P))
        { SceneManager.LoadScene("SampleScene");}

            //End game
        if (Input.GetKeyDown(KeyCode.Escape))
        {   Application.Quit();}


        //Horizontal Mouvement

        if (Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow))
        {
            H_Mouvement = 1;

        }
        else if (Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
        {
            H_Mouvement = -1;

        }
        else { H_Mouvement = 0; }




        //Attack
        if (Input.GetKeyDown(KeyCode.Z) && !isAttacking && !isHit && !isDashing && !isDead && !guiManager.switchWeaponOnCD)
        {
            if (equipement.wSlots[0] == 'B' && equipement.currentArr == 0) {
                guiManager.noArrow();

            }
            else
            {
                myAni.SetTrigger("Attack");
                isAttacking = true;
                myRb.velocity = new Vector2(0, myRb.velocity.y);
                countdowns("atkCldwn");
            }
        }
        else if (isAttacking) 
        {
            countdowns("atkCldwn");
        }

            //Jump Mouvement
        jump = Input.GetKey(KeyCode.UpArrow);

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            nbjumps--;
            myAni.SetBool("onGround", true);
        }

        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
           

            if (nbjumps != 0) {
                
                countJTime = 0;
            }

        }

        //Dash
        if (Input.GetKeyDown(KeyCode.Space) && !isDashing && !isHit && guiManager.dashElapsed == 0)
        {
            dash = true;
            isDashing = true;
            myBxC.excludeLayers = LayerMask.GetMask("Enemy");
            guiManager.dashFB();

            if (gCheck)
            {
                myAni.SetTrigger("Dash");
            }
            else
            {
                myAni.SetTrigger("airDash");
            }

        }

        //Use item
        if (Input.GetKeyDown(KeyCode.C) && !isHit){

            //guiManager.useItem();

        }





        //print test
        if (Input.GetKeyDown(KeyCode.L))
        {
            //Debug.Log();
        }


        


    }

    private void getAttackDur()
    {
        Clips = myAni.runtimeAnimatorController.animationClips;

        foreach(AnimationClip clip in Clips)
        {
            if(clip.name == "AttackF")
            {   
                attackDur = clip.length;
                break;
            }
        }

    }

    private void animationManager()
    {

        
        if (!isHit && !isAttacking && !isDashing && !isDead)
        {
            myAni.SetBool("onGround", gCheck);

        }
        else
        {
            myAni.SetBool("onGround", true);
        }


        myAni.SetBool("Hurt", isHit);

        myAni.SetFloat("Run", Mathf.Abs(myRb.velocity.x));
    }

    private void xyVelocityTreshold()
    {
        if(myRb.velocity.y > 30)
        {

            myRb.velocity = new Vector2(myRb.velocity.x, 0);
        }

        if (myRb.velocity.x > 30)
        {
            myRb.velocity = new Vector2(0, myRb.velocity.y);
        }
    }

    private void FallSpeed()
    {
        if (!gCheck && myRb.velocity.y < 0 && myRb.gravityScale != fallGravity)
        {
            //Debug.Log("Gravity changed, objt heavier");
            myRb.gravityScale = fallGravity;
        }

        if (myRb.velocity.y < -19)
        {
            //Speed limiter
            myRb.velocity = new Vector2(myRb.velocity.x, -19);
        }
    }

    

    private void FixedUpdate()
    {

        xyVelocityTreshold();
        FallSpeed();

        gCheck = groundChck();
       
        if (gCheck)
        {
            myRb.gravityScale = 1;

            countJTime = 0;
            nbjumps = 1;

        }

        if (!isHit && !isDashing && !isDead) { 

            mouvChoice();
        }
        
        if(isHit)
        {
            Hitted(pushBackDir);
        }

        if (isDashing && !isDead)
        {

            goDash();
            
        }

    }

    private void mouvChoice()
    {

        if (H_Mouvement > 0.1)
        {

            goRight();

        }

        if (H_Mouvement < -0.1)
        {
            goLeft();

        }

        if (H_Mouvement < 0.1 && H_Mouvement > -0.1)
        {

            if (gCheck) {

            //myRb.velocity = new Vector2(0, myRb.velocity.y);
            myRb.velocity = new Vector2(0, 0);

            }

        }


        if (jump)
        {

            countJTime += Time.deltaTime;

            if (countJTime < jumpTime)
            {
                gojump();
            }
        }

    }

    private void countdowns(String Choice)
    {

        switch (Choice)
        {
            case "hitCldwn":

                timer1 += Time.deltaTime;

                if (timer1 > 0.5f)
                {
                    //Physics2D.IgnoreLayerCollision(3, 8, false);

                    myBxC.excludeLayers = LayerMask.GetMask("Nothing");

                    isHit = false;
                    timer1 = 0.0f;
                    myRb.velocity = Vector2.zero;
                    Debug.Log("Hit cooldown ends");

                    
                }

                break;

            case "atkCldwn":

                timer1 += Time.deltaTime;


                if (timer1 > attackDur - 0.04f)
                {
                    isAttacking = false;
                    actAtk = false;
                    timer1 = 0.0f;

                    Debug.Log("Attack cooldown ends");

                }else if(timer1 > 0.1 && timer1 <= attackDur - 0.04f)
                {
                    actAtk = true;
                }

                break;
        }

        

    }


    private void Hitted(int x_sign)
    {
        //Need to revisit to avoid bugs

        Vector2 pushedBack;


        if (timer1 == 0)
        {
            //Debug.Log("inside hitted condition timer 1 = 0");


            myRb.velocity = Vector3.zero;

            countdowns("hitCldwn");
        }
        else
        {
            pushedBack = new Vector2(10 * x_sign, 5);

            if(myRb.velocity.x > -10f && myRb.velocity.x < 10f && myRb.velocity.y < 5 )
            {
                myRb.AddForce(pushedBack, ForceMode2D.Impulse);

            }
            else
            {
                myRb.AddForce(-pushedBack / 2, ForceMode2D.Impulse);
            }



            //Debug.Log("inside hitted condition else");
            countdowns("hitCldwn");
        }

    
    }




    private void goRight()
    {
        transform.rotation = Quaternion.Euler(0f, 0f, 0f);

        if (/*Grouded*/ gCheck)
        {
            if (myRb.velocity.x < 0)
            {
                myRb.velocity = Vector2.zero;
            }


            if (myRb.velocity.x < 10f)
            {
                myRb.AddForce(Vector2.right * speed);
            }
            else
            {
                Vector2 vec_test = new Vector2(10f, myRb.velocity.y);
                myRb.velocity = vec_test;
            }

        }
        else{
            //on air
       
            
            if (myRb.velocity.x < 12f && myRb.velocity.x >= 0)
            {
                myRb.AddForce(Vector2.right * (this.airSpeed));
            }
            else
            {
                myRb.velocity = new Vector2(+12f, myRb.velocity.y);
            }


        }


    }

    private void goLeft()
    {
        transform.rotation = Quaternion.Euler(0f, 180f, 0f);

        if (/*Grouded */ gCheck) {

            if (myRb.velocity.x > 0)
            {
                myRb.velocity = Vector2.zero;
            }


            if (myRb.velocity.x > (-10f))
            {
                myRb.AddForce(Vector2.right * (-speed));
            }
            else
            {
                Vector2 vec_test = new Vector2(-10f, myRb.velocity.y);
                myRb.velocity = vec_test;
            }
        }
        else
        {
            //on air
            if(myRb.velocity.x > -12f && myRb.velocity.x <= 0)
            {
                myRb.AddForce(Vector2.right * (-this.airSpeed));
            }
            else
            {
                myRb.velocity = new Vector2(-12f, myRb.velocity.y);
            }

        }

    }

   

    private void gojump()
    {

        if (/*Grouded*/ gCheck || nbjumps > 0) {

            myRb.gravityScale = onAirGScale;
   

            if (myRb.velocity.y < 15f)
            {
                myRb.AddForce(Vector2.up * JumpForce);
            }
            else
            {
                myRb.velocity = new Vector2(myRb.velocity.x, 15f);
            }

        }

    }


    private void goDash()
    {
        countDTime += Time.deltaTime;

        if (countDTime < dashTime)
        {
            if (dash)
            {
                Vector2 dashDir;
                if (transform.rotation.y == 0)
                {
                    dashDir  = new Vector2(1, 0);
                }
                else
                {
                    dashDir = new Vector2(-1, 0);
                }

                myRb.velocity = new Vector2(0, myRb.velocity.y);
                myRb.AddForce(dashDir * dashForce, ForceMode2D.Impulse);

                dash = false;
            }
        }
        else
        {
            myBxC.excludeLayers = LayerMask.GetMask("Nothing");
            isDashing = false;
            countDTime = 0;
        }

    }


    private void Dies()
    {
        myAni.SetTrigger("Dead");
    
        myBxC.excludeLayers = LayerMask.GetMask("Enemy");

        isDead = true;
        guiManager.gameOverScreen();
    }


    private bool groundChck()
    {

        RaycastHit2D GrounfChckBoxCast = Physics2D.BoxCast(myBxC.bounds.center - new Vector3(0, 0.6f), myBxC.bounds.size - new Vector3(0, 1f), 0f, Vector2.down, 0f, theGroundMask);

        return GrounfChckBoxCast.collider;
    }

   

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(myBxC.bounds.center - new Vector3(0, 0.6f), myBxC.bounds.size - new Vector3(0f, 1f));
    }


    public bool heal(int uses)
    {
        if (health < 3 && !isHit )
        {
            guiManager.useItem(uses);
            guiManager.callhealCoroutine(health);
            health++;
            return true;

        }
        else
        {
            return false;
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (myBxC.IsTouching(collision.collider) && collision.gameObject.layer == 8 && !isHit)
        {
            myRb.velocity = Vector2.zero;

            health--;
            guiManager.callDmgCoroutine(health);
            if(health <= 0)
            {
                health = 0;
                Dies();


            }
            else
            {
                isHit = true;


                myBxC.excludeLayers = LayerMask.GetMask("Enemy");

                pushBackDir = choseDir(collision.contacts[0].point.x);

                Debug.Log(collision.contacts[0].point);
            }
            
            
        }



    }

    private int choseDir(float x_contactPt)
    {
        //Choose direction to be pushed towards
        if (x_contactPt - transform.position.x < 0)
        {
            return  1;
        }
        else if (x_contactPt - transform.position.x > 0)
        {
            return -1;
        }
        else
        {
            return 0;
        }

    }

   










}
