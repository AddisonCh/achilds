using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlternateCameraFollow : MonoBehaviour
{

    public Transform target;
    public Vector3 offset;
    public bool useOffsetValues;
    public float rotateSpeedX;
    public float rotateSpeedY;

    public CharacterController thisCC;

    // Start is called before the first frame update
    void Start()
    {
        if(!useOffsetValues)
        {
            offset = target.position - transform.position;
        }

        //thisCC = target.GetComponentInParent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        thisCC.enabled = false;

        float horizontal = Input.GetAxis("Mouse X") * rotateSpeedX;
        target.Rotate(0, horizontal, 0);

        float vertical = Input.GetAxis("Mouse Y") * rotateSpeedY;
        //Mathf.Clamp(vertical, -10,10);
        target.Rotate(-vertical, 0, 0);

        float desiredYAngle = target.eulerAngles.y;
        float desiredXAngle = target.eulerAngles.x;
        Quaternion rotation = Quaternion.Euler(desiredXAngle, desiredYAngle, 0);
        transform.position = target.position - (rotation * offset);

        transform.LookAt(target);

        if (Input.GetKeyDown(KeyCode.B))
        {
            target.transform.parent.transform.Translate(new Vector3(0, 10, 0));
        }
    }

    private void LateUpdate()
    {
        thisCC.enabled = true;
    }
}
