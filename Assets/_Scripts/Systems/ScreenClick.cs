using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ScreenClick : MonoBehaviour
{
    public void OnLeftClick(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            switch (GameManager.Instance.CurrentState)
            {
                case GameState.Intro:
                    Debug.Log("����:��Ʈ��");
                    break;

                case GameState.Title:
                    Debug.Log("����:Ÿ��Ʋ");
                    GameManager.Instance.mapManager.SwitchTitle(1);
                    GameManager.Instance.ChangeState(GameState.GamePlay);
                    break;

                case GameState.GamePlay:
                    Debug.Log("����:�÷���");
                    break;

                default:
                    Debug.Log("����:???");
                    break;
            }
        }
    }
}