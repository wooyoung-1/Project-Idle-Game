using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class ItemSlot : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Image icon;
    private ItemData currentItem;
    private Outline outline;

    [SerializeField] private GameObject equipMark;

    public bool IsMark()
    {
        return equipMark.activeSelf;
    }

    void Awake()
    {
        outline = this.GetComponent<Outline>();
    }

    public void SetItem(ItemData item)
    {
        currentItem = item;
        if (item != null)
        {
            icon.sprite = item.icon;
            icon.enabled = true;
        }
        else
        {
            icon.sprite = null;
            icon.enabled = false;
        }

        equipMark.SetActive(false);
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        if (currentItem == null || eventData.button != PointerEventData.InputButton.Left) return;

        Adventurer adventurer = GameManager.Instance.pickAdventurer;

        Adventurer itemOwner = GameManager.Instance.adventurerManager.GetEquip(this);
        if (itemOwner != null && itemOwner != adventurer)
        {
            Debug.Log($"¿Â¬¯¡ﬂ");
            return;
        }

        if (this == adventurer.equipWeaponSlot || this == adventurer.equipArmorSlot)
        {
            adventurer.UnequipItem(currentItem.itemType);
            SetMark(false);
        }
        else
        {
            ItemSlot oldEquippedSlot = null;
            if (currentItem.itemType == ItemType.Weapon)
            {
                oldEquippedSlot = adventurer.equipWeaponSlot;
            }
            else if (currentItem.itemType == ItemType.Armor)
            {
                oldEquippedSlot = adventurer.equipArmorSlot;
            }

            adventurer.EquipItem(currentItem, this);

            SetMark(true);

            if (oldEquippedSlot != null)
            {
                oldEquippedSlot.SetMark(false);
            }
        }
        GameManager.Instance.uiManager.ShowTooltip(currentItem, transform as RectTransform);

    }


    public void SetMark(bool isActive)
    {
        if (equipMark != null)
        {
            equipMark.SetActive(isActive);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (currentItem != null)
        {
            outline.enabled = true;
            GameManager.Instance.uiManager.ShowTooltip(currentItem, transform as RectTransform);
        }  
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        outline.enabled = false;
        GameManager.Instance.uiManager.hideTooltip();
    }
}
