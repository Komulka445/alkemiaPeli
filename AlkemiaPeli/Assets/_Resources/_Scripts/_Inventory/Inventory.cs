using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Inventory : MonoBehaviour
{
    [SerializeField] private int slotCount = 4;   // Hotbarin enimmäismäärä paikkoja
    [SerializeField] private List<Item> items = new List<Item>(); // Lista, joka sisältää kaikki tavarat inventaariossa
    [SerializeField] private Transform itemPosition; // Tavaran tiputus- ja pidätyspaikka
    private GameObject currentHeldItem; // Tällä hetkellä kädessä oleva tavara

    // Viittaukset käyttöliittymän elementteihin
    [SerializeField] private List<Transform> slotBackgrounds; // Taustat hotbar-paikoille
    [SerializeField] private List<Image> itemHolders; // Kuvakkeet, jotka esittävät tavaroita
    [SerializeField] private List<TMP_Text> slotTexts; // Tekstit, jotka näyttävät paikkojen numerot

    private Color emptySlotColor = new Color(1, 1, 1, 0.2f); // Väri tyhjille paikoille
    private int selectedSlot = 0; // Tällä hetkellä valittu paikka
    [SerializeField] private int dropForce; // Voima, jolla tavara heitetään

    private void Start()
    {
        UpdateHotbarUI(); // Päivittää hotbarin käyttöliittymän
    }

    private void Update()
    {
        HandleHotbarInput(); // Käsittelee näppäimistösyötteen hotbar-paikkojen valitsemiseen

        // Pudota tavara painamalla 'Q'
        if (Input.GetKeyDown(KeyCode.Q))
        {
            DropItem(selectedSlot); // Pudottaa tällä hetkellä valitun paikan tavaran
        }
    }

    private void HandleHotbarInput()
    {
        // Vaihda hotbarin paikka näppäimistön numeroilla (1-4)
        if (Input.GetKeyDown(KeyCode.Alpha1)) selectedSlot = 0;
        else if (Input.GetKeyDown(KeyCode.Alpha2)) selectedSlot = 1;
        else if (Input.GetKeyDown(KeyCode.Alpha3)) selectedSlot = 2;
        else if (Input.GetKeyDown(KeyCode.Alpha4)) selectedSlot = 3;

        EquipSelectedItem(); // Varustaa valitun tavaran
        UpdateHotbarUI(); // Päivittää hotbarin käyttöliittymän
    }

    private void EquipSelectedItem()
    {
        // Poistaa aiemmin kädessä olleen tavaran
        if (currentHeldItem != null)
        {
            Destroy(currentHeldItem);
        }

        // Tarkistaa, onko valitussa paikassa tavara
        if (selectedSlot < items.Count && items[selectedSlot] != null)
        {
            // Luo tavaran prefab itemPosition-paikassa
            currentHeldItem = Instantiate(items[selectedSlot].itemPrefab, itemPosition.position, itemPosition.rotation);

            // Asettaa tavaran vanhemmaksi itemPositionille
            currentHeldItem.transform.SetParent(itemPosition);

            // Nollaa paikallisen sijainnin, rotaation ja skaalauden
            currentHeldItem.transform.localPosition = Vector3.zero;
            currentHeldItem.transform.localRotation = Quaternion.identity;
            currentHeldItem.transform.localScale = items[selectedSlot].originalScale;
        }
    }

    private void UpdateHotbarUI()
    {
        // Käy jokainen hotbarin paikka läpi ja päivittää käyttöliittymän
        for (int i = 0; i < slotCount; i++)
        {
            // Korostaa valitun paikan
            slotBackgrounds[i].GetComponent<Image>().color = (i == selectedSlot) ? Color.yellow : Color.white;

            // Asettaa tavaran kuvakkeen tai näyttää paikan tyhjänä
            if (i < items.Count && items[i] != null)
            {
                itemHolders[i].sprite = items[i].itemIcon; // Asettaa tavaran kuvakkeen
                itemHolders[i].color = Color.white; // Näyttää kuvakkeen
            }
            else
            {
                // Asettaa kuvakkeen värittömäksi, jos paikka on tyhjä
                itemHolders[i].sprite = null; // Poistaa kuvakkeen
                itemHolders[i].color = emptySlotColor; // Asettaa kuvakkeen läpinäkyväksi
            }

            // Päivittää paikan numerotekstin
            slotTexts[i].text = (i + 1).ToString(); // Näyttää paikan numeron (1, 2, 3, 4, ...)
        }
    }

    public bool AddItem(Item item)
    {
        // Lisää uuden tavaran, jos inventaario ei ole täynnä
        if (items.Count < slotCount)
        {
            items.Add(item);
            item.originalInstance = item.gameObject; // Tallentaa viittauksen alkuperäiseen tavaraan
            item.originalInstance.SetActive(false);   // Piilottaa alkuperäisen tavaran maailmasta
            UpdateHotbarUI(); // Päivittää käyttöliittymän
            return true;
        }
        else
        {
            Debug.Log("Inventory is full!"); // Ilmoittaa, että inventaario on täynnä
            return false;
        }
    }

    public void DropItem(int slotIndex)
    {
        // Tarkistaa, onko paikassa tavara ja pudottaa sen
        if (slotIndex < items.Count && items[slotIndex] != null)
        {
            Item itemToDrop = items[slotIndex];

            // Aktivoi uudelleen alkuperäisen tavaran ja asettaa sen sijainnin
            if (itemToDrop.originalInstance != null)
            {
                itemToDrop.originalInstance.SetActive(true);
                itemToDrop.originalInstance.transform.position = itemPosition.position;
                itemToDrop.originalInstance.transform.rotation = Quaternion.identity;

                // Lisää Rigidbody, jos tavaralla ei ole sellaista
                Rigidbody rb = itemToDrop.originalInstance.GetComponent<Rigidbody>();
                if (rb == null)
                    rb = itemToDrop.originalInstance.AddComponent<Rigidbody>();

                // Lisää Collider, jos tavaralla ei ole sellaista
                if (!itemToDrop.originalInstance.GetComponent<Collider>())
                    itemToDrop.originalInstance.AddComponent<BoxCollider>();

                rb.isKinematic = false;
                rb.useGravity = true;

                // Lisää voimaa, jolla tavara heitetään eteenpäin
                Vector3 forwardForce = itemPosition.forward * dropForce; // Määrittää voiman suunnan
                rb.velocity = Vector3.zero; // Nollaa aiemman nopeuden
                rb.AddForce(forwardForce, ForceMode.Impulse);
            }

            items.RemoveAt(slotIndex); // Poistaa tavaran inventaariosta
            UpdateHotbarUI(); // Päivittää käyttöliittymän
        }
    }
}
