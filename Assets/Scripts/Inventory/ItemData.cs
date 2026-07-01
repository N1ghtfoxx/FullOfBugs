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
}
