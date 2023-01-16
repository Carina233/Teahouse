using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

[System.Serializable]
public abstract class ItemBase : ScriptableObject
{
    [SerializeField]
    private string itemID;
    public string _itemID
    {
        get
        {
            return itemID;
        }
    }

    [SerializeField]
    private string itemName;
    public string _itemName
    {
        get
        {
            return itemName;
        }
    }

    [SerializeField]
    private ItemType itemType;
    public ItemType _itemType
    {
        get
        {
            return itemType;
        }
        protected set
        {
            itemType = value;
        }
    }

    public enum ItemType { 

    single,
    mixtable
    }

    [SerializeField]
    private ItemLevel itemLevel;
    public ItemLevel _itemLevel
    {
        get
        {
            return itemLevel;
        }
    }

    public enum ItemLevel
    {
        primary,
        middle,
        senior
    }

    [SerializeField]
    private Sprite icon;
    public Sprite Icon
    {
        get
        {
            return icon;
        }
    }

    [SerializeField, TextArea]
    private string description;
    public string Description
    {
        get
        {
            return description;
        }
    }

    public interface IUsable
    {
        void OnUse();
    }



}
