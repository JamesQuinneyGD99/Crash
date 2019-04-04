using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// QUI16000158
// James Quinney
// Rotates the camera based on player input
public class CameraRotator : MonoBehaviour
{
    float pitch = 0.0f;
    float yaw = 0.0f;

    // Update is called once per frame
    void Update()
    {
		// We increase the pitch/yaw of the amera based on the player's mouse movement
		yaw += Input.GetAxis("Mouse X") * Time.deltaTime;
		pitch += Input.GetAxis("Mouse Y") * Time.deltaTime;

        transform.eulerAngles = new Vector3(pitch * 50.0f, yaw * 50.0f, 0.0f); // We update the camera's angles
    }
}
