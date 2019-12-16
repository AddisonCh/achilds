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

    float currentSpeed()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            return runSpeed;
        }
        else
        {
            return walkSpeed;
        }
    }


    public float jumpForce;

    void FixedUpdate()
    {
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

        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Vector2 inputDir = input.normalized;

        if(inputDir != Vector2.zero)
        transform.eulerAngles = Vector3.up * Mathf.Atan2(inputDir.x, inputDir.y) * Mathf.Rad2Deg;

        bool running = Input.GetKey(KeyCode.LeftShift);
        float speed = ((running) ? runSpeed : walkSpeed) * inputDir.magnitude;

        transform.Translate(transform.forward * speed * Time.deltaTime, Space.World);

        if (CC.isGrounded)
        {
            if (Input.GetKey(KeyCode.Space))
            {
            }
        }

        //NOTE - Must implement final direction, movedirection will normalize and become finalDirection.

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
