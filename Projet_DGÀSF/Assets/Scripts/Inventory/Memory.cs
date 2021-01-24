using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static public class Memory
{
    //static public List<string> itemName;
    //static public List<int> itemNum;
    static public string itemName;
    static public int itemNum;

    static public void Memorize(string name, int num)
    {
        //itemName.Add(name);
        //itemNum.Add(num);
        itemName = name;
        itemNum = num;
    }

    /*static public void Forget(string name, int num)
    {
        itemName.Remove(name);
        itemNum.Remove(num);
    }*/
}
