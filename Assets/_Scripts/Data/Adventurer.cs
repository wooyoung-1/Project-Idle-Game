using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum AdventurerType
{
    Knight,
    Warrior,
    Thief
}

[Serializable]
public class Adventurer
{
    public string name;
    public AdventurerType type;
    public int level;
    public float attack;
    public float defense;
    public float maxHp;
    public float currentHp;
    public float maxMp;
    public float currentMp;
    public long maxExp;
    public long currentExp;

    public ItemData equipWeapon;
    public ItemData equipArmor;

    public ItemSlot equipWeaponSlot;
    public ItemSlot equipArmorSlot;

    public Adventurer(string adventurerName, AdventurerType adventurerType)
    {
        name = adventurerName;
        type = adventurerType;
        level = 1;
        SetStartStats();
    }

    void SetStartStats()
    {
        currentExp = 0;
        maxExp = 100;

        switch (type)
        {
            case AdventurerType.Knight:
                attack = 15f;
                defense = 20f;
                maxHp = 120f;
                maxMp = 30f;
                break;

            case AdventurerType.Warrior:
                attack = 25f;
                defense = 10f;
                maxHp = 100f;
                maxMp = 20f;
                break;

            case AdventurerType.Thief:
                attack = 20f;
                defense = 15f;
                maxHp = 80f;
                maxMp = 40f;
                break;
        }

        currentHp = maxHp;
        currentMp = maxMp;
    }

    public void GetExp(long a)
    {
        currentExp += a;
        if (currentExp >= maxExp)
        {
            GoLevelUp();
        }
    }

    void GoLevelUp()
    {
        level++;
        currentExp = 0;
        maxExp += 50;

        maxHp += 20;
        maxMp += 10;
        attack += 5;
        defense += 3;

        currentHp = maxHp;
        currentMp = maxMp;

        Debug.Log($"·¹º§¾÷ {level}");
    }



    public ItemData EquipItem(ItemData newItem, ItemSlot slot)
    {
        ItemData oldItem = null;

        if (newItem.itemType == ItemType.Weapon)
        {
            oldItem = equipWeapon;
            if (equipWeaponSlot != null) equipWeaponSlot = null;

            if (oldItem != null)
            {
                attack -= oldItem.attackBonus;
                defense -= oldItem.defenseBonus;
                maxHp -= oldItem.hpBonus;
            }
            equipWeapon = newItem;
            equipWeaponSlot = slot;
        }
        else if (newItem.itemType == ItemType.Armor)
        {
            oldItem = equipArmor;
            if (equipArmorSlot != null) equipArmorSlot = null;

            if (oldItem != null)
            {
                attack -= oldItem.attackBonus;
                defense -= oldItem.defenseBonus;
                maxHp -= oldItem.hpBonus;
            }
            equipArmor = newItem;
            equipArmorSlot = slot;
        }

        attack += newItem.attackBonus;
        defense += newItem.defenseBonus;
        maxHp += newItem.hpBonus;
        currentHp = maxHp;

        return oldItem;
    }

    public ItemData UnequipItem(ItemType type)
    {
        ItemData unequippedItem = null;
        if (type == ItemType.Weapon && equipWeapon != null)
        {
            unequippedItem = equipWeapon;
            equipWeapon = null;
            equipWeaponSlot = null;
        }
        else if (type == ItemType.Armor && equipArmor != null)
        {
            unequippedItem = equipArmor;
            equipArmor = null;
            equipArmorSlot = null;
        }

        if (unequippedItem != null)
        {
            attack -= unequippedItem.attackBonus;
            defense -= unequippedItem.defenseBonus;
            maxHp -= unequippedItem.hpBonus;
            currentHp = maxHp;
        }
        return unequippedItem;
    }

    public bool IsEquip(ItemData item)
    {
        if (item == null) return false;
        return equipWeapon == item || equipArmor == item;
    }
}