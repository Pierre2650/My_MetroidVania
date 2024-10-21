using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dead_Zone : MonoBehaviour
{
    private Player_scrpt playerController;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        playerController = collision.gameObject.GetComponent<Player_scrpt>();
        playerController.health = 0;
    }
}
