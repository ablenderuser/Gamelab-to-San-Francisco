using System.Collections;
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
        if (m_InventoryDisplay != null)
        {
            if (InInventory("Clé"))
            {
                m_InventoryDisplay.SetActive(true);
            }
            else
            {
                m_InventoryDisplay.SetActive(false);
            }
        }
    }

    public void GiveItem(string title)
    {
        Item itemToAdd = itemDatabase.GetItem(title);
        characterItems.Add(itemToAdd);
        //Debug.Log("Added item: " + itemToAdd.title);
        InInventory(title);
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
