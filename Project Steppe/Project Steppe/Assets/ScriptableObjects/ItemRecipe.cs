using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Recipe")]
public class ItemRecipe : ScriptableObject
{
    public List<ItemAmount> Materials;
    public List<ItemAmount> Results;

    public bool CanCraft(ItemContainer container)
    {
        foreach (ItemAmount itemAmount in Materials)
        {
            if(ItemContainer.ItemCount(itemAmount.Item) < itemAmount.Amount)
            {
                return false;
            }
            return true;
        }
    }

    public void Craft(ItemContainer container)
    {
        if(CanCraft(container))
        {
            foreach (ItemAmount amount in Materials)
            {
                for (int i = 0; i < amount.Amount; i++)
                {
                    container.RemoveItem(amount.item);
                }
            }
            foreach (ItemAmount amount in Results)
            {
                for (int i = 0; i < amount.Amount; i++)
                {
                    container.AddItem(amount.item);
                }
            }
        }
    }
}