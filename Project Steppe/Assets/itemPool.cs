using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class itemPool : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public bool hovering = false;
    [SerializeField]
    private GameObject lolWTF;
    [SerializeField]
    private RectTransform whyDoINeedThis;

    InventoryBlock isCursorFull()
    {
        InventoryBlock invent = null;
        foreach (InventoryBlock block in FindObjectsOfType<InventoryBlock>())
        {
            if(block.isClicked)
            {
                invent = block;
            }
        }
        return invent;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        hovering = true;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        hovering = false;
    }

    public void AddItemToPool()
    {
        Debug.Log("Button is clicked");
        FindObjectOfType<PlayerController>().pickedItem.transform.SetParent(gameObject.transform);
        LayoutRebuilder.ForceRebuildLayoutImmediate(whyDoINeedThis);
    }
}
