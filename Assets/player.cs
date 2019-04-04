using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// QUI16000158
// James Quinney
// Moves the player based on player input
public class player : MonoBehaviour
{
    public static bool onFloor; // This is whether the player is on the floor, it's static for ease of access in other scripts
    public static int gems; // The number of gems the player has
    public static TextMesh gemText; // The gem counter on the top left
    [SerializeField]
    Transform body; // The player's player model transform
	Rigidbody rb; // The rigidbody attached to the player

    // Start is called before the first frame update
    void Start()
    {
        Physics.gravity = new Vector3(0.0f, -55.0f,0.0f); // We set the gravity as the player spawns
		Cursor.visible = false; // We make the cursor invisible
		Cursor.lockState = CursorLockMode.Locked; // We lock the cursor so it wont leave the game
        gemText = GameObject.Find("Gem Counter").GetComponent<TextMesh>(); // We find the gem text and cache it, so we can easily update it later
		 rb = GetComponent<Rigidbody>(); // We store the rigidbody
    }

    // Update is called once per frame
    void Update()
    {
        float mult = 3 - (Mathf.Abs(Input.GetAxis("Speed")) + 1); // If the player is holding the speed key, we store a multiplier for their move speed

		Transform camTrans = Camera.main.transform; // We store the cameras current transform
        Vector3 cameraAngles = camTrans.eulerAngles; // We store the cameras current angles

		// We check to see if the player is currently moving
		if(Mathf.Abs(Input.GetAxis("Vertical")) > 0.1f || Mathf.Abs(Input.GetAxis("Horizontal")) > 0.1f){
			body.rotation = Quaternion.LookRotation(new Vector3(Input.GetAxis("Horizontal"),0.0f,Input.GetAxis("Vertical"))); // We make the player look in the direction they are moving
			body.eulerAngles += new Vector3(0.0f,cameraAngles.y + 180.0f,0.0f); // (cont'd) We then factor in the camera's angles
		}
        float yVel = rb.velocity.y; // We store the player's Y velocity so it isn't overwritten

		rb.velocity = camTrans.forward * Input.GetAxis("Vertical") * 3.0f * mult + camTrans.right * Input.GetAxis("Horizontal") * 3.0f * mult; // We make the player move, relative to the camera's rotation
        rb.velocity = new Vector3(rb.velocity.x,yVel,rb.velocity.z); // We restore the Y velocity

		// note, restoring Y velocity ensures the player doesn't walk along 3 axes

		// We check if the player is trying to jump, and is on the floor
        if(Input.GetButtonDown("Jump") && onFloor){
            rb.AddForce(transform.up * 900.0f); // We throw the player in the air
        }
    }
}
