using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ZRotateBackAndForth : MonoBehaviour
{
    public Vector3 endRotation;
    public float secondsToRotate;

    Coroutine coroutine;
    Tween tween;

    public void Init()
    {
        coroutine = StartCoroutine(RotateCo());
    }

    private void OnEnable()
    {
        coroutine = StartCoroutine(RotateCo());
    }

    private void OnDisable()
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
            coroutine = null;
        }

        if (tween != null)
        {
            tween.Kill();
        }
    }

    private IEnumerator RotateCo()
    {
        if (transform != null)
        {
            tween = transform.DORotate(endRotation, secondsToRotate, RotateMode.Fast).SetEase(Ease.InOutQuad);
        }
        yield return new WaitForSeconds(secondsToRotate);
        if (transform != null)
        {
            tween = transform.DORotate(-endRotation, secondsToRotate, RotateMode.Fast).SetEase(Ease.InOutQuad);
        }
        yield return new WaitForSeconds(secondsToRotate);
        coroutine = StartCoroutine(RotateCo());
    }

    //private void OnDestroy()
    //{
    //    if (coroutine != null)
    //    {
    //        StopCoroutine(coroutine);
    //    }

    //    if (tween != null)
    //    {
    //        tween.Kill();
    //    }
    //}
}