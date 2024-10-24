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
        Ray ray = playerCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.CompareTag("Item"))
            {
                if (previousItem != hit.collider.gameObject)
                {

                    previousItem = hit.collider.gameObject;
                    previousItem.layer = LayerMask.NameToLayer("Outlined");
                    Debug.Log("Hit an Item and set its layer to Outlined");
                }
            }
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

    void ResetPreviousItem()
    {
        if (previousItem != null)
        {
            previousItem.layer = LayerMask.NameToLayer("Default");
            previousItem = null;
        }
    }
}
