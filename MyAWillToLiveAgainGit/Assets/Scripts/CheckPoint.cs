using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CheckPoint : MonoBehaviour
{
    private SpriteRenderer statue;
    private int currentSprite;
    public Sprite[] animationSprites;
    public float animSpeed;

    // Start is called before the first frame update
    void Start()
    {
        statue = GetComponent<SpriteRenderer>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N)) { 
        
           

            StartCoroutine(statueArrowAnim(statue));

        }

    }

    private IEnumerator statueArrowAnim(SpriteRenderer statue)
    {
        bool end = false;

        yield return new WaitForSeconds(animSpeed);

        if (currentSprite > 8)
        {

            currentSprite = 0;
            end = true;


        }



        statue.sprite = animationSprites[currentSprite];



        if (!end)
        {
            currentSprite += 1;
            StartCoroutine(statueArrowAnim(statue));

        }


    }
}
