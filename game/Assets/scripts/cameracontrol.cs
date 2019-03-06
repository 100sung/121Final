using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameracontrol : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public bool usedOffset;
    public float rotateSpeed;
    public Transform pivot;
    public float minA;
    public float maxA;

    // Start is called before the first frame update
    void Start()
    {
        if (!usedOffset)
        {
            offset = target.position - transform.position;
        }
        pivot.transform.position = target.transform.position;
        pivot.transform.parent = target.transform;

        //curser on or off
        //Cursor.lockState = CursorLockMode.Locked;

    }

    // Update is called once per frame
    void LateUpdate()
    {
        //rotation get x pos of mouse and rotate
        float horizontal = Input.GetAxis("Mouse X") * rotateSpeed;
        target.Rotate(0, horizontal, 0);

        //rotate get y pos and rotate pivot
        float vertical = Input.GetAxis("Mouse Y") * rotateSpeed;
        pivot.Rotate(vertical, 0, 0);

        // rotation lock
        if (pivot.rotation.eulerAngles.x > maxA && pivot.rotation.eulerAngles.x < 180f)
        {
            pivot.rotation = Quaternion.Euler(maxA, 0f, 0f);
        }
        if (pivot.rotation.eulerAngles.x > 180f && pivot.rotation.eulerAngles.x < 360f + minA)
        {
            pivot.rotation = Quaternion.Euler(360f + minA, 0f, 0f);
        }
        float desiredY = target.eulerAngles.y;
        float desiredX = pivot.eulerAngles.x;

        Quaternion rotation = Quaternion.Euler(desiredX, desiredY, 0);
        transform.position = target.position - (rotation * offset);

        //transform.position = target.position - offset;
        transform.LookAt(target);
    }
}
