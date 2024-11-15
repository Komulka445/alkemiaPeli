using System;
using UnityEngine;

[Serializable]
public class Item : MonoBehaviour
{
    public string itemName;
    public Sprite itemIcon;
    public GameObject itemPrefab; // Reference to the 3D prefab for dropping

    public Item(string name, Sprite icon, GameObject prefab)
    {
        itemName = name;
        itemIcon = icon;
        itemPrefab = prefab;
    }
}
