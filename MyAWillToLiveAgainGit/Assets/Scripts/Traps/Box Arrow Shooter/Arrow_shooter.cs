using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.Mathematics;
using UnityEngine;

public class Arrow_shooter : MonoBehaviour
{

    private SpriteRenderer box;
    private int currentSprite;
    public Sprite[] animationSprites;
    public float animSpeed;


    private GameObject prefabEnemyArrow;
    private float arrowDir;

    public float arrowFrequency;


    // Start is called before the first frame update
    void Start()
    {

        box = GetComponent<SpriteRenderer>();

        arrowDir = transform.rotation.y;

        prefabEnemyArrow = (GameObject)Resources.Load("Prefabs/Enemy_Arrow_projectile", typeof(GameObject));



    }

    // Update is called once per frame
    void Update()
    {

       

        arrowFrequency += Time.deltaTime;

        if(arrowFrequency > 1)
        {
            StartCoroutine(boxArrowAnim(box));
            arrowFrequency = 0;

        }

    }

    private IEnumerator boxArrowAnim(SpriteRenderer Box)
    {
        bool end = false;

        yield return new WaitForSeconds(animSpeed);

        if (currentSprite > 5)
        {

            currentSprite = 0;
            end = true;


        }

        if (currentSprite == 3)
        {
            GameObject arrow = Instantiate(prefabEnemyArrow, this.transform.position, prefabEnemyArrow.transform.rotation) ;
            Physics2D.IgnoreCollision(arrow.GetComponent<PolygonCollider2D>(), GetComponent<BoxCollider2D>());

            if (arrowDir == -1)
            {
                arrow.GetComponent<Arrow_Projectile>().dir = -1;
                arrow.transform.rotation = Quaternion.Euler(0f, 180f, -45);

            }


        }


        Box.sprite = animationSprites[currentSprite];



        

        if (!end)
        {
            currentSprite += 1;
            StartCoroutine(boxArrowAnim(Box));

        }
      

    }
}
