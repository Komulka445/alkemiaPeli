using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
public class Inventory : MonoBehaviour
{
    public GameObject List;
    private TextMeshProUGUI ListText;
    private string[] items;
    void Start()
    {
        ListText = List.GetComponent<TextMeshProUGUI>();
        items = new string[10];
        items[0] = "esine1";
        items[1] = "esine2";
        items[2] = "esine3";
        foreach (var item in items)
        {
            ListText.text += item + "\n";
        }
    }
    void Update()
    {
    }
}
