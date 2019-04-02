using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegMover : MonoBehaviour
{
    Vector3 defaultRot;
    [SerializeField]
    int speed = 1;
    float totalMove = 0;
    // Start is called before the first frame update
    void Start()
    {
        defaultRot = transform.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        if(player.onFloor){
            totalMove += Mathf.Abs(Input.GetAxis("Horizontal")) * Time.deltaTime * 5.0f;
            totalMove += Mathf.Abs(Input.GetAxis("Vertical")) * Time.deltaTime * 5.0f;
            transform.localRotation = Quaternion.Euler(defaultRot.x + Mathf.Sin(totalMove) * speed,defaultRot.y,defaultRot.z);
        }
        else{
            transform.localRotation = Quaternion.Euler(0.0f,speed * 3.0f,defaultRot.z);
        }
    }
}
