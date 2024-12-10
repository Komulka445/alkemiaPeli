using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;
using Unity.VisualScripting;

public class Cooking : MonoBehaviour
{
    public TextMeshPro cauldronText;
    public GameObject player;
    public GameObject collidingItem;
    public GameObject inventoryCanvas;
    // Start is called before the first frame update
    void Start()
    {
        cauldronText.text = "Empty!";
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ingredient" || collision.gameObject.tag == "Item")
        {
            if(cauldronText.text == "Empty!")
            {
                cauldronText.text = "";
            }
            collidingItem = collision.gameObject;
            cauldronText.text += collidingItem.name + "\n";
            //Debug.Log(collidingItem.name+" "+cauldronText.text);
            Destroy(collidingItem);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
