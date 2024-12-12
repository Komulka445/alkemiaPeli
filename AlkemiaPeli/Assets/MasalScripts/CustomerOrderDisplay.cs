using System.Collections;
using UnityEngine;

public class CustomerOrderDisplay : MonoBehaviour
{
    public GameObject orderUIPrefab; // Drag and drop your UI prefab here (e.g., an image to show the order)
    public Transform orderDisplayPoint; // Where the order UI should appear (e.g., above the head)
    public AudioClip customerSound; // Sound that plays when the customer arrives
    public float displayDuration = 5f; // How long the order is displayed

    private GameObject currentOrderUI;
    private AudioSource audioSource;

    void Start()
    {
        // Attach an AudioSource if none exists
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the trigger is tagged as "Customer"
        if (other.CompareTag("Customer"))
        {
            DisplayOrder();
            PlayCustomerSound();
        }
    }

    private void DisplayOrder()
    {
        // Ensure the order UI spawns at the correct world-space position
        if (orderUIPrefab != null && orderDisplayPoint != null)
        {
            // Instantiate the UI at the world position of the OrderDisplayPoint
            currentOrderUI = Instantiate(orderUIPrefab, orderDisplayPoint.position, Quaternion.identity);

            // Parent the order UI to the OrderDisplayPoint so it stays above the customer
            currentOrderUI.transform.SetParent(orderDisplayPoint, true);

            // Ensure no unintentional offsets occur
            currentOrderUI.transform.localPosition = Vector3.zero;

            StartCoroutine(RemoveOrderAfterDelay());
        }
    }


    private IEnumerator RemoveOrderAfterDelay()
    {
        // Remove the order UI after the specified duration
        yield return new WaitForSeconds(displayDuration);
        if (currentOrderUI != null)
        {
            Destroy(currentOrderUI);
        }
    }

    private void PlayCustomerSound()
    {
        if (customerSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(customerSound);
        }
    }
}
