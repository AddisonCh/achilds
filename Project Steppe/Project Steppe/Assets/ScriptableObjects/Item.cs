using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item")]
public class Item : ScriptableObject
{
    public string itemName;
    public Sprite sprite;
}

[System.Serializable]
public struct ItemAmount
{
    public Item item;
    [Range(1,99)]
    public int Amount;
}
