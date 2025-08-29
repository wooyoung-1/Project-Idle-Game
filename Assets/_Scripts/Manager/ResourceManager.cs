using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    private int gold = 0;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            AddGold(100);
        }
    }
    public int GetGold()
    {
        return gold;
    }

    public void SetGold(int a)
    {
        gold = a;
        GameManager.Instance.uiManager.UpdateGold(gold);
    }

    // °ñµå Ãß°¡
    public void AddGold(int a)
    {
        gold += a;
        GameManager.Instance.uiManager.UpdateGold(gold);
    }
}

