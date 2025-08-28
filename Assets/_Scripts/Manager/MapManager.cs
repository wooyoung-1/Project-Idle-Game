using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> mapObjects;
    [SerializeField] private List<Material> materials;
    [SerializeField] private Image targetRenderer;

    [SerializeField] private GameObject mainFrame;
    [SerializeField] private GameObject setFrame;

    [SerializeField] private float fadeDuration = 1f;

    public void Switch(int index)
    {
        StartCoroutine(SwitchFadeIn(index));

        Debug.Log("¸ÊÃ¼Å©:" + index);
    }

    public void SwitchTitle(int index)
    {
        StartCoroutine(SwitchFadeTitle(index));

        Debug.Log("¸ÊÃ¼Å©:" + index);
    }

    IEnumerator SwitchFadeTitle(int index)
    {
        yield return StartCoroutine(Fade(1, 0));

        if (GameManager.Instance.mapIndex >= 0 && GameManager.Instance.mapIndex < mapObjects.Count)
            mapObjects[GameManager.Instance.mapIndex].SetActive(false);

        mapObjects[index].SetActive(true);
        GameManager.Instance.mapIndex = index;

        targetRenderer.material = materials[index];

        yield return new WaitForSeconds(1f);

        //setFrame.SetActive(true);
        RectTransform rectTransform = mainFrame.GetComponent<RectTransform>();

        Vector2 currentPos = rectTransform.anchoredPosition;
        rectTransform.anchoredPosition = new Vector2(currentPos.x, 150f);
        rectTransform.localScale = new Vector3(0.515f, 0.52f, 0.52f);
        GameManager.Instance.uiManager.ShowGold();

        yield return StartCoroutine(Fade(0, 1));
    }

    IEnumerator SwitchFadeIn(int index)
    {
        yield return StartCoroutine(Fade(0, 1));

        if (GameManager.Instance.mapIndex >= 0 && GameManager.Instance.mapIndex < mapObjects.Count)
            mapObjects[GameManager.Instance.mapIndex].SetActive(false);

        mapObjects[index].SetActive(true);
        GameManager.Instance.mapIndex = index;

        targetRenderer.material = materials[index];

        //yield return StartCoroutine(Fade(1, 0));
    }

    IEnumerator SwitchFadeOut(int index)
    {
        yield return StartCoroutine(Fade(1, 0));

        if (GameManager.Instance.mapIndex >= 0 && GameManager.Instance.mapIndex < mapObjects.Count)
            mapObjects[GameManager.Instance.mapIndex].SetActive(false);

        mapObjects[index].SetActive(true);
        GameManager.Instance.mapIndex = index;

        targetRenderer.material = materials[index];

        yield return StartCoroutine(Fade(0, 1));
    }

    IEnumerator Fade(float start, float end)
    {
        Color blackColor = Color.black;
        Color whiteColor = Color.white;

        float time = 0f;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            float t = time / fadeDuration;
            float progress = Mathf.Lerp(start, end, t);

            targetRenderer.color = Color.Lerp(blackColor, whiteColor, progress);
            yield return null;
        }

        targetRenderer.color = Color.Lerp(blackColor, whiteColor, end);
    }
}
