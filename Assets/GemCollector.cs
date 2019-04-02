using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemCollector : MonoBehaviour
{
    void OnTriggerEnter(Collider collider){
        if(collider.gameObject.tag == "Player"){
            player.gems += 1;
            player.gemText.text = ""+player.gems;
            Destroy(gameObject);
        }
    }
}
