using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "EqWeapon")]
public class Scriptable_weapon : ScriptableObject
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
