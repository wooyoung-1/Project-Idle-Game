using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class InventoryManager : MonoBehaviour
{
    public int inventorySize = 16;
    public List<ItemData> items = new List<ItemData>();

    public bool AddItem(ItemData newItem)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i] == null)
            {
                items[i] = newItem;
                GameManager.Instance.uiManager.UpdateSlot(i, newItem);
                return true;
            }
        }

        if (items.Count < inventorySize)
        {
            items.Add(newItem);
            GameManager.Instance.uiManager.UpdateSlot(items.Count - 1, newItem);
            Debug.Log($"{newItem.itemName} 추가");
            return true;
        }

        Debug.Log("인벤토리 가득참");
        return false;
    }


    public void RemoveItem(ItemData item)
    {
        int index = items.IndexOf(item);
        if (index >= 0) 
            items[index] = null;
    }
}