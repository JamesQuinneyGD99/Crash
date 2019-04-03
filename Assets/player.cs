using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    public static bool onFloor;
    public static int gems;
    public static TextMesh gemText;
    [SerializeField]
    Transform body;

    // Start is called before the first frame update
    void Start()
    {
        Physics.gravity = new Vector3(0.0f, -55.0f,0.0f);
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;
        gemText = GameObject.Find("Gem Counter").GetComponent<TextMesh>();
    }

    // Update is called once per frame
    void Update()
    {
        float mult = 3 - (Mathf.Abs(Input.GetAxis("Speed")) + 1);

		Transform camTrans = Camera.main.transform;
        Vector3 cameraAngles = camTrans.eulerAngles;

		if(Mathf.Abs(Input.GetAxis("Vertical")) > 0.1f || Mathf.Abs(Input.GetAxis("Horizontal")) > 0.1f){
			body.rotation = Quaternion.LookRotation(new Vector3(Input.GetAxis("Horizontal"),0.0f,Input.GetAxis("Vertical")));
			body.eulerAngles += new Vector3(0.0f,cameraAngles.y + 180.0f,0.0f);
		}
        Rigidbody rb = GetComponent<Rigidbody>();
        float yVel = rb.velocity.y;

		rb.velocity = camTrans.forward * Input.GetAxis("Vertical") * 3.0f * mult + camTrans.right * Input.GetAxis("Horizontal") * 3.0f * mult;
        rb.velocity = new Vector3(rb.velocity.x,yVel,rb.velocity.z);

        if(Input.GetButtonDown("Jump") && onFloor){
            rb.AddForce(transform.up * 900.0f);
        }
    }
}
