using UnityEngine;
using System;
using System.Collections.Generic;

[Serializable]
public class SaveData
{
    public int level;
    public float playtime;
    public float posX;
    public float posY;
    public List<ItemData> inventory;
    public List<ItemData> chest;
}
