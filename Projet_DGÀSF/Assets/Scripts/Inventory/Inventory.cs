using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public List<Item> characterItems;
    public GameObject m_InventoryDisplay;

    private ItemDatabase itemDatabase;
	
	public void Start()
	{
        characterItems = new List<Item>();
        itemDatabase = new ItemDatabase();
        //InInventory("Clé");
	}

    public void Update()
    {
        Debug.Log(gameObject.GetComponent<Inventory>().InInventory("Bout de pain"));
        Debug.Log(characterItems.Count);
        Debug.Log("Bonjour");

        for (int i = 0; i < characterItems.Count; i++)
        {
            Debug.Log(characterItems[i].title);
            GameObject.Find("InventoryItem").GetComponent<Image>().sprite = characterItems[i].icon;
        }

        /*if (m_InventoryDisplay != null)
        {
            if (InInventory("Clé"))
            {
                m_InventoryDisplay.SetActive(true);
            }
            else
            {
                m_InventoryDisplay.SetActive(false);
            }
        }*/
    }

    public void GiveItem(string title)
    {
        Item itemToAdd = itemDatabase.GetItem(title);
        characterItems.Add(itemToAdd);
        //Debug.Log("Added item: " + itemToAdd.title);
        //m_InventoryDisplay.GetComponentInChildren<Image>().sprite = itemToAdd.icon;
        //InInventory(title);
    }

    public bool InInventory(string title)
    {
        Item item = itemDatabase.GetItem(title);
        if (characterItems.Contains(item))
        {
            //Debug.Log(title + " is in inventory");
            return true;
        }
        //Debug.Log(title + " is not in inventory");
        return false;
    }
}
