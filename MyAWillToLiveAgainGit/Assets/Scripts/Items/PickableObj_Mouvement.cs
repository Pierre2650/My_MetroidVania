using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swr_Mouvemente : MonoBehaviour
{
    //To know if to switch mouvement
    private bool switchPos = false;
    private Vector2 startPos, endPos;

    [SerializeField] private float duration;
    public float elapsedT;

    //Curve for type of mouvement
    [SerializeField] private AnimationCurve curve;


    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        endPos = new Vector2(startPos.x, startPos.y + 0.3f);
        StartCoroutine(floatingMouv());
    }


    // Update is called once per frame
    private void Update()
    {



        if (switchPos)
        {
            var temp = startPos; 
            startPos = endPos;
            endPos = temp;
            StartCoroutine(floatingMouv());
            switchPos = false;
        }

    }


    IEnumerator floatingMouv()
    {
       
        float percentageDur = 0;


        while(elapsedT < duration) 
        {
            elapsedT += Time.deltaTime;

            percentageDur = elapsedT / duration;

            transform.position = Vector2.Lerp(startPos, endPos, curve.Evaluate(percentageDur));

            elapsedT += Time.deltaTime;
            yield return null;

        }

        switchPos = true;
        elapsedT = 0;

    }


 
}
