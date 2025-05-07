using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class MainMenuSequencer : MonoBehaviour
{
    //Branches to merge
    //Dash
    //Mega Laser
    //Nathan New Enemy Train
    //Make sure powerups drop
    //Character select
    
    public Transform leftHorse;
    public Transform rightHorse;
    public Transform title;

    public float distanceToMoveRight;
    public float distanceToMoveLeft;

    public AudioSource introRiff;

    void Start()
    {
        introRiff.pitch = Random.Range(0.8f, 1.2f);

        StartCoroutine(IntroSequence());
    }

    private IEnumerator IntroSequence()
    {
        leftHorse.DOMoveX(leftHorse.position.x + distanceToMoveRight, 0.4f).SetEase(Ease.OutBack);
        yield return new WaitForSeconds(.35f);

        rightHorse.DOMoveX(rightHorse.position.x + distanceToMoveLeft, 0.4f).SetEase(Ease.OutBack);
        yield return new WaitForSeconds(.35f);

        title.DOScale(1, .4f).SetEase(Ease.OutBack);
    }
}
