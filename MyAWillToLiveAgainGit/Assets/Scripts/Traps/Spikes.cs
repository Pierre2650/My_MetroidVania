using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
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

   
    private void OnCollisionEnter2D(Collision2D collision)
    {
        playerController = collision.gameObject.GetComponent<Player_scrpt>();
        playerController.health = 0;

    }
}
