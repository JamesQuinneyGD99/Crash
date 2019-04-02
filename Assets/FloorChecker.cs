using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorChecker : MonoBehaviour
{
    [SerializeField]
    Rigidbody rb;
    List<Collider> colliders;
    Dictionary<GameObject, float> reloadFloor;

    void Start(){
        colliders = new List<Collider>();
        reloadFloor = new Dictionary<GameObject, float>();
    }

    void OnTriggerEnter(Collider collider){
        if(collider.gameObject.tag != "Player"){
            player.onFloor = true;
            colliders.Add(collider);
        }

        if(collider.gameObject.tag == "Box"){
            reloadFloor[collider.gameObject] = Time.time;
            collider.gameObject.SetActive(false);
            player.onFloor = false;
            colliders = new List<Collider>();
            if(Input.GetButton("Jump")){
                rb.velocity = new Vector3(rb.velocity.x,32.5f,rb.velocity.z);
            }
            else{
                rb.velocity = new Vector3(rb.velocity.x,30.0f,rb.velocity.z);
            }
        }
    }

    void Update(){
        List<GameObject> toReload = new List<GameObject>();

        foreach(KeyValuePair<GameObject, float> boxes in reloadFloor){
            if(boxes.Value + 10 < Time.time){
                toReload.Add(boxes.Key);
            }
        }

        foreach(GameObject box in toReload){
            box.SetActive(true);
            reloadFloor.Remove(box);
        }
    }

    void OnTriggerExit(Collider collider){
        if(collider.gameObject.tag != "Player"){
            colliders.Remove(collider);
            if(colliders.Count == 0){
                player.onFloor = false;
            }
        }
    }
}
