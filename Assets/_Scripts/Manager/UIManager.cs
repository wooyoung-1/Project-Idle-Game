using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class UIManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject playerMenuUI;
    [SerializeField] private GameObject npcMenuUI;
    [SerializeField] private GameObject statusUI;
    [SerializeField] private GameObject equipUI;

    [SerializeField] private GameObject backButton;

    [SerializeField] private UIImageEvent uIImageEvent;

    [SerializeField] private GameObject click;
    [SerializeField] private GameObject gold;

    [Header("statusUI")]
    [SerializeField] private Text _NAME;
    [SerializeField] private Text _LV;
    [SerializeField] private Text _HP;
    [SerializeField] private Text _ATK;
    [SerializeField] private Text _DF;

    [Header("itemUI")]
    [SerializeField] private GameObject slotPrefab;
    [SerializeField] private Transform inven;

    [SerializeField] private InventoryManager inventoryManager;

    [Header("ToolTip")]
    [SerializeField] private GameObject toolTip;
    [SerializeField] private GameObject toolTip_Name;
    [SerializeField] private GameObject toolTip_Hp;
    [SerializeField] private GameObject toolTip_Atk;
    [SerializeField] private GameObject toolTip_Df;
    [SerializeField] private GameObject toolTip_Text;
    [SerializeField] private GameObject toolTip_Eq;

    [SerializeField] private RectTransform toolTipRect;
    [SerializeField] private Vector2 tooltipOffset = new Vector2(100f, -100f);

    private ItemSlot[] slots;

    void Awake()
    {
        slots = new ItemSlot[inventoryManager.inventorySize];

        for (int i = 0; i < inventoryManager.inventorySize; i++)
        {
            GameObject slotObj = Instantiate(slotPrefab, inven);

            ItemSlot slot = slotObj.GetComponent<ItemSlot>();
            slots[i] = slot;

            if (i < inventoryManager.items.Count && inventoryManager.items[i] != null)
            {
                slot.SetItem(inventoryManager.items[i]);
            }
            else
            {
                slot.SetItem(null);
            }
        }
    }

    public void UpdateSlot(int index, ItemData itemData)
    {
        if (index < 0 || index >= slots.Length) return;

        slots[index].SetItem(itemData);
    }


    public void ShowTooltip(ItemData item, RectTransform slotRect)
    {

        toolTip_Name.GetComponent<Text>().text = item.itemName;
        toolTip_Text.GetComponent<Text>().text = item.itemText;

        if (item.hpBonus > 0)
        {
            toolTip_Hp.SetActive(true);
            toolTip_Hp.GetComponent<Text>().text = $"최대체력 + {item.hpBonus}";
        }
        else
        {
            toolTip_Hp.SetActive(false);
        }

        if (item.attackBonus > 0)
        {
            toolTip_Atk.SetActive(true);
            toolTip_Atk.GetComponent<Text>().text = $"공격력 + {item.attackBonus}";
        }
        else
        {
            toolTip_Atk.SetActive(false);
        }

        if (item.defenseBonus > 0)
        {
            toolTip_Df.SetActive(true);
            toolTip_Df.GetComponent<Text>().text = $"방어력 + {item.defenseBonus}";
        }
        else
        {
            toolTip_Df.SetActive(false);
        }
        ItemSlot currentSlot = slotRect.GetComponent<ItemSlot>();

        if (currentSlot.IsMark())
        {
            toolTip_Eq.SetActive(true);
        }
        else
        {
            toolTip_Eq.SetActive(false);
        }

        toolTipRect.position = slotRect.position + (Vector3)tooltipOffset;
        toolTip.SetActive(true);
    }


    public void hideTooltip()
    {
        toolTip.SetActive(false);
    }

    public void ShowNpcMenu()
    {
        npcMenuUI.SetActive(true);
        playerMenuUI.SetActive(false);
        statusUI.SetActive(false);
        equipUI.SetActive(false);
        backButton.SetActive(false);
        click.SetActive(false);
    }

    public void ShowPlayerMenu()
    {
        playerMenuUI.SetActive(true);
        npcMenuUI.SetActive(false);
        statusUI.SetActive(false);
        equipUI.SetActive(false);
        backButton.SetActive(false);
        click.SetActive(false);
    }

    public void ShowGold()
    {
        gold.SetActive(true);
    }

    public void HideGold()
    {
        gold.SetActive(false);
    }

    public void ShowStatusUI()
    {
        Adventurer adv = GameManager.Instance.pickAdventurer;

        playerMenuUI.SetActive(false);
        statusUI.SetActive(true);
        equipUI.SetActive(false);
        backButton.SetActive(true);

        _NAME.text = adv.name;
        _LV.text = $"레벨 {adv.level}";
        _HP.text = $"체력 {adv.currentHp}/{adv.maxHp}";
        _ATK.text = $"공격력 {adv.attack}";
        _DF.text = $"방어력 {adv.defense}";
    }

    public void ShowEquipUI()
    {
        playerMenuUI.SetActive(false);
        statusUI.SetActive(false);
        equipUI.SetActive(true);
        backButton.SetActive(true);
    }

    public void BackToMenu()
    {
        ShowPlayerMenu();
    }

    public void BackToNpcMenu()
    {
        ShowNpcMenu();
    }

    public void CloseAllUI()
    {
        click.SetActive(true);
        uIImageEvent.ResetCamera();

        npcMenuUI.SetActive(false);
        playerMenuUI.SetActive(false);
        statusUI.SetActive(false);
        equipUI.SetActive(false);
        backButton.SetActive(false);
    }
}
