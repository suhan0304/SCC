using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

public class Target : MonoBehaviour
{
    [TabGroup("Tap","Rotate",SdfIconType.CodeSlash, TextColor="Green")]
    [TabGroup("Tap","OnHit Effect",SdfIconType.CodeSlash, TextColor="Red")]
    [TabGroup("Tap","Position",SdfIconType.CodeSlash, TextColor="Blue")]
    
    [TabGroup("Tap","Rotate")] public float minRotation = 90f;
    [TabGroup("Tap","Rotate")] public float maxRotation = 500f;
    [TabGroup("Tap","Rotate")] public float minDuration = 1f;
    [TabGroup("Tap","Rotate")]  public float maxDuration = 8f;
    [TabGroup("Tap","Rotate")] public float maxRotationSpeed = 180f;
    [TabGroup("Tap","Rotate")] public float minRotateDelay = 0.5f;
    [TabGroup("Tap","Rotate")] public float maxRotateDelay = 1f;
    [TabGroup("Tap","Position")] public float startPositionY = 0.5f;

    [TabGroup("OnHit Effect","Shake",SdfIconType.CodeSlash, TextColor="Yellow")]
    [TabGroup("OnHit Effect","Flash",SdfIconType.CodeSlash, TextColor="Yellow")]

    [TabGroup("OnHit Effect","Shake")] public float shakeDuration = 0.2f;
    [TabGroup("OnHit Effect","Shake")] public float shakeStrength = 0.125f;
    [TabGroup("OnHit Effect","Shake")] public int vibrato = 10;
    [TabGroup("OnHit Effect","Shake")] public float randomness = 90f; 

    [TabGroup("OnHit Effect","Flash")] public float flashDuration = 0.05f; 
    [TabGroup("OnHit Effect","Flash")] public SpriteRenderer FlashWhiteRenderer;

    public readonly int knivesToDestroy = 10;

    private void OnEnable() {
        Events.OnAllKnivesOnHit += DestroyTarget;
    }

    private void OnDisable() {
        Events.OnAllKnivesOnHit -= DestroyTarget;
    }


    private void Start() {

        transform.position = new Vector3(0f, startPositionY, 0f);
        StartCoroutine(RotateTargetObject());
    }

    IEnumerator RotateTarget() {
        float randomZRotation = Random.Range(minRotation, maxRotation);
        float rotationDirection = Random.Range(0, 2) * 2 - 1;
        float targetRotation = transform.eulerAngles.z + rotationDirection * randomZRotation;
        float calculatedDurtaion = Mathf.Clamp(Mathf.Abs(rotationDirection * randomZRotation) / maxRotationSpeed, minDuration, maxDuration);

        transform.DORotate(new Vector3(0f, 0f, targetRotation), calculatedDurtaion, RotateMode.FastBeyond360)
            .SetEase(Ease.InOutSine);
        
        yield return new WaitForSeconds(calculatedDurtaion);;
    }

    IEnumerator RotateTargetObject() {
        while (true) {
            float waitTime = Random.Range(minRotateDelay, maxRotateDelay);
            yield return new WaitForSeconds(waitTime);

            yield return StartCoroutine(RotateTarget());
        }
    }

    [Button("OnHit")]
    public void OnHit() {
        transform.DOPunchPosition(Vector3.right * shakeStrength, shakeDuration, vibrato, randomness);
        FlashWhiteRenderer.DOFade(1f, flashDuration / 2)
            .OnComplete(() => FlashWhiteRenderer.DOFade(0f, flashDuration /2));

        if (GameManager.Instance.RemainKnives == 0) {
            Events.OnAllKnivesOnHit.Invoke();
        }
    }

    [Button("DestroyTarget")]
    public void DestroyTarget() {
        transform.DOKill();
        StartCoroutine(DestroyTargetCoroutine());
    }

    IEnumerator DestroyTargetCoroutine() {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
