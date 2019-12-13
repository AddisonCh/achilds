using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeCamera : MonoBehaviour
{
    /*
    public float CameraMoveSpeed = 120.0f;
    public GameObject target;
    public PlayerController targetPlayer;
    private Vector3 followPos;
    public float clampAngle;
    public float inputSensitivity;
    public float camDistanceX;
    public float camDistanceY;
    public float camDistanceZ;
    public float mouseX;
    public float mouseY;
    public float finalInputX;
    public float smoothX;
    public float smoothY;
    private float rotY = 0.0f;
    private float rotX = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 rot = transform.localRotation.eulerAngles;
        rotY = rot.y;
        rotX = rot.x;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        targetPlayer = target.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        //gameObject.transform.position = new Vector3(target.transform.position.x + camDistanceX, target.transform.position.y + camDistanceY, target.transform.position.z + camDistanceZ);

        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");

        rotY += mouseX * inputSensitivity * Time.deltaTime;
        rotX += mouseY * inputSensitivity * Time.deltaTime;

        rotX = Mathf.Clamp(rotX, -clampAngle, clampAngle);

        Quaternion localRotation = Quaternion.Euler(rotX, rotY, 0.0f);
        transform.rotation = localRotation;
    }

    void LateUpdate()
    {
        CameraUpdater();     
    }

    void CameraUpdater()
    {
        Transform thisTarget = target.transform;
        float step = CameraMoveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, thisTarget.position, step);
    }
    */

    public float mouseSensitivity = 10; 
    public Transform target;
    public float distFromTarget = 2;
    public Vector2 pitchMinMax = new Vector2(-40,85);

    public float rotationSmoothTime = 1.2f;
    Vector3 rotationSmoothVelocity;
    Vector3 currentRotation;

    public float yaw;
    public float pitch;

    private void Update()
    {
        yaw += Input.GetAxis("Mouse X") * mouseSensitivity;
        pitch -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        pitch = Mathf.Clamp(pitch, pitchMinMax.x, pitchMinMax.y);

        Vector3 targetRotation = new Vector3(pitch, yaw);
        transform.eulerAngles = targetRotation;

        transform.position = target.position - transform.forward * distFromTarget;
    }

}
