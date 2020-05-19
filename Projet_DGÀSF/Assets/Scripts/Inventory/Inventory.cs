using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Item> characterItems;
    private ItemDatabase itemDatabase;
	
	public void Start()
	{
        characterItems = new List<Item>();
        itemDatabase = new ItemDatabase();
        InInventory("Clé");
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
