using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeController : MonoBehaviour
{
    [SerializeField] private Image _fadeImage;

    private void Start ()
    {
        if (_fadeImage != null)
            FadeIn(5f);
    }
    
    public void FadeOut (float duration)
    {
        StartCoroutine(CanvasFade(duration, 0f, 1f));
    }

    public void FadeIn (float duration)
    {
        StartCoroutine(CanvasFade(duration, 1f, 0f));
    }
    
    IEnumerator CanvasFade (float duration, float startAlpha, float endAlpha)
    {
        float startTime = Time.time;
        float endTime = Time.time + duration;
        float elapsedTime = 0f;
        float percentage = 0f;

        
        while (Time.time <= endTime)
        {
            elapsedTime = Time.time - startTime; 			
            percentage = 1 / (duration / elapsedTime);  
            if (startAlpha > endAlpha) 						
            {
                _fadeImage.color = new Color(0f, 0f, 0f, startAlpha - percentage);
            }
            else 											// if we are fading in/up
            {
                _fadeImage.color = new Color(0f, 0f, 0f, startAlpha + percentage);
            }
            yield return new WaitForEndOfFrame();
        }

        _fadeImage.color = new Color(0f, 0f, 0f, endAlpha);
    }
}
