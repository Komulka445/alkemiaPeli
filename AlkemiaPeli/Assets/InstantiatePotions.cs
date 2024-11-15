using UnityEngine;

public class ItemInstantiator : MonoBehaviour
{
    [SerializeField] private GameObject itemPrefab;    // The item prefab to instantiate
    [SerializeField] private float interactionRange = 10f; // The range within which the player can interact
    [SerializeField] private Transform spawnPosition;  // The position from which the item will spawn (optional)

    private bool isPlayerInRange = false;  // Track if the player is in range

    private void Update()
    {
        // Check if the player is in range and presses "T"
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.T))
        {
            InstantiateItem();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the player enters the range of this object
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Check if the player exits the range of this object
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
        }
    }

    private void InstantiateItem()
    {
        // If there's a spawn position, spawn the item there, otherwise, spawn at the object's position
        Vector3 spawnPos = spawnPosition != null ? spawnPosition.position : transform.position;

        // Instantiate the item prefab
        Instantiate(itemPrefab, spawnPos, Quaternion.identity);

        Debug.Log("Item instantiated!");
    }
}
