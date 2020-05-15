using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    public List<Item> items = new List<Item>();
	
	private void Awake()
	{
		BuildDatabase();
	}
	
	public Item GetItem(int id)
	{
		return items.Find(item => item.id == id);
	}
	/*
	public Item GetItem(string itemName)
	{
		return items.Find(item => item.title == itemName);
	}
	*/
	void BuildDatabase()
	{
		items = new List<Item>(
		new Item(0, "Clef", "Une jolie clef")
		);
		//items = new Item(0, "Clef", "Une jolie clef");
	}
}
