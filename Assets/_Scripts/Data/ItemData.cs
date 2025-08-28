using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum ItemType
{
    Weapon,
    Armor
}

[CreateAssetMenu(fileName = "Item", menuName = "New Item")]
public class ItemData : ScriptableObject
{
    public string itemName;
    public string itemText;
    public ItemType itemType;
    public Sprite icon;

    public float attackBonus;
    public float defenseBonus;
    public float hpBonus;
    public float mpBonus;
}
