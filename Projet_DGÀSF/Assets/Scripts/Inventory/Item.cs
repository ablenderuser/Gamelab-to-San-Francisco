using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Item
{
	public int id;
	public string title;
	public string description;
	public Sprite icon;
	
	public Item(int id, string title, string description, string sprite)
	{
		this.id = id;
		this.title = title;
		this.description = description;
		this.icon = Resources.Load<Sprite>(sprite);
	}

	public Item(int id, string title, string description, string altas, string sprite)
	{
		this.id = id;
		this.title = title;
		this.description = description;
		Sprite[] atlas = Resources.LoadAll<Sprite>(altas);
		this.icon = atlas.Single(s => s.name == sprite);
	}
	
	public Item(Item item)
	{
		this.id = item.id;
		this.title = item.title;
		this.description = item.description;
		this.icon = Resources.Load<Sprite>(item.title);
	}
}
