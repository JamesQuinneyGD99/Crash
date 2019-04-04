using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// QUI16000158
// James Quinney
// Rotates gems
public class GemRotator : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(transform.position, transform.up * 15.0f,Time.deltaTime*60.0f); // We rotate the gem, based on their current rotation
    }
}
