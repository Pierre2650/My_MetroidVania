using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class Pl_GUI_Manager : MonoBehaviour
{

    [Header("At start")]
    public GameObject UI;
    public GameObject GameOver;



    [Header("Life animations")]

    public Image[] heartsImg;
    public Sprite[] dmgSprites;
    public Sprite[] healSprites;
    [SerializeField] private float DmgHealAnimSpeed;
    private Coroutine currentHearthAnim = null;
    private int currentHearthSprite = 0;

    [Header("Equipement animations")]

    public Image[] equipementImg;
    //handle sprites by searching them
    private float eqElapsedT = 0;
    public float eqDuration = 0;
    //Curve for type of mouvement
    [SerializeField] private AnimationCurve curve;

    private float eqElapsedT2 = 0;
    private float eqSwitchElapsedT = 0;
    public float eqSwitchDuration = 0;
    private Coroutine switchWeapon;

    [Header("Usable animation")]
    public Sprite[] bottles;
    public Sprite[] usingBottles;
    public Image usableImg;
    //Animation Coroutine 0
    private float usableElapsedT;
    public float usableDuration;

    //Animation Coroutine 1
    private float useElapsedT;
    public float useDuration;
    //Animation Coroutine 2
    private float useElapsedT2;
    public float useDuration2;
    //Animation Coroutine 3
    public float useAnimSpeed;
    private int currentBottleSprite = 0;

    [Header("Currency Animation")]
    public Text curAmount;
    public Sprite[] souls;
    public Image currencyImg;

    public float curAnimSpeed;
    private int currentSoulSprite = 0;
    private float curElapsedT;

    private Coroutine activeCurAnim = null;

    [Header("Arrow")]
    public Text ArrowAmount;
    public Image arrowImg;
    private float noArrElapsed = 0;
    public float noArrDuration  ;
    private float noArrSpeedElapsed = 0;
    public float noArrSpeedDuration;

    private Coroutine activeNoArrAnim = null;


    [Header("Dash")]
    public Image dashImg;
    public Sprite[] dashes;
    public float dashElapsed = 0;
    public float dashDuration;
    private float dashAnimSpeed ;
    private int currentDashSpr = 0;
    private Coroutine dashAnimationCoroutine;

    [Header("Cooldowns")]
    public bool switchWeaponOnCD = false;
    public bool potionOnCD = false;


    private void Start()
    {
        dashAnimSpeed = dashDuration / 6;
    }
    // Update is called once per frame
    void Update()
    {
        

        if (Input.GetKeyDown(KeyCode.M))
        {
            //StopAllCoroutines();


        }



    }

    private void emptyEqCheck()
    {
        if (equipementImg[0].sprite == null)
        {
            equipementImg[0].rectTransform.sizeDelta = Vector2.zero;
        }

    }



    public void callDmgCoroutine(int i)
    {
        currentHearthSprite = 0;

        if (currentHearthAnim != null)
        {
            StopCoroutine(currentHearthAnim);

        }

        currentHearthAnim = StartCoroutine(dmgUIAnimation(heartsImg[i]));
    }
    private IEnumerator dmgUIAnimation(Image Heart)
    {
        bool end = false;
    
            yield return new WaitForSeconds(DmgHealAnimSpeed);

            if (currentHearthSprite > 5)
            {

                currentHearthSprite = 5;
                end = true;
                

            }


            Heart.sprite = dmgSprites[currentHearthSprite];
            currentHearthSprite += 1;
       
        if (!end)
        {
            currentHearthAnim = StartCoroutine(dmgUIAnimation(Heart));

        }
        else
        {
            Debug.Log("dmgUiAnimation Ended");
        }

    }


    public void callhealCoroutine(int i)
    {
        // need to put a coldown, cant be called while active
        currentHearthSprite = 0;

        if (currentHearthAnim != null)
        {
            StopCoroutine(currentHearthAnim);
            heartsImg[i-1].sprite = healSprites[4];

        }

        
       currentHearthAnim = StartCoroutine(healUIAnimation(heartsImg[i]));
    }
    private IEnumerator healUIAnimation(Image Heart)
    {
        bool end = false;

        yield return new WaitForSeconds(DmgHealAnimSpeed);

        if (currentHearthSprite > 4)
        {

            currentHearthSprite = 4;
            end = true;


        }


        Heart.sprite = healSprites[currentHearthSprite];
        currentHearthSprite += 1;

        if (!end)
        {
            currentHearthAnim =  StartCoroutine(healUIAnimation(Heart));

        }

    }




    public void equipementSlot1( Sprite spr){

        StartCoroutine(eqSlot1Animation(equipementImg[0], spr));
               
    }


    private IEnumerator eqSlot1Animation(Image currentWeapon, Sprite spr)
    {
        Debug.Log("Coroutine Started");
        float percentageDur = 0;

        currentWeapon.sprite = spr;

        Vector2 start = new Vector2(0, 0);
        Vector2 end = new Vector2(135, 135);


        while (eqElapsedT < eqDuration)
        {
            if(currentWeapon.sprite == null)
            {
                break;
            }


            percentageDur = eqElapsedT / eqDuration;

            currentWeapon.rectTransform.sizeDelta = Vector2.Lerp(start, end, curve.Evaluate(percentageDur));

            eqElapsedT += Time.deltaTime;
            yield return null;

        }
        eqElapsedT = 0;


    }



    public void equipementSlot2( Sprite spr)
    {
    
            StartCoroutine(eqSlot2Animation(equipementImg[1], spr));
           
    }

    private IEnumerator eqSlot2Animation(Image currentWeapon, Sprite spr)
    {
        Debug.Log("Coroutine Started");
        float percentageDur = 0;

        currentWeapon.sprite = spr;

        Vector2 start = new Vector2(0, 0);
        Vector2 end = new Vector2(90, 90);


        while (eqElapsedT2 < eqDuration)
        {
            if (currentWeapon.sprite == null)
            {
                break;
            }

            percentageDur = eqElapsedT2 / eqDuration;

            currentWeapon.rectTransform.sizeDelta = Vector2.Lerp(start, end, curve.Evaluate(percentageDur));

            eqElapsedT2 += Time.deltaTime;
            yield return null;

        }

     
        eqElapsedT2 = 0;

        switchWeapon = null;

    }



    public void callSwitchEqCoroutine()
    {
        
        StartCoroutine(eqSlotChangeAnimation());
        
    }
    private IEnumerator eqSlotChangeAnimation()
    {
        switchWeaponOnCD = true;
        StartCoroutine(switchWeaponCD());

        Image imgSlot1 = equipementImg[0];
        Image imgSlot2 = equipementImg[1];
        Debug.Log(" eqSlotChangeAnimation Coroutine Started");
        float percentageDur = 0;

        //size
        Vector2 startImgS1 = new Vector2(135, 135);  Vector2 endImgS1 = new Vector2(0, 0);
        Vector2 startImgS2 = new Vector2(90, 90);  Vector2 endImgS2 = new Vector2(0, 0);

        //position
        Vector2 startImgP1 = imgSlot1.rectTransform.anchoredPosition; Vector2 endImgP1 = new Vector2(equipementImg[0].rectTransform.anchoredPosition.x + 66, equipementImg[0].rectTransform.anchoredPosition.y);


        Vector2 startImgP2 = imgSlot2.rectTransform.anchoredPosition; Vector2 endImgP2 = new Vector2(equipementImg[1].rectTransform.anchoredPosition.x - 48, equipementImg[1].rectTransform.anchoredPosition.y);


        while (eqSwitchElapsedT < eqSwitchDuration)
        {

            percentageDur = eqSwitchElapsedT / eqSwitchDuration;

            if(imgSlot1.sprite != null) {

                imgSlot1.rectTransform.sizeDelta = Vector2.Lerp(startImgS1, endImgS1, curve.Evaluate(percentageDur));
                imgSlot1.rectTransform.anchoredPosition = Vector2.Lerp(startImgP1, endImgP1, curve.Evaluate(percentageDur));
            }

            if (imgSlot2.sprite != null) {

                imgSlot2.rectTransform.sizeDelta = Vector2.Lerp(startImgS2, endImgS2, curve.Evaluate(percentageDur));
                imgSlot2.rectTransform.anchoredPosition = Vector2.Lerp(startImgP2, endImgP2, curve.Evaluate(percentageDur));
            }
            
            

            eqSwitchElapsedT += Time.deltaTime;
            yield return null;

        }

        eqSwitchElapsedT = 0;


        equipementImg[0].rectTransform.anchoredPosition = startImgP1;
        equipementImg[1].rectTransform.anchoredPosition = startImgP2;

       Sprite temp = imgSlot1.sprite;
       StartCoroutine(eqSlot1Animation(imgSlot1, imgSlot2.sprite));
       StartCoroutine(eqSlot2Animation(imgSlot2, temp));


    }

    private IEnumerator switchWeaponCD()
    {
        yield return new WaitForSeconds(0.5f);

        switchWeaponOnCD = false;

    }



    public void fillUsableSlot(Sprite sprite) {

        StartCoroutine(usableSlotAnimation(usableImg, sprite));
    }

    private IEnumerator usableSlotAnimation(Image currentUsableSpr,Sprite spr) {
      
        float percentageDur = 0;

        currentUsableSpr.sprite = spr;

        Vector2 start = new Vector2(0, 0);
        Vector2 end = new Vector2(114, 114);


        while (usableElapsedT < usableDuration)
        {
            if (currentUsableSpr.sprite == null)
            {
                Debug.Log("no sprite  to put on current usable sprite");
                break;
            }


            percentageDur = usableElapsedT / usableDuration;

            currentUsableSpr.rectTransform.sizeDelta = Vector2.Lerp(start, end, curve.Evaluate(percentageDur));

            usableElapsedT += Time.deltaTime;
            yield return null;

        }


        usableElapsedT = 0;
    }

    public void useItem(int uses)
    {
        StartCoroutine(useItemAnimation(uses));

    }
    private IEnumerator useItemAnimation(int u)
    {
        potionOnCD = true;
        Coroutine animation2 = StartCoroutine(useItemAnimation2());
        Coroutine animation3 = StartCoroutine(useItemAnimation3(usableImg, 0,u));

        float percentageDur = 0;

        Vector2 start = new Vector2(114, 114);
        Vector2 end = new Vector2(165, 165);


        while (useElapsedT < useDuration/2)
        {

            percentageDur = useElapsedT / useDuration;

            usableImg.rectTransform.sizeDelta = Vector2.Lerp(start, end, curve.Evaluate(percentageDur));

            useElapsedT += Time.deltaTime;
            yield return null;

        }

        usableImg.rectTransform.sizeDelta = end;

        useElapsedT = 0;

        while (useElapsedT < useDuration/2)
        {

            percentageDur = useElapsedT / useDuration;

            usableImg.rectTransform.sizeDelta = Vector2.Lerp(end, start, curve.Evaluate(percentageDur));

            useElapsedT += Time.deltaTime;
            yield return null;

        }
        
        useElapsedT = 0;
        usableImg.rectTransform.sizeDelta = start;

        switchWeapon = null;

        StopCoroutine(animation2);
        usableImg.rectTransform.rotation = Quaternion.Euler(0f, 0f, 0f);
        useElapsedT2 = 0;

        potionOnCD = false;

    }

    private IEnumerator useItemAnimation2()
    {
        
        bool turn = false;

        float percentageDur = 0;

       
        Quaternion start = Quaternion.Euler(0, 0, 0);
        Quaternion end = Quaternion.Euler(0, 0, -11);
        Quaternion end2 = Quaternion.Euler(0, 0, 45);


        while (useElapsedT2 < useDuration2/2) {


            percentageDur = useElapsedT2 / useDuration2;

            usableImg.rectTransform.rotation = Quaternion.Lerp(start, end, curve.Evaluate(percentageDur));

            useElapsedT2 += Time.deltaTime;

            yield return null;

        }

        useElapsedT2 = 0;
        turn = true;



        while (true)
        {
            if (turn)
            {
                percentageDur = useElapsedT2 / useDuration2;

                usableImg.rectTransform.rotation = Quaternion.Lerp(end, end2, curve.Evaluate(percentageDur));

                useElapsedT2 += Time.deltaTime;


            }
            else{

                percentageDur = useElapsedT2 / useDuration2;

                usableImg.rectTransform.rotation = Quaternion.Lerp(end2, end, curve.Evaluate(percentageDur));

                useElapsedT2 += Time.deltaTime;

            }


            if(useElapsedT2 > useDuration2/2)
            {
                turn = !turn;
                useElapsedT2 = 0;
            }


            yield return null;
        }



    }

    private IEnumerator useItemAnimation3(Image potion, int counter,int uses)
    {
        

        bool end = false;
        yield return new WaitForSeconds(curAnimSpeed);

        if (counter > 2 )
        {
            end = true;
        }

        
         


        if (currentBottleSprite == 0) {

            if (uses == 0 ) {
                potion.sprite = usingBottles[uses + 1];
            }
            else
            {
                potion.sprite = usingBottles[uses];
            }
            currentBottleSprite++;
        }
        else
        {
            if (uses == 0)
            {
                potion.sprite = bottles[uses + 1];
            }
            else
            {
                potion.sprite = bottles[uses];
            }
            counter++;
            currentBottleSprite = 0;

        }

        


        if (!end)
        {
            StartCoroutine(useItemAnimation3(potion, counter, uses));

        }
        else
        {
            if (uses >= 1) { 
                potion.sprite = bottles[uses];
            }
            else
            {
                usableImg.rectTransform.sizeDelta = Vector2.zero;
            }
        }
    }

    public void Currency(int currentVal)
    {
        if (currentVal > 9)
        {
            curAmount.text = "0"+ currentVal.ToString();

        }
        else if (currentVal > 99)
        {
            curAmount.text = currentVal.ToString();

        }else if (currentVal > 999)
        {
            curAmount.text = "999";
        }
        else
        {
            curAmount.text = "00" + currentVal.ToString();
        }
        
        if (activeCurAnim != null)
        {
            StopCoroutine(activeCurAnim);

        }
        
       activeCurAnim = StartCoroutine(currencyAnimation(currencyImg,0));


    }

    private IEnumerator currencyAnimation(Image curSoul, int counter) {
        bool end = false;

        yield return new WaitForSeconds(curAnimSpeed);

        if (counter > 2)
        {
            end = true;
        }

        if (currentSoulSprite > 2)
        {

            currentSoulSprite = 0;
            counter++;


        }

        curSoul.sprite = souls[currentSoulSprite];
        currentSoulSprite++;


        if (!end)
        {
            StartCoroutine(currencyAnimation(curSoul,counter));

        }
        else
        {
            curSoul.sprite = souls[0];
            activeCurAnim = null;
        }
    }

    public void Arrow(int currentVal)
    {
        ArrowAmount.text = currentVal.ToString();


    }

    public void noArrow()
    {
        if (activeNoArrAnim != null)
        {
            StopCoroutine(activeNoArrAnim);

        }

        activeNoArrAnim = StartCoroutine(noArrowAnimation());

    }

    private IEnumerator noArrowAnimation()
    {
        bool turn = false;

        float percentageDur = 0;


        Quaternion start = Quaternion.Euler(0, 0, 0);
        Quaternion end = Quaternion.Euler(0, 0, -22);
        Quaternion end2 = Quaternion.Euler(0, 0, 22);


        while (noArrSpeedElapsed < noArrSpeedDuration / 2)
        {


            percentageDur = noArrSpeedElapsed / noArrSpeedDuration;

            arrowImg.rectTransform.rotation = Quaternion.Lerp(start, end, curve.Evaluate(percentageDur));

            noArrSpeedElapsed += Time.deltaTime;

            yield return null;

        }

        noArrElapsed = 0;
        turn = true;



        while (noArrElapsed < noArrDuration)
        {
            if (turn)
            {
                percentageDur = noArrSpeedElapsed / noArrSpeedDuration;

                arrowImg.rectTransform.rotation = Quaternion.Lerp(end, end2, curve.Evaluate(percentageDur));

                noArrSpeedElapsed += Time.deltaTime;


            }
            else
            {

                percentageDur = noArrSpeedElapsed / noArrSpeedDuration;

                arrowImg.rectTransform.rotation = Quaternion.Lerp(end2, end, curve.Evaluate(percentageDur));

                noArrSpeedElapsed += Time.deltaTime;

            }


            if (noArrSpeedElapsed > noArrSpeedDuration / 2)
            {
                turn = !turn;
                noArrSpeedElapsed = 0;
            }

            noArrElapsed += Time.deltaTime;

            yield return null;
        }

    }


    public void dashFB()
    {
        dashImg.enabled = true;
        dashAnimationCoroutine = StartCoroutine(dashFeedbackAnimation(dashImg));
        StartCoroutine(dashColdown());
    }
    private IEnumerator dashColdown()
    {

        while (dashElapsed < dashDuration) {

            dashElapsed += Time.deltaTime;
            
            yield return null;
        }

        

        StopCoroutine(dashAnimationCoroutine);
        dashElapsed = 0;
        currentDashSpr = 0;
        dashImg.sprite = dashes[currentDashSpr]; 
        dashImg.enabled = false;

    }
    private IEnumerator dashFeedbackAnimation(Image curDash)
    {

        yield return new WaitForSeconds(dashAnimSpeed);


        if (currentDashSpr > 5)
        {

            currentDashSpr = 0;

        }

        curDash.sprite = dashes[currentDashSpr];
        currentDashSpr++;


        
        dashAnimationCoroutine = StartCoroutine(dashFeedbackAnimation(curDash));

        
       

    }


    public void gameOverScreen()
    {
        //StopAllCoroutines();
        StartCoroutine(gameOverCountdown());

    }

    private IEnumerator gameOverCountdown() {
        float elapsedT = 0;
        float duration = 1.5f;

        while (elapsedT < duration) { 
            elapsedT += Time.deltaTime;
            yield return null;
        }

        //Instantiate
        Destroy(UI);
        Instantiate(GameOver);
    }



}
