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
                    Debug.Log("상태:인트로");
                    break;

                case GameState.Title:
                    Debug.Log("상태:타이틀");
                    GameManager.Instance.mapManager.SwitchTitle(1);
                    GameManager.Instance.ChangeState(GameState.GamePlay);
                    break;

                case GameState.GamePlay:
                    Debug.Log("상태:플레이");
                    break;

                default:
                    Debug.Log("상태:???");
                    break;
            }
        }
    }
}