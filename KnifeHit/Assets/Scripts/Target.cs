using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

public class Target : MonoBehaviour
{
    [TabGroup("Tap","Rotate",SdfIconType.CodeSlash, TextColor="Green")]
    
    [TabGroup("Tap","Rotate")] public float minRotation = 90f;
    [TabGroup("Tap","Rotate")] public float maxRotation = 500f;
    [TabGroup("Tap","Rotate")] public float minDuration = 1f;
    [TabGroup("Tap","Rotate")]  public float maxDuration = 8f;
    [TabGroup("Tap","Rotate")] public float maxRotationSpeed = 180f;
    [TabGroup("Tap","Rotate")] public float minRotateDelay = 0.5f;
    [TabGroup("Tap","Rotate")] public float maxRotateDelay = 1f;
    [TabGroup("Tap","Position")] public float startPositionY = 0.5f;
    
    
    [TabGroup("Tap","OnHit Effect/Shake")] public float shakeDuration = 0.2f;
    [TabGroup("Tap","OnHit Effect/Shake")] public float shakeStrength = 0.125f;
    [TabGroup("Tap","OnHit Effect/Shake")] public int vibrato = 10;
    [TabGroup("Tap","OnHit Effect/Shake")] public float randomness = 90f; 

    [TabGroup("Tap","OnHit Effect/Flash")]public float flashDuration = 0.1f; 
    [TabGroup("Tap","OnHit Effect/Flash")]public Material paintWhiteMaterial; 
    [TabGroup("Tap","OnHit Effect/Flash")]private Material originalMaterial; 
    [TabGroup("Tap","OnHit Effect/Flash")]private SpriteRenderer spriteRenderer; 
    
    private void Start() {        
        spriteRenderer = GetComponent<SpriteRenderer>(); 
        originalMaterial = spriteRenderer.material; 

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
        float randomShakeStrength = Random.Range(0, shakeStrength);

        if (randomShakeStrength >= 0.75f) Shake(randomShakeStrength);
        Flash();
    }

    private void Shake(float shakeStrength) {
        transform.DOPunchPosition(Vector3.right * shakeStrength, shakeDuration, vibrato, randomness);
    }

    // 반짝이게 하는 메서드
    private void Flash()
    {
        spriteRenderer.material = paintWhiteMaterial;

        // 반짝이는 애니메이션
        DOTween.To(() => paintWhiteMaterial.color, x => paintWhiteMaterial.color = x, Color.white, flashDuration / 2)
               .OnComplete(() => DOTween.To(() => paintWhiteMaterial.color, x => paintWhiteMaterial.color = x, originalMaterial.color, flashDuration / 2))
               .OnKill(() => spriteRenderer.material = originalMaterial);
    }
}
