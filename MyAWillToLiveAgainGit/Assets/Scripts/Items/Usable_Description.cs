using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Usable_Description : MonoBehaviour
{
    // Start is called before the first frame update

    public Scriptable_Usable scrpt_Usable ;
    public char nameId;
    public Sprite sprite;
    public string effect;
    public int uses;

    void Start()
    {
       
        nameId = scrpt_Usable.nameId;
        sprite = scrpt_Usable.sprite;
        effect = scrpt_Usable.Effect;
        uses = scrpt_Usable.Uses;
      

    }

}
