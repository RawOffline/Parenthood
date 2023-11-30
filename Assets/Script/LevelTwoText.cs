using System.Collections;
using TMPro;
using UnityEngine;

public class LevelTwoText : MonoBehaviour
{
    TextMeshProUGUI text;

    private void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
            yield return new WaitForSeconds(1f);
            yield return StartCoroutine(FadeTextToFullAlpha(4f, text));
            yield return new WaitForSeconds(1f);
            yield return StartCoroutine(FadeTextToZeroAlpha(2f, text));

    }

    public IEnumerator FadeTextToFullAlpha(float t, TextMeshProUGUI i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 0);
        while (i.color.a < 1.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a + (Time.deltaTime / t));
            yield return null;
        }
    }

    public IEnumerator FadeTextToZeroAlpha(float t, TextMeshProUGUI i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 1);
        while (i.color.a > 0.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / t));
            yield return null;
        }
    }
}
