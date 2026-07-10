using UnityEngine;
using System;
using System.Collections.Generic;
using Crafting;

[Serializable]
public class ItemData
{
    public string name;
    public int quantity;
    public string description;
    public string type;
    public Sprite icon;
    public Ingrediant ingrediant;

    //public ItemData(string name, int quantity, Sprite icon, string description, string type, Ingrediant ingrediant)
    //{
    //    this.name = name;
    //    this.quantity = quantity;
    //    this.icon = icon;
    //    this.description = description;
    //    this.type = type;
    //    this.ingrediant = ingrediant;
    //}
}
