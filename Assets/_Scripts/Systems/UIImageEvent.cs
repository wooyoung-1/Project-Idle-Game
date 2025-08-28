using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIImageEvent : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private GameObject targetObject;
    [SerializeField] private Camera targetCamera;

    [SerializeField] private Vector3 moveTransform;
    [SerializeField] private int npcType;

    private float moveDuration = 1f;

    private Action setAction;

    private Vector3 cameraTransform;
    private Transform targetTransform;
    private SpriteOutline outline;
    private Coroutine moveCoroutine;

    private Adventurer setAdventurer;

    private void Start()
    {
        outline = targetObject.GetComponent<SpriteOutline>();
        targetTransform = targetObject.transform;
        cameraTransform = targetCamera.transform.position;

        if (npcType == -1)
            setAction = () => GameManager.Instance.uiManager.ShowNpcMenu();

        if (npcType >= 0)
            setAction = () => GameManager.Instance.uiManager.ShowPlayerMenu();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (GameManager.Instance.onEvent)
            return;

        outline.enabled = true;
        Debug.Log("올림");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (GameManager.Instance.onEvent)
            return;

        outline.enabled = false;
        Debug.Log("나감");
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (GameManager.Instance.onEvent)
            return;

        if (npcType >= 0)
        {
            setAdventurer = GameManager.Instance.adventurerManager.GetAllAdventurer()[npcType];
        }

        outline.enabled = true;

        GameManager.Instance.onObject = targetObject;
        GameManager.Instance.pickAdventurer = setAdventurer;

        GameManager.Instance.onEvent = true;
        MoveCamera(moveTransform);
        Debug.Log("클릭됨");
    }

    private void MoveCamera(Vector3 targetPos)
    {
        if (moveCoroutine != null)
            StopCoroutine(moveCoroutine);

        moveCoroutine = StartCoroutine(MoveCameraCoroutine(targetPos, setAction));
    }

    public void ResetCamera()
    {
        if (moveCoroutine != null)
            StopCoroutine(moveCoroutine);

        GameManager.Instance.onObject.GetComponent<SpriteOutline>().enabled = false;

        moveCoroutine = StartCoroutine(MoveCameraCoroutine(cameraTransform, null));
        GameManager.Instance.onEvent = false;
    }
    private IEnumerator MoveCameraCoroutine(Vector3 targetPos, Action action)
    {
        Vector3 startPos = targetCamera.transform.position;
        float a = 0f;

        while (a < moveDuration)
        {
            a += Time.deltaTime;
            float t = Mathf.Clamp01(a / moveDuration);
            targetCamera.transform.position = Vector3.Lerp(startPos, targetPos, t);
            yield return null;
        }

        targetCamera.transform.position = targetPos;
        moveCoroutine = null;

        action?.Invoke();
    }
}
