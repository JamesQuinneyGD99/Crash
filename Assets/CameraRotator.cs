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
		yaw += Input.GetAxis("Mouse X") * 100.0f * Time.deltaTime;
		pitch += Input.GetAxis("Mouse Y") * 100.0f * Time.deltaTime;

        transform.eulerAngles = new Vector3(pitch, yaw, 0.0f); // We update the camera's angles
    }
}
