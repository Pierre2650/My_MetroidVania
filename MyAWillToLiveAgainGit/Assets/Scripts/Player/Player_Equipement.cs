using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.PackageManager.UI;
using UnityEngine;

public class Player_Equipement : MonoBehaviour
{
    [Header("To Init")]
    private Rigidbody2D myRb;
    private Animator myAni;
    //Animator skins
    public AnimatorOverrideController Skins;

    [Header("Weapon Inventory")]
    private int inventorySize = 0;
    public char[] wSlots = new char[2];


    [Header("Usable Inventory")]
    private int usableSize = 0;
    public char uSlot = 'N';
    public int uses = 0;


    [Header("Currency")]

    [SerializeField] private int currentCur = 0;

    [Header("Arrows")]

    public int currentArr = 0;


    [Header("Foreign Scrips")]
    public Pl_GUI_Manager guiManager;
    public Player_attack playerAtk;
    public Player_scrpt playerCtrl;



    private bool action;

    struct weapon
    {
        public int damage;
        public char nameId;
        public Sprite sprite;
        public AnimationClip attackClip;


        //hitbox
        //C -> circle
        //B - > box
        //R - > range

        public char typeOfCollider;
        public Vector2 cSize;
        public float cRadius;
        public Vector2 cOffset;

    }

    struct usable
    {
        public char nameId;
        public Sprite sprite;
        //Effec  is a string code  that would help us choice the effect the usable has 
        public string effect;
        public int uses;
    }

    private List<weapon> currentWeapons = new List<weapon>();
    private List<usable> currentUsables = new List<usable>();


