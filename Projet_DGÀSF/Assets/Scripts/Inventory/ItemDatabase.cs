﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase
{
    private List<Item> items;

    public ItemDatabase()
    {
        this.items = new List<Item>();

        Item clef = new Item(0, "Clé", "Une jolie clé");
        this.items.Add(clef);

        Item bread = new Item(0, "Pain", "Un bon bout de pain");
        this.items.Add(bread);
    }

	public Item GetItem(string itemName)
	{
		return items.Find(item => item.title == itemName);
	}
}
