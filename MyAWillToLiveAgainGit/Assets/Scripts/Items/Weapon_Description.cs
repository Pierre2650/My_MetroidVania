using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEngine;

public class Weapon_Description : MonoBehaviour
{
    public Scriptable_weapon scrpt_Weapon;
    public int damage;
    public char nameId;
    public Sprite sprite;

    public AnimationClip atkClip;
    public char typeCollider;
    public Vector2 cSize;
    public Vector2 cOffset;
    public float cRadius;


    void Start()
    {

        damage = scrpt_Weapon.damage;
        nameId = scrpt_Weapon.nameId;
        sprite = scrpt_Weapon.sprite;
        atkClip = scrpt_Weapon.attackClip;
        typeCollider = scrpt_Weapon.typeOfCollider;
        cSize = scrpt_Weapon.cSize;
        cOffset = scrpt_Weapon.cOffset;
        cRadius = scrpt_Weapon.cRadius;
        
       
    }

    





}
