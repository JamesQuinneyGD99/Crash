using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// QUI16000158
// James Quinney
// Makes enemies follow the player if they are close
public class Enemy : MonoBehaviour
{
	Transform plyTransform; // This is the player's transform
	Rigidbody rb; // The rigidbody attached to the enemy

    // Start is called before the first frame update
    void Start()
    {
		plyTransform = GameObject.FindGameObjectWithTag("Player").transform; // We cache the player's transform
		rb = GetComponent<Rigidbody>(); // We cache the rigidbody
    }

    // Update is called once per frame
    void Update()
    {
		// We check if the enemy is close to the player
		if(Vector3.Distance(transform.position, plyTransform.position) < 15.0f){
			transform.LookAt(plyTransform); // We tell the enemy to look at the player
			float saveY = rb.velocity.y; // We store the old Y velocity
			rb.velocity = transform.forward * 5.5f; // We make the object move towards the player
			rb.velocity = new Vector3(rb.velocity.x,saveY,rb.velocity.z); // We restore the old Y velocity
		}
    }
}
