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

    // 골드 지정
    public void SetGold(int a)
    {
        gold = a;
        GameManager.Instance.uiManager.UpdateGold(gold);
    }

    // 골드 추가
    public void AddGold(int a)
    {
        gold += a;
        GameManager.Instance.uiManager.UpdateGold(gold);
    }

    // 상점같은데 사용 할 돈이 많으면 true 반환하는 함수?

}

