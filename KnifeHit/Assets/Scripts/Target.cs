using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

public class Target : MonoBehaviour
{
#region OrdinInspector settings
    [TabGroup("Tap","Rotate",SdfIconType.CodeSlash, TextColor="Green")]
    [TabGroup("Tap","OnHit Effect",SdfIconType.CodeSlash, TextColor="Red")]
    [TabGroup("Tap","Position",SdfIconType.CodeSlash, TextColor="Blue")]
    [TabGroup("OnHit Effect","Shake",SdfIconType.CodeSlash, TextColor="Yellow")]
    [TabGroup("OnHit Effect","Flash",SdfIconType.CodeSlash, TextColor="Yellow")]
    [TabGroup("Destruction Effect","Segements",SdfIconType.CodeSlash, TextColor="Purple")]
#endregion
    
#region variables
    [TabGroup("Tap","Rotate")] public float minRotation = 90f;
    [TabGroup("Tap","Rotate")] public float maxRotation = 500f;
    [TabGroup("Tap","Rotate")] public float minDuration = 1f;
    [TabGroup("Tap","Rotate")]  public float maxDuration = 8f;
    [TabGroup("Tap","Rotate")] public float maxRotationSpeed = 180f;
    [TabGroup("Tap","Rotate")] public float minRotateDelay = 0.5f;
    [TabGroup("Tap","Rotate")] public float maxRotateDelay = 1f;
    [TabGroup("Tap","Position")] public float startPositionY = 0.5f;

    [TabGroup("OnHit Effect","Shake")] public float shakeDuration = 0.2f;
    [TabGroup("OnHit Effect","Shake")] public float shakeStrength = 0.125f;
    [TabGroup("OnHit Effect","Shake")] public int vibrato = 10;
    [TabGroup("OnHit Effect","Shake")] public float randomness = 90f; 

    [TabGroup("OnHit Effect","Flash")] public float flashDuration = 0.05f; 
    [TabGroup("OnHit Effect","Flash")] public SpriteRenderer FlashWhiteRenderer;


    [TabGroup("Destruction Effect","Segements")] public GameObject[] Segements;    
    [TabGroup("Destruction Effect","Segements")] public float forceMagnitude = 10f; 
    [TabGroup("Destruction Effect","Segements")] public float upwardForceMultiplier = 2f;
    [TabGroup("Destruction Effect","Segements")] public float destroyDuration = 1f; 
    [TabGroup("Destruction Effect","Segements")] public float torqueMagnitude = 300f;
    [TabGroup("Destruction Effect","Segements")] public float segmentGravityScale = 2f;

    [TabGroup("Animation","StartAnimation")] public float targetScale = 0.6f;
    [TabGroup("Animation","StartAnimation")] public float animationDuration = 0.35f;
    
    private Coroutine rotateCoroutine;
    private Tween rotateTween;
#endregion


    public readonly int knivesToDestroy = 5;

    private void OnEnable() {
        Events.OnAllKnivesOnHit += DestroyTarget;
    }

    private void OnDisable() {
        Events.OnAllKnivesOnHit -= DestroyTarget;
    }


    private void Start() {
        transform.position = new Vector3(0f, startPositionY, 0f);
        rotateCoroutine = StartCoroutine(RotateTargetObject());

        StartTargetAnimation();
    }


    private void StartTargetAnimation() {
        transform.localScale = Vector3.zero;
        
        DOTween.Sequence()
            .Append(transform.DOScale(targetScale, animationDuration)
                .SetEase(Ease.OutBack)) 
            .Play();
    }

    IEnumerator RotateTarget() {
        float randomZRotation = Random.Range(minRotation, maxRotation);
        float rotationDirection = Random.Range(0, 2) * 2 - 1;
        float targetRotation = transform.eulerAngles.z + rotationDirection * randomZRotation;
        float calculatedDurtaion = Mathf.Clamp(Mathf.Abs(rotationDirection * randomZRotation) / maxRotationSpeed, minDuration, maxDuration);

        rotateTween?.Kill();

        rotateTween = transform.DORotate(new Vector3(0f, 0f, targetRotation), calculatedDurtaion, RotateMode.FastBeyond360)
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
        StopCoroutine(rotateCoroutine);

        rotateTween?.Kill();

        ApplyForceToSegments();

    }

    private void ApplyForceToSegments() {
        Vector3 parentPosition = transform.position;

        foreach(GameObject segement in Segements) {
            Rigidbody2D rb = segement.GetComponent<Rigidbody2D>();
            SpriteRenderer sr = segement.GetComponent<SpriteRenderer>();
            
            float RandomUpwardForceMultiplier = Random.Range(upwardForceMultiplier - 2 , upwardForceMultiplier + 2);
            Vector3 direction = ((segement.transform.position - parentPosition).normalized + Vector3.up * RandomUpwardForceMultiplier).normalized;

            float RandomforceMagnitude = Random.Range(forceMagnitude - 2 , forceMagnitude + 2);
            Vector3 force = direction * RandomforceMagnitude;

            rb.gravityScale = segmentGravityScale;
            rb.bodyType = RigidbodyType2D.Dynamic;

            rb.AddForce(force, ForceMode2D.Impulse);
            
            float randomTorque = Random.Range(-torqueMagnitude, torqueMagnitude);
            rb.angularVelocity = randomTorque;

            sr.DOFade(0, destroyDuration)
                .OnComplete(() => destroyTarget());
        }
    }

    private void destroyTarget() {
        Destroy(gameObject);
    }
}
