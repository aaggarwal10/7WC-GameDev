using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // Start is called before the first frame update
    int change_x, change_z; // each axis
    public float speed;
    public float speedH;
    float horizRot = 0;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        change_x = 0;
        change_z = 0;
        if (Input.GetKey(KeyCode.W))
        {
            change_z = 1;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            change_z = -1;
        } else if (Input.GetKey(KeyCode.A))
        {
            change_x = -1;
        } else if (Input.GetKey(KeyCode.D))
        {
            change_x = 1;
        }
        Vector3 forw = transform.forward.normalized;
        Vector3 right = -Vector3.Cross(forw, transform.up.normalized);
        Vector3 change = change_z * forw + change_x * right;
        this.transform.position = this.transform.position + speed * change;

        horizRot += speedH * Input.GetAxis("Mouse X");
        Vector3 rot = transform.eulerAngles;
        transform.eulerAngles = new Vector3(rot.x, horizRot, 0);        
    }
}
