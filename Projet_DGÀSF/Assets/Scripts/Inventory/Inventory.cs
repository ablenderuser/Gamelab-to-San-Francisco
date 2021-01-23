using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public List<Item> characterItems;
    public List<GameObject> m_InventoryDisplay;

    private ItemDatabase itemDatabase;
    private int itemNum;
	
	public void Start()
	{
        characterItems = new List<Item>();
        itemDatabase = new ItemDatabase();
        itemNum = 0;
	}

    public void GiveItem(string title)
    {
        Item itemToAdd = itemDatabase.GetItem(title);
        characterItems.Add(itemToAdd);
        m_InventoryDisplay[itemNum].GetComponent<Image>();
        itemNum++;

        Debug.Log("Added item: " + itemToAdd.title);
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
