using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float speed;
    public float min;
    public float max;
    public GameObject positioner;

    // Update is called once per frame
    void Update()
    {
        float camRot = Input.GetAxis("Mouse Y") * speed;
        Vector3 origRot = transform.eulerAngles;
        transform.Rotate(-camRot, 0, 0);
        float rot = transform.eulerAngles.x;
        if (rot > 180)
        {
            rot -= 360;
        }

        if (min > rot || max < rot)
        {
            transform.eulerAngles = origRot;
        }
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0);
        transform.position = positioner.transform.position;
    }
}
