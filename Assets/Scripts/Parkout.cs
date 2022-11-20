using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parkout : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            FailParkout();
        }
    }

    // Move the player to spawn
    // And reduce health, with soud
    // @param none
    // @return void
    private void FailParkout()
    {
        AudioManager.instance.PlaySFX(8);
        HealthManager.instance.Hurt();
        Vector3 ini = new Vector3(334.77f, 92.69f, 288.7f);
        PlayerController.instance.movePlayerTo(ini);
    }
}
