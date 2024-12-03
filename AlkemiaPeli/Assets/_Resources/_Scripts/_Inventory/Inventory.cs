using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Inventory : MonoBehaviour
{
    [SerializeField] private int slotCount = 4;   // The maximum number of hotbar slots
    [SerializeField] private List<Item> items = new List<Item>(); // List that stores all the items in inventory
    [SerializeField] private Transform itemPosition; // Item drop and hold position
    private GameObject currentHeldItem; // Currently held item

    // References to the UI elements
    [SerializeField] private List<Transform> slotBackgrounds;
    [SerializeField] private List<Image> itemHolders;
    [SerializeField] private List<TMP_Text> slotTexts;

    private Color emptySlotColor = new Color(1, 1, 1, 0.2f);
    private int selectedSlot = 0;
    [SerializeField] private int dropForce;

    private void Start()
    {
        UpdateHotbarUI();
    }

    private void Update()
    {
        HandleHotbarInput();

        // Drop item by pressing 'Q'
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

        EquipSelectedItem(); // Equip the selected item
        UpdateHotbarUI(); // Updates the hotbar UI
    }

    private void EquipSelectedItem()
    {
        // Removes the previously held item
        if (currentHeldItem != null)
        {
            Destroy(currentHeldItem);
        }

        // Check if thers an item in the selected slot
        if (selectedSlot < items.Count && items[selectedSlot] != null)
        {
            // Instantiate the items prefab at the item point
            currentHeldItem = Instantiate(items[selectedSlot].itemPrefab, itemPosition.position, itemPosition.rotation);

            // Parent it to the hold point
            currentHeldItem.transform.SetParent(itemPosition);

            // Reset local position, rotation, and scale
            currentHeldItem.transform.localPosition = Vector3.zero;
            currentHeldItem.transform.localRotation = Quaternion.identity;
            currentHeldItem.transform.localScale = items[selectedSlot].originalScale;
        }
    }

    private void UpdateHotbarUI()
{
    // Loop through each slot to update the UI
    for (int i = 0; i < slotCount; i++)
    {
        // Highlight the selected slot
        slotBackgrounds[i].GetComponent<Image>().color = (i == selectedSlot) ? Color.yellow : Color.white;

        // Set item icon or make slot empty if there's no item
        if (i < items.Count && items[i] != null)
        {
            itemHolders[i].sprite = items[i].itemIcon; // Set the item icon
            itemHolders[i].color = Color.white; // Make the icon visible
        }
        else
        {
            // Set item holder color to transparent if no item
            itemHolders[i].sprite = null; // Clear the item icon sprite
            itemHolders[i].color = emptySlotColor; // Set the icon color to transparent
        }

        // Update slot number text
        slotTexts[i].text = (i + 1).ToString(); // Set the slot number (1, 2, 3, 4, ...)
    }
}


    public bool AddItem(Item item)
    {
        if (items.Count < slotCount)
        {
            items.Add(item);
            item.originalInstance = item.gameObject; // Store reference to the original instance
            item.originalInstance.SetActive(false);   // Hide the original item in the world
            UpdateHotbarUI();
            return true;
        }
        else
        {
            Debug.Log("Inventory is full!");
            return false;
        }
    }

    public void DropItem(int slotIndex)
{
    if (slotIndex < items.Count && items[slotIndex] != null)
    {
        Item itemToDrop = items[slotIndex];

        // Re-enable and position the original item instance instead of creating a new one
        if (itemToDrop.originalInstance != null)
        {
            itemToDrop.originalInstance.SetActive(true);
            itemToDrop.originalInstance.transform.position = itemPosition.position;
            itemToDrop.originalInstance.transform.rotation = Quaternion.identity;

            // if the item doesnt have a rigidbody, add one
            Rigidbody rb = itemToDrop.originalInstance.GetComponent<Rigidbody>();
            if (rb == null)
                rb = itemToDrop.originalInstance.AddComponent<Rigidbody>();

            // if the item doesnt have a collider, add one
            if (!itemToDrop.originalInstance.GetComponent<Collider>())
                itemToDrop.originalInstance.AddComponent<BoxCollider>();

            rb.isKinematic = false;

            rb.useGravity = true;

            // Shoots the dropped item a little when dropped
            Vector3 forwardForce = itemPosition.forward * dropForce; // Controls how much the item shoots forward
            rb.velocity = Vector3.zero; // Reset any existing velocity
            rb.AddForce(forwardForce, ForceMode.Impulse);
        }

        items.RemoveAt(slotIndex);

        UpdateHotbarUI();
    }
}

}
