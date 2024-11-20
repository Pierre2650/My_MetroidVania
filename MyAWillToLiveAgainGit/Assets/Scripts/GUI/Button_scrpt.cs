using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Button_scrpt : MonoBehaviour
{
    private Button myBTN;
    private GameObject uiManager;
    // Start is called before the first frame update

    private void Awake()
    {
        uiManager = GameObject.Find("GUI Manager");
        myBTN = GetComponent<Button>();
    }
    void Start()
    {
        //myBTN = GetComponent<Button>();
        //Game_Over temp = uiManager.GetComponent<Game_Over>();
        myBTN.onClick.AddListener(uiManager.GetComponent<Game_Over>().restart);


    }
}
