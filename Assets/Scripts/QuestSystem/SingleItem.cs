using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "SingleItem", menuName = "Item/single")]
public class SingleItem :ItemBase
{
    public SingleItem()
    {
        _itemType = ItemType.single;
    }
    public void OnUse()
    {
        Debug.Log("UseSingleItem");
    }
}
