using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightItem : MonoBehaviour
{
    public Camera playerCamera;
    private GameObject previousItem;

    private void Update()
    {
        LookingAtItem();
    }

    void LookingAtItem()
    {
        // Shoots a ray from the center of the screen
        Ray ray = playerCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        RaycastHit hit;

        // Checks if the ray hits anything or not
        if (Physics.Raycast(ray, out hit))
        {
            // If the hit objects tag is "Item", highlight it, else do nothing.
            if (hit.collider.CompareTag("Item"))
            {
                if (previousItem != hit.collider.gameObject)
                {
                    // Keeps track of the highlighted object
                    previousItem = hit.collider.gameObject;
                    previousItem.layer = LayerMask.NameToLayer("Outlined");
                    Debug.Log("Hit an Item and set its layer to Outlined");
                }
            }
            // When no longer looking at an object remove the highlight.
            else
            {
                ResetPreviousItem();
            }
        }
        else
        {
            ResetPreviousItem();
        }
    }

    // Removes the highlight based on the 'previousItem' variables input.
    // Sets the items layer to Default which doesnt get highlighted.
    void ResetPreviousItem()
    {
        if (previousItem != null)
        {
            previousItem.layer = LayerMask.NameToLayer("Default");
            previousItem = null;
            Debug.Log("Set the layer to Default");
        }
    }
}
