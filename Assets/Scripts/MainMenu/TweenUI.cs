using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TweenUI : MonoBehaviour
{
    public LeanTweenType easeType;
    public float delay;
    public float duration;
    public UnityEvent onCompleteCallback;
    public AnimationCurve curve;

    public void OnEnable()
    {
        // Scales the object up from nothing
        transform.localScale = new Vector3(0, 0, 0);

        if (easeType == LeanTweenType.animationCurve)
        {
            LeanTween.scale(gameObject, new Vector3(1, 1, 1), duration).setDelay(delay).setEase(curve);
        }
        else
        {
            LeanTween.scale(gameObject, new Vector3(1, 1, 1), duration).setDelay(delay).setEase(easeType);
        }
    }

    // Scales the UI down to zero
    public void CloseElement(UnityEvent onCompleted)
    {
        onCompleteCallback = onCompleted;
        LeanTween.scale(gameObject, new Vector3(0, 0, 0), duration).setDelay(delay).setEase(easeType).setOnComplete(OnComplete);
    }

    public void OnComplete()
    {
        if(onCompleteCallback != null)
        {
            onCompleteCallback.Invoke();
        }
    }
}
