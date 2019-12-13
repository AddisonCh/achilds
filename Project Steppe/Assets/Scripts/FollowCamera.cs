using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{

    [SerializeField]
    private GameObject target;
    public float rotateSpeed;
    Vector3 offset;

    void Start()
    {
        offset = target.transform.position - transform.position;
    }

    void LateUpdate()
    {
        float horizontal = Input.GetAxis("Mouse X") * rotateSpeed;
        target.transform.Rotate(0, horizontal, 0);

        float desiredAngle = target.transform.eulerAngles.y;
        Quaternion rotation = Quaternion.Euler(0, desiredAngle, 0);
        transform.position = target.transform.position - (rotation * offset);

        float vertical = Input.GetAxis("Mouse Y") * rotateSpeed;
        //target.transform.Rotate(vertical, 0, 0);

        float desiredVertAngle = target.transform.eulerAngles.x;
        //Quaternion vertRotation = Quaternion.Euler(desiredVertAngle, 0, 0);
        //transform.position = target.transform.position - (vertRotation * offset);

        gameObject.transform.eulerAngles = new Vector3(gameObject.transform.eulerAngles.x, Camera.main.transform.eulerAngles.y, gameObject.transform.eulerAngles.y);
    }
}
