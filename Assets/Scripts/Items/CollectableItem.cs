using UnityEngine;

public class CollectableItem : CollectableMapItem
{
    void Awake()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = _data.icon;
    }

    public void SetItem(ItemData data)
    {
        _data = data;
        gameObject.GetComponent<SpriteRenderer>().sprite = _data.icon;
    }
}
