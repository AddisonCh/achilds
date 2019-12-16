using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class POOP : MonoBehaviour
{

    public Transform target;
    public Transform pivot;
    public Vector3 offset;
    public bool useOffsetValues;
    public float rotateSpeedX;
    public float rotateSpeedY;
    public float angleOffset;

    public float maxViewAngle;
    public float minViewAngle;

    public bool invertY;

    public CharacterController thisCC;

    // Start is called before the first frame update
    void Start()
    {
        if(!useOffsetValues)
        {
            offset = target.position - transform.position;
            //Camera.main.transform.Translate +=
        }

        //thisCC = target.GetComponentInParent<CharacterController>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        thisCC.enabled = false;

        float horizontal = Input.GetAxis("Mouse X") * rotateSpeedX;
        target.Rotate(0, horizontal, 0);

        float vertical = Input.GetAxis("Mouse Y") * rotateSpeedX;
        //Mathf.Clamp(vertical, -10,10);
        if(invertY)
        {
            pivot.Rotate(vertical, 0, 0);
        }
        else
        {
            pivot.Rotate(-vertical, 0, 0);
        }

        if(pivot.rotation.eulerAngles.x > maxViewAngle && pivot.rotation.eulerAngles.x < 180f)
        {
            pivot.rotation = Quaternion.Euler(maxViewAngle,0,0);
        }

        if(pivot.rotation.eulerAngles.x > 180 && pivot.rotation.eulerAngles.x < 360f + minViewAngle)
        {
            pivot.rotation = Quaternion.Euler(360f + minViewAngle,0,0);
        }

        float desiredYAngle = target.eulerAngles.y;
        float desiredXAngle = pivot.eulerAngles.x;
        Quaternion rotation = Quaternion.Euler(desiredXAngle, desiredYAngle, 0);
        transform.position = target.position - (rotation * offset);

        if(transform.position.y < target.position.y)
        {
            transform.position = new Vector3(transform.position.x, target.position.y - .5f, transform.position.z);
        }

        transform.LookAt(target);

        transform.Rotate(new Vector3(angleOffset, 0, 0));

        if (Input.GetKeyDown(KeyCode.B))
        {
            target.transform.parent.transform.Translate(new Vector3(0, 10, 0));
        }

        if(transform.position.y < target.position.y)
        {
            //transform.position = new Vector3(transform.position.x, target.position.y, transform.position.z);
        }
        thisCC.enabled = true;
    }
}
