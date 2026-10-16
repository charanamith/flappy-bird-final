using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DeathFlash : MonoBehaviour
{
    public Image flashImage;
    public float flashDuration = 0.15f;

    public void Flash()
    {
        StopAllCoroutines();
        StartCoroutine(FlashEffect());
    }

    IEnumerator FlashEffect()
    {
        Color color = flashImage.color;
        color.a = 0.8f;
        flashImage.color = color;

        yield return new WaitForSecondsRealtime(flashDuration);

        color.a = 0f;
        flashImage.color = color;
    }
}