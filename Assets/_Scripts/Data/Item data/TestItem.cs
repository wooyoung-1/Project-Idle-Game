using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestItem : MonoBehaviour
{
    [SerializeField] private InventoryManager inventoryManager;
    [SerializeField] private ItemData testItem;
    [SerializeField] private ItemData testItem2;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            inventoryManager.AddItem(testItem);
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            inventoryManager.AddItem(testItem2);
        }
    }
}
