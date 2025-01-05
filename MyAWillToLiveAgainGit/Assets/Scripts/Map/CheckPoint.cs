using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using System.IO;

public class CheckPoint : MonoBehaviour
{
    private SpriteRenderer statue;
    private int currentSprite;
    public Sprite[] animationSprites;
    public float animSpeed;
    private bool animating = false;

    private bool onObj = false;

    public OnGameStart gameStart;

    // Start is called before the first frame update
    void Start()
    {
        statue = GetComponent<SpriteRenderer>();
        gameStart = GameObject.Find("SceneLoadManager").GetComponent<OnGameStart>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V) && onObj)
        {
            Debug.Log("New Spawn Has been saved");

            if (!animating) { 
                StartCoroutine(statueArrowAnim(statue));
            }

            //save Spawn point
            savePosition();
            
        }

    }

    private void savePosition()
    {
        Player_SpawnPos newSpawn = new Player_SpawnPos();
        newSpawn.PosX = this.transform.position.x;
        newSpawn.PosY = this.transform.position.y;

        //Serializer
        string json = JsonUtility.ToJson(newSpawn);
        File.WriteAllText(gameStart.playerSpawnPosFile, json);
    }

    private IEnumerator statueArrowAnim(SpriteRenderer statue)
    {
        animating = true;
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

        animating = false ;

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        onObj = true;

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        onObj = false;
    }
}
