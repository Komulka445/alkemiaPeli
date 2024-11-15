using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializeField] private int slotCount = 4;
    [SerializeField] private List<Item> items = new List<Item>();
    [SerializeField] private Transform dropPoint;

    [SerializeField] private List<Image> slotImages;
    [SerializeField] private Color emptySlotColor = new Color(1, 1, 1, 0.2f);

    private int selectedSlot = 0;

    private void Start()
    {
        UpdateHotbarUI();
    }

    private void Update()
    {
        HandleHotbarInput();

        if (Input.GetKeyDown(KeyCode.Q))
        {
            DropItem(selectedSlot);
        }
    }

    private void HandleHotbarInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) selectedSlot = 0;
        else if (Input.GetKeyDown(KeyCode.Alpha2)) selectedSlot = 1;
        else if (Input.GetKeyDown(KeyCode.Alpha3)) selectedSlot = 2;
        else if (Input.GetKeyDown(KeyCode.Alpha4)) selectedSlot = 3;

        UpdateHotbarUI();
    }

    private void UpdateHotbarUI()
    {
        for (int i = 0; i < slotImages.Count; i++)
        {
            slotImages[i].color = i == selectedSlot ? Color.yellow : Color.white;

            if (i < items.Count && items[i] != null)
                slotImages[i].sprite = items[i].itemIcon;
            else
                slotImages[i].color = emptySlotColor;
        }
    }


    public bool AddItem(Item item)
    {
        if (items.Count < slotCount)
        {
            items.Add(item);
            UpdateHotbarUI();
            return true;
        }
        else
        {
            Debug.Log("Inventory is full!");
            return false;
        }
    }

    // Drop the item from the selected slot
    public void DropItem(int slotIndex)
    {
        if (slotIndex < items.Count && items[slotIndex] != null)
        {
            // Instantiate item prefab at drop point
            Instantiate(items[slotIndex].itemPrefab, dropPoint.position, Quaternion.identity);

            // Remove from inventory and update UI
            items.RemoveAt(slotIndex);
            UpdateHotbarUI();
        }
    }
}
