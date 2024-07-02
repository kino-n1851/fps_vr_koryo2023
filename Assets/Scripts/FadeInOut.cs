using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.Events;
public class FadeInOut : MonoBehaviour
{
    [SerializeField]
    private Volume volume;
    // Start is called before the first frame update
    public void ChangeScene(Action ev, float duration)
    {
        Debug.Log("ChangeScene");
        StartCoroutine(FadeOut(ev, duration));
    }

    public void StartScene(float duration)
    {
        StartCoroutine(FadeIn(null, duration));
    }

    private IEnumerator FadeOut(Action ev, float duration)
    {
        float elapsedTime = 0.0f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            volume.weight = Mathf.Lerp(0.0f, 1.0f, Mathf.Clamp01(elapsedTime / duration));
            yield return new WaitForEndOfFrame();
        }
        Debug.Log($"FadeOut, {ev}");
        ev?.Invoke();
    }

    private IEnumerator FadeIn(Action ev, float duration)
    {
        float elapsedTime = 0.0f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            volume.weight = Mathf.Lerp(1.0f, 0.0f, Mathf.Clamp01(elapsedTime / duration));
            yield return new WaitForEndOfFrame();
        }
        ev?.Invoke();
    }
}
