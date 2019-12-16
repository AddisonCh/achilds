using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ItemContainer
{
    bool ContainsItem(Item item);
    bool RemoveItem(Item item);
    bool AddItem(Item item);
    bool IsFull();
}
