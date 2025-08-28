using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeIn : MonoBehaviour
{
    public float fadeDuration = 2.0f;

    private Image targetImage;

    void Awake()
    {
        targetImage = GetComponent<Image>();
    }

    void Start()
    {
        StartCoroutine(FadeInRoutine());
    }

    private IEnumerator FadeInRoutine()
    {
        Color startColor = new Color(0, 0, 0, 0);
        Color endColor = Color.white;
        float time = 0f;

        targetImage.color = startColor;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            float progress = time / fadeDuration;
            targetImage.color = Color.Lerp(startColor, endColor, progress);
            yield return null;
        }

        targetImage.color = endColor;
    }
}
