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

        /*for(int i=0; i<Memory.itemName.Count; i++)
        {
            for (int j=0; j<Memory.itemNum[i]; j++)
            {
                GiveItem(Memory.itemName[i]);
            }
        }*/
        for (int j = 0; j < Memory.itemNum; j++)
        {
            GiveItem(Memory.itemName);
        }
    }

    public void GiveItem(string title)
    {
        Item itemToAdd = itemDatabase.GetItem(title);
        characterItems.Add(itemToAdd);
        m_InventoryDisplay[itemNum].GetComponent<Image>().sprite = itemToAdd.icon;
        itemNum++;
    }

    public bool InInventory(string title)
    {
        Item item = itemDatabase.GetItem(title);
        if (characterItems.Contains(item))
        {
            return true;
        }
        return false;
    }
}
