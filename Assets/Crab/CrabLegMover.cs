using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// QUI16000158
// James Quinney
// Rotates crab legs
public class CrabLegMover : MonoBehaviour
{
	Vector3 euler;
	float curRot; // This is so each leg stars at a different rotation
	float moveSpeed = 0.4f; // This is how fast the crab moves, ideally below 0.5f

    // Start is called before the first frame update
    void Start()
    {
		curRot = Random.Range(-1,2); // We start the leg at a random rotation, this makes it look like the legs are moving independently
		euler = transform.localRotation.eulerAngles; // We store the starting angle, we rotate around this later on
    }

    // Update is called once per frame
    void Update()
    {
			curRot += Time.deltaTime * 30; // We increase the current rotation

			transform.localRotation = Quaternion.Euler(euler.x,Mathf.Sin(curRot) * 20.0f,euler.z); // We apply the rotation to the joint
    }
}