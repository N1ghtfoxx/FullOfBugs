using UnityEngine;
using System.Collections.Generic;

public class ItemDescriptions : Singleton<ItemDescriptions>
{
    [System.Serializable]
    public class Entry
    {
        public string itemName;
        [TextArea] public string description;
    }

    [SerializeField] private List<Entry> _entries;
    private Dictionary<string, string> _lookup;

    protected override void Awake()
    {
        base.Awake();
        _lookup = new Dictionary<string, string>();
        foreach (Entry entry in _entries)
        {
            _lookup[entry.itemName] = entry.description;
        }
    }

    public string GetDescription(string itemName)
    {
        return _lookup.TryGetValue(itemName, out string desc) ? desc : "";
    }   
}
