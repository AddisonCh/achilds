using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryBlock : MonoBehaviour
{
    public Item thisItem;
    public Image itemFrame;
    public bool isClicked = false;

    public void setClick()
    {
        PlayerController player = FindObjectOfType<PlayerController>();
        if (!isClicked && !player.clicked)
        {
            isClicked = true;
            player.pickedItem = gameObject;
            player.clicked = true;
            player.inventoryList.Remove(thisItem);
            player.madeList.Remove(gameObject);
            player.SetInventoryBoth();
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, -10);
        }
        else if (isClicked)
        {
            itemPool myPool = Pool();
            if (myPool != null)
            {
                isClicked = false;
                player.clicked = false;
                gameObject.transform.SetParent(myPool.transform);
                gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, -10);
            }
        }
    }

    private itemPool Pool()
    {
        foreach (itemPool pools in FindObjectsOfType<itemPool>())
        {
            if(pools.hovering)
            {
                return pools;
            }
        }
        return null;
    }

    private void Update()
    {
        if(isClicked)
        {
            //it looks like I have two blocks for if a click happens. Fix this future me!
            gameObject.transform.position = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, 1000);
            if(Input.GetMouseButtonDown(1))
            {
                PlayerController thisPlayer = FindObjectOfType<PlayerController>();
                thisPlayer.clicked = false;
                thisPlayer.inventoryList.Add(thisItem);
                thisPlayer.madeList.Add(gameObject);
                thisPlayer.SetInventoryBoth();
            }
        }
    }
}
