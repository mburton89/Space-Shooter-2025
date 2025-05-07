using UnityEngine;
using System.Collections;
using DG.Tweening;

public class MainMenuSequencer : MonoBehaviour
{
    public Transform leftHorse;
    public Transform rightHorse;
    public Transform title;

    public float distanceToMoveRight;
    public float distanceToMoveLeft;

    public AudioSource introRiff;

    private Vector3 leftHorseStartPos;
    private Vector3 rightHorseStartPos;
    private Vector3 titleStartScale;

    void Start()
    {
        // Store initial positions and scale
        leftHorseStartPos = leftHorse.position;
        rightHorseStartPos = rightHorse.position;
        titleStartScale = title.localScale;

        StartCoroutine(LoopIntroSequence());
    }

    private IEnumerator LoopIntroSequence()
    {
        while (true)
        {
            // Reset positions and scale instantly
            leftHorse.position = leftHorseStartPos;
            rightHorse.position = rightHorseStartPos;
            title.localScale = Vector3.zero;

            introRiff.pitch = Random.Range(0.8f, 1.2f);
            introRiff.Play();

            // Animate sequence
            yield return StartCoroutine(IntroSequence());

            // Wait the remaining time until 10 seconds
            yield return new WaitForSeconds(10f - 0.35f - 0.35f - 0.4f); // Total = ~1.1s animation
        }
    }

    private IEnumerator IntroSequence()
    {
        leftHorse.DOMoveX(leftHorse.position.x + distanceToMoveRight, 0.4f).SetEase(Ease.OutBack);
        yield return new WaitForSeconds(0.35f);

        rightHorse.DOMoveX(rightHorse.position.x + distanceToMoveLeft, 0.4f).SetEase(Ease.OutBack);
        yield return new WaitForSeconds(0.35f);

        title.DOScale(1, 0.4f).SetEase(Ease.OutBack);
        yield return new WaitForSeconds(0.4f); // Ensure the scale animation finishes before restarting
    }
}
