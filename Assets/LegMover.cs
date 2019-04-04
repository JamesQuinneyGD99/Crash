using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// QUI16000158
// James Quinney
// Rotates the player's legs based on player input
public class LegMover : MonoBehaviour
{
    Vector3 defaultRot; // This is the rotation when the player spawns
    [SerializeField]
    int speed = 1; // This is how fast the player's legs move
    float totalMove = 0; // This increases as the player moves
    // Start is called before the first frame update
    void Start()
    {
		defaultRot = transform.eulerAngles + new Vector3(0.0f,90.0f,0.0f); // We store the player's starting rotation, with the offset of their starting rotation
    }

    // Update is called once per frame
    void Update()
    {
		// We check if the player is on the floor
        if(player.onFloor){
            totalMove += Mathf.Abs(Input.GetAxis("Horizontal")) * Time.deltaTime * 5.0f; // We increase the movement of the leg by the player's input
            totalMove += Mathf.Abs(Input.GetAxis("Vertical")) * Time.deltaTime * 5.0f; // We do the same along both axis
            transform.localRotation = Quaternion.Euler(defaultRot.x + Mathf.Sin(totalMove) * speed,defaultRot.y,defaultRot.z); // We update the leg's rotation
        }
		// This is if the player isn't on the floor
        else{
            transform.localRotation = Quaternion.Euler(0.0f,speed * 3.0f,defaultRot.z); // We put the player's legs on the sids of their body
        }
    }
}
