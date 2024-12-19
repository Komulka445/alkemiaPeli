using System;
using UnityEngine;

[Serializable]
public class Item : MonoBehaviour
{
    public string itemName;
    public Sprite itemIcon;
    public GameObject itemPrefab;
    
    // Reference to the original GameObject instance
    public GameObject originalInstance;

    // Store the item's original scale
    [HideInInspector] public Vector3 originalScale;

    private void Awake()
    {
        // Save the original scale of the item at the start
        originalScale = transform.localScale;
    }
}
