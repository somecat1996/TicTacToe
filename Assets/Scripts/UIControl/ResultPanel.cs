using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResultPanel : MonoBehaviour
{
    public Image panel;
    public TMP_Text info;

    private float fadeTime;
    private float fadeTimer;
    private Coroutine coroutine;

    /// <summary>
    /// Set panel text and show panel, if showTime > 0, panel will have fade out effect
    /// </summary>
    /// <param name="text"></param>
    /// <param name="showTime"> fade out time</param>
    public void SetInfo(string text, float showTime)
    {
        if (coroutine is not null && fadeTimer > 0)
            StopCoroutine(coroutine);
        panel.color = new Color(panel.color.r, panel.color.g, panel.color.b, 0.6f);
        info.color = new Color(info.color.r, info.color.g, info.color.b, 0.6f);
        info.text = text;
        if (showTime > 0)
        {
            fadeTime = showTime;
            fadeTimer = showTime;
            coroutine = StartCoroutine("FadeOut");
        }
    }

    /// <summary>
    /// Fade out panel and text
    /// </summary>
    /// <returns></returns>
    private IEnumerator FadeOut()
    {
        while (fadeTimer > 0)
        {
            fadeTimer -= Time.fixedDeltaTime;
            panel.color = new Color(panel.color.r, panel.color.g, panel.color.b, fadeTimer / fadeTime);
            info.color = new Color(info.color.r, info.color.g, info.color.b, fadeTimer / fadeTime);
            yield return new WaitForFixedUpdate();
        }
        fadeTimer = 0;
        gameObject.SetActive(false);
    }
}
