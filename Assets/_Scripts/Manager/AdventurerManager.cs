using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdventurerManager : MonoBehaviour
{
    public List<Adventurer> allAdventurers = new List<Adventurer>();

    void Start()
    {
        StartAdventurer();
    }

    void StartAdventurer()
    {
        MakeAdventurer("������", AdventurerType.Knight);
        MakeAdventurer("�Ƹ���", AdventurerType.Warrior);
        MakeAdventurer("������", AdventurerType.Thief);
    }

    public Adventurer MakeAdventurer(string name, AdventurerType type)
    {
        Adventurer newAdventurer = new Adventurer(name, type);
        allAdventurers.Add(newAdventurer);

        Debug.Log($"+{name}");
        return newAdventurer;
    }


    public List<Adventurer> GetAllAdventurer()
    {
        return allAdventurers;
    }


    public void HealAdventurers()
    {
        foreach (Adventurer adventurer in allAdventurers)
        {
            adventurer.currentHp = adventurer.maxHp;
            adventurer.currentMp = adventurer.maxMp;
        }
        Debug.Log("ȸ��");
    }

    public Adventurer GetEquip(ItemSlot slot)
    {
        if (slot == null) return null;

        foreach (Adventurer adv in allAdventurers)
        {
            if (adv.equipWeaponSlot == slot || adv.equipArmorSlot == slot)
            {
                return adv;
            }
        }

        return null; 
    }


}