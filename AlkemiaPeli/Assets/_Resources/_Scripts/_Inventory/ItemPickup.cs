using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public float pickupDistance = 5f;  // Maximum distance to pick up items
    public Camera playerCamera;        // Reference to the player's camera
    public Inventory playerInventory;  // Reference to the player's inventory script

    void Update()
    {
        // Check for the pickup input, e.g., left mouse button
        if (Input.GetKeyDown(KeyCode.E))
        {
            AttemptPickup();
        }
    }

    void AttemptPickup()
    {
        // Create a ray from the center of the screen
        Ray ray = playerCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        RaycastHit hit;

        // Check if the ray hits any object
        if (Physics.Raycast(ray, out hit, pickupDistance))
        {
            // Check if the object has the "Item" tag
            if (hit.collider.CompareTag("Item"))
            {
                Item item = hit.collider.GetComponent<Item>();

                // If the object has an Item component, add it to the inventory
                if (item != null)
                {
                    if (playerInventory.AddItem(item))
                    {
                        Debug.Log("Picked up: " + item.itemName);

                        // Disable the item object in the scene without destroying it
                        hit.collider.gameObject.SetActive(false);
                    }
                }
            }
        }
    }
}
