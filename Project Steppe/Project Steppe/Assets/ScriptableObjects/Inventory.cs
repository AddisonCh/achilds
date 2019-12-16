using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Inventory : MonoBehaviour, ItemContainer
{

    [FormerlySerializedAs("items")]
    [SerializeField] List<Item> startingItems;
    [SerializeField] Transform itemParent;
    [SerializeField] ItemSlot[] itemSlots;

    public event Action<ItemSlot> OnPointerEnterEvent;
    public event Action<ItemSlot> OnPointerExitEvent;
    public event Action<ItemSlot> OnRightClickEvent;
    public event Action<ItemSlot> OnBeginDragEvent;
    public event Action<ItemSlot> OnEndDragEvent;
    public event Action<ItemSlot> OnDragEvent;
    public event Action<ItemSlot> OnDropEvent;

    void Start()
    {
        for (int i = 0; i < itemSlots.Length; i++)
        {
            itemSlots[i].OnPointerEnterEvent += OnPointerEnterEvent;
            itemSlots[i].OnPointerExitEvent += OnPointerExitEvent;
            itemSlots[i].OnRightClickEvent += OnRightClickEvent;
            itemSlots[i].OnBeginDragEvent += OnBeginDragEvent;
            itemSlots[i].OnEndDragEvent += OnEndDragEvent;
            itemSlots[i].OnDragEvent += OnDragEvent;
            itemSlots[i].OnDropEvent += OnDropEvent;
        }
        SetStartingItems();
    }

    private void OnValidate()
    {
        if(itemParent != null)
        {
            itemSlots = itemParent.GetComponentsInChildren<ItemSlot>();
        }
        SetStartingItems();
        RefreshUI();
    }

    void SetStartingItems()
    {
        for (int i = 0; i < itemSlots.Length && i < startingItems.Count; i++)
        {
            itemSlots[i].item = startingItems[i];
        }

        for (int i = 0; i < itemSlots.Length; i++)
        {
            itemSlots[i].item = null;
        }
    }

    public bool AddItem(Item item)
    {
        for (int i = 0; i < itemSlots.Length; i++)
        {
            if(itemSlots[i].item == null)
            {
                itemSlots[i].item = item;
                return true;
            }
        }
        return false;
    }

    public bool RemoveItem(Item item)
    {
        for (int i = 0; i < itemSlots.Length; i++)
        {
            if (itemSlots[i].item == null)
            {
                itemSlots[i].item = null;
                return true;
            }
        }
        return false;
    }

    public bool IsFull()
    {
        for (int i = 0; i < itemSlots.Length; i++)
        {
            if (itemSlots[i].item == null)
            {
                return false;
            }

        }
        return true;
    }

    public bool ContainsItem(Item item)
    {
        for (int i = 0; i < itemSlots.Length; i++)
        {
            if (itemSlots[i].item == item)
            {
                return true;
            }
        }
        return false;
    }

    public int ItemCount(Item item)
    {
        int number = 0;
        for (int i = 0; i < itemSlots.Length; i++)
        {
            if(itemSlots[i].item == item)
            {
                number++;
            }
        }
        return number;
    }

    private void RefreshUI()
    {
        int i = 0;
        for(; i < startingItems.Count && i < itemSlots.Length; i++)
        {
            itemSlots[i].item = startingItems[i];
        }
        for(; i < itemSlots.Length; i++)
        {
            itemSlots[i].item = null;
        }
    }

}
