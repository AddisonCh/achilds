using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float walkSpeed = 2;
    public float runSpeed = 6;
    public float inputDelay = 0.1f;
    public float rotateSpeed = 100;
    public float gravityScale;

    Transform cameraT;

    public float speedSmoothTime = 0.1f;

    private Rigidbody thisRB;
    [SerializeField]
    private GameObject spellBook;

    public GameObject equipWindow;
    public GameObject craftWindow;
    public GameObject inventoryWindow;
    public List<Item> inventoryList;
    public GameObject inventoryBlock;
    public GameObject inventoryScroll;
    [HideInInspector]
    public List<GameObject> madeList;

    public bool clicked;
    public GameObject pickedItem;

    public GameObject fireBall;

    [HideInInspector]
    public CharacterController CC;


    public bool crafting = false;

    void Start()
    {
        CC = GetComponent<CharacterController>();
        thisRB = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        cameraT = Camera.main.transform;
        targetRotation = transform.rotation;
    }

    private void OnTriggerStay(Collider other)
    {
        switch(other.gameObject.tag)
        {
            case "Platform":
                Debug.Log("setparent called");
                transform.parent = other.transform.parent.transform;
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        switch(other.gameObject.tag)
        {
            case "CraftTable":
                craftWindow.SetActive(true);
                break;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Platform":
            transform.parent = null;
                break;

            case "CraftTable":
                craftWindow.SetActive(false);
                break;
        }
    }
    void SetInventory()
    {
        foreach (Item item in inventoryList)
        {
            GameObject myItem = Instantiate(inventoryBlock, inventoryWindow.transform) as GameObject;
            myItem.transform.SetParent(inventoryScroll.transform);
            madeList.Add(myItem);
            InventoryBlock otherBlock = myItem.GetComponent<InventoryBlock>();
            otherBlock.thisItem = item;
            otherBlock.itemFrame.sprite = item.sprite;
        }
    }

    void SetInventoryFalse()
    {
        foreach (GameObject block in madeList)
        {
            Destroy(block);
        }
    }

    public void SetInventoryBoth()
    {
        foreach (GameObject block in madeList)
        {
            Destroy(block);
        }
        foreach (Item item in inventoryList)
        {
            GameObject myItem = Instantiate(inventoryBlock, inventoryWindow.transform) as GameObject;
            myItem.transform.SetParent(inventoryScroll.transform);
            madeList.Add(myItem);
            InventoryBlock otherBlock = myItem.GetComponent<InventoryBlock>();
            if (otherBlock != null)
            {
                otherBlock.thisItem = item;
                otherBlock.itemFrame.sprite = item.sprite;
            }
        }
    }

    Vector3 moveDirection()
    {
        if(Input.GetKey(KeyCode.LeftShift))
        {
            return transform.forward * Input.GetAxis("Vertical") * runSpeed + (transform.right * Input.GetAxis("Horizontal") * runSpeed);
        }
        else
        {
            return transform.forward * Input.GetAxis("Vertical") * walkSpeed + (transform.right * Input.GetAxis("Horizontal") * walkSpeed);
        }
    }

    public float jumpForce;
    private Quaternion targetRotation;
    private Vector3 finalDirection;

    void FixedUpdate()
    {
        CC.Move(moveDirection() * Time.deltaTime);
        if(Input.GetKeyDown(KeyCode.I))
        {
            inventoryWindow.SetActive(!inventoryWindow.activeInHierarchy);
            if(inventoryWindow.activeSelf)
            {
                Cursor.visible = true;
                SetInventory();
            }
            else
            {
                Cursor.visible = false;
                SetInventoryFalse();
            }
        }

        //moveDirection = new Vector3(Input.GetAxis("Horizontal") * currentSpeed(), moveDirection.y, Input.GetAxis("Vertical") * currentSpeed());
        //moveDirection = transform.forward * Input.GetAxis("Vertical") * currentSpeed() + (transform.right * Input.GetAxis("Horizontal") * currentSpeed());
        
        if (CC.isGrounded)
        {
            finalDirection.y = 0;
            if (Input.GetKey(KeyCode.Space))
            {
                finalDirection.y = jumpForce;
            }
        }

        //NOTE - Must implement final direction, movedirection will normalize and become finalDirection.

        finalDirection.y = finalDirection.y + Physics.gravity.y * gravityScale * Time.deltaTime;
        if(Input.GetKeyDown(KeyCode.F))
        {
            equipWindow.SetActive(!equipWindow.activeInHierarchy);
        }

        if (Input.GetKey(KeyCode.W))
        {
            //transform.localEulerAngles = new Vector3(0, Camera.main.transform.eulerAngles.y, 0);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            //transform.localEulerAngles = new Vector3(0, Camera.main.transform.eulerAngles.y - 180, 0);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            //player moves right
        }
        else if (Input.GetKey(KeyCode.A))
        {
            //player moves left
        }

        if(Input.GetKeyDown(KeyCode.E))
        {
            Instantiate(fireBall, spellBook.transform.position, Quaternion.identity);
        }

        moveDirection.y += (Physics.gravity.y * Time.deltaTime) * gravityScale;

    }

    private void LateUpdate()
    {
         
    }

    public void AddItem(Item itemToGive)
    {
        inventoryList.Add(itemToGive);
        SetInventoryBoth();
    }

}
