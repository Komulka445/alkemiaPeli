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

    public TMP_Text ggtext;
    public GameObject panel;
    // Start is called before the first frame update
    void Start()
    {
        cauldronText.text = "Empty!";
    }

    void OnCollisionEnter(Collision collision)
{
    if (collision.gameObject.tag == "Ingredient" || collision.gameObject.tag == "Item")
    {
        if (cauldronText.text == "Empty!")
        {
            cauldronText.text = "";
        }

        collidingItem = collision.gameObject;
        cauldronText.text += collidingItem.name + "\n";
        Destroy(collidingItem);

        int lineCount = cauldronText.text.Split('\n').Length - 1;

        if (lineCount >= 2)
        {
            panel.SetActive(true);
        }
        else
        {
            panel.SetActive(false);
        }
    }
}


    // Update is called once per frame
    void Update()
    {
    }
}