    // Start is called before the first frame update
    void Start()
    {
        myRb = GetComponent<Rigidbody2D>();
        myAni = GetComponent<Animator>();
        wSlots[0] = 'F';
        wSlots[1] = 'F';

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.V))
        {
            action = false;
        }


        if (Input.GetKeyDown(KeyCode.V))
        {
            action = true;
        }

        if (Input.GetKeyDown(KeyCode.S) && !guiManager.potionOnCD)
        {
            switch (uSlot)
            {
                case 'P':
                    usePotion();

                    break;
                case 'N':
                    Debug.Log("no Current Usable");
                    break;
            }
        
            
        }

        if (Input.GetKeyDown(KeyCode.A) && !guiManager.switchWeaponOnCD && inventorySize > 0)
        {
            //change equipement
            playerAtk.destroyCollider(wSlots[0]);
            char temp = wSlots[0];
            wSlots[0] = wSlots[1];
            wSlots[1] = temp;
            selectWeapon();
            guiManager.callSwitchEqCoroutine();

        }



    }

    private void usePotion()
    {
        if (uses > 0)
        {
            Debug.Log("Uses = ");
            Debug.Log(uses);

            if (playerCtrl.heal(uses - 1))
            {
                uses--;
            }
                
                   
                
        }

            if(uses == 0)
            {
                int i = 0, debug = 0;
                while (currentUsables[i].nameId != uSlot && debug < 100)
                {
                    i++;
                    debug++;
                }

                if (debug < 100)
                {
                    currentUsables.RemoveAt(i);
                    uSlot = 'N';
                    usableSize = 0;

                }
                else
                {
                    Debug.Log("error in code");
                }



            }

        

    }


    public AnimationClip findAnimation(string name)
    {

        foreach (weapon w in currentWeapons)
        {
            if (w.attackClip.name == name)
            {
                
                return w.attackClip;
            }
        }

        Debug.Log("no clip found");
        return null;
    }



    private void selectWeapon()
    {


        switch (wSlots[0])
        {
            case 'F':

                if (myAni.runtimeAnimatorController.name == "Player_anim_Sw")
                {
                    Skins["AttackF"] = findAnimation("AttackF");

                }
                else
                {

                    AnimatorOverrideController temp = new AnimatorOverrideController(myAni.runtimeAnimatorController);
                    Debug.Log(temp);
                    temp["AttackF"] = findAnimation("AttackF");
                    myAni.runtimeAnimatorController = temp;

                }
                //Skins["AttackF"] = findAnimation("AttackF");




                break;
            case 'S':

                weapon? findW = findWeapon();

                if (findW.HasValue)
                {
                    Skins["AttackF"] = findAnimation("Attack_Swd");
                    weapon currentW = findW.Value;
                    playerAtk.createSlashHBox(currentW.cOffset, currentW.cRadius);
                }
                else
                {
                    Debug.Log("error in code");
                }

                break;
            case 'B':

                if (myAni.runtimeAnimatorController.name == "Player_anim_Sw")
                {
                    Skins["AttackF"] = findAnimation("BowAtk_adv");


                }
                else
                {
                    AnimatorOverrideController temp = new AnimatorOverrideController(myAni.runtimeAnimatorController);
                    Debug.Log(temp);
                    temp["AttackF"] = findAnimation("BowAtk_adv");
                    myAni.runtimeAnimatorController = temp;
                }
                

                break;

        }


    }

    private weapon? findWeapon()
    {

        foreach (weapon w in currentWeapons)
        {
            if (w.nameId == wSlots[0])
            {
                return w;
            }
        }

        Debug.Log("no weapon in inventory found");

        return null;
       
    }

    private usable? findUsable()
    {
        /*
        int i = 0, debug = 0;
        while (currentUsables[i].nameId != uSlot && debug < 100)
        {
            i++;
            debug++;
        }

        return currentUsables[i];
        */

        
        foreach(usable u in currentUsables)
        {
            if (u.nameId == uSlot)
            {
                return u;
            }
        }

        Debug.Log("no weapon in inventory found");
        
        return null;

    }



    private void changeSkins(char path)
    {
        switch (path)
        {
            case 'S':
                myAni.runtimeAnimatorController = Skins;
                break;
            case 'B':
                // chnage runtime animation clip
                if (myAni.runtimeAnimatorController.name == "Player_anim_Sw")
                {
                    Skins["AttackF"] = findAnimation("BowAtk_adv");


                }
                else
                {
                    AnimatorOverrideController temp = new AnimatorOverrideController(myAni.runtimeAnimatorController);
                    Debug.Log(temp);
                    temp["AttackF"] = findAnimation("BowAtk_adv");
                    myAni.runtimeAnimatorController = temp;
                }
                break;

        }
    }


    private void addWeapon(char weaponId, Sprite weaponSprite)
    {

        for (int i = 0; i < 2; i++)
        {
            if (wSlots[i] == 'F'){

                wSlots[i] = weaponId;
                inventorySize++;
                //problema con change skin
                changeSkins(weaponId);


                if (i == 0) {
                    guiManager.equipementSlot1(weaponSprite);
                }

                if (i == 1) {

                    if (wSlots[0] != 'S')
                    {
                        changeSkins(weaponId);
                    }
                    guiManager.equipementSlot2(weaponSprite);
                }


                break;

            }

        }


        /*
        switch (inventorySize)
        {
            case 0:
                wSlots[0] = weaponId;
                inventorySize++;
                changeSkins(weaponId);
                
                guiManager.equipementSlot1( weaponSprite);

                return true;
            case 1:
                wSlots[1] = weaponId;
                inventorySize++;

                if (wSlots[0] != 'S')
                {
                    changeSkins(weaponId);
                }

                guiManager.equipementSlot2(weaponSprite);
                return true;

        }*/
    }

    private void addUsable(char usableID, Sprite usableSprite)
    {
        if (usableSize < 1) {
            uSlot = usableID;
            
            guiManager.fillUsableSlot(usableSprite);
            usableSize++;

        }
    }

    private void addCurrency()
    {
        if (currentCur < 999) { 
            currentCur++;
            guiManager.Currency(currentCur);
        }
    }


    private void addArrow()
    {
        
         currentArr++;
         guiManager.Arrow(currentArr);
        
    }

    public void removeArrow()
    {
        currentArr--;
        guiManager.Arrow(currentArr);
    }

    private void RemoveCur(int amount) {
        currentCur = currentCur - amount;

        if (currentCur < 0) { 
            currentCur = 0;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(action)
        {
            switch (collision.gameObject.tag)
            {
                case "Equipement":

                    //Take weapon
                    if (inventorySize < 2)
                    {
                        collision.GetComponent<Take_item>().take();
                        Weapon_Description WD = collision.GetComponent<Weapon_Description>();

                        createWeapon(WD);

                        selectWeapon();
                    }

                    action = false;

                    break;

                case "Usable":

                    if (usableSize < 1)
                    {
                        
                        collision.GetComponent<Take_item>().take();
                        Usable_Description UD = collision.GetComponent<Usable_Description>();

                        createUsable(UD);

                    }


                    action = false;
                    break ;
            }
            
           
            
            
        }


        if (collision.gameObject.tag == "Currency" && Vector2.Distance(transform.position, collision.transform.position) < 0.5f)
        {
            addCurrency();

        }



    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Arrow" && currentArr < 5)
        {
            collision.GetComponent<Take_item>().take();
            addArrow();
        }


    }


    private void createWeapon(Weapon_Description WD)
    {
        weapon tempW = new weapon();

        tempW.nameId = WD.nameId;
        tempW.damage = WD.damage;
        tempW.sprite = WD.sprite;
        tempW.attackClip = WD.atkClip;
        tempW.typeOfCollider = WD.typeCollider;
        tempW.cSize = WD.cSize;
        tempW.cOffset = WD.cOffset;
        tempW.cRadius = WD.cRadius;

        currentWeapons.Add(tempW);

        addWeapon(tempW.nameId, tempW.sprite);

    }

    private void createUsable(Usable_Description UD)
    {
       
        usable tempU = new usable();

        tempU.nameId = UD.nameId;
        tempU.sprite = UD.sprite;
        tempU.effect = UD.effect;
        tempU.uses = UD.uses;
        uses = UD.uses;

        addUsable(tempU.nameId, tempU.sprite);

        currentUsables.Add(tempU);

    }
}
