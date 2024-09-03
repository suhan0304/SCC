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
    [TabGroup("Tap","Rotate")] public Tween rotateTween;
    [TabGroup("Tap","Rotate")] public Sequence bossSequence;
    [TabGroup("Tap","Rotate")] public Coroutine rotateCoroutine;

    [TabGroup("Tap","Settings",SdfIconType.CodeSlash, TextColor="Cyan")]
    [TabGroup("Tap","Settings")] public int knivesToDestroy = 2;

    [TabGroup("Tap","OnHit Effect",SdfIconType.CodeSlash, TextColor="Red")]

    [TabGroup("OnHit Effect","Shake",SdfIconType.CodeSlash, TextColor="Yellow")]
    [TabGroup("OnHit Effect","Shake")] public float shakeDuration = 0.2f;
    [TabGroup("OnHit Effect","Shake")] public float shakeStrength = 0.125f;
    [TabGroup("OnHit Effect","Shake")] public int vibrato = 10;
    [TabGroup("OnHit Effect","Shake")] public float randomness = 90f; 

    [TabGroup("OnHit Effect","Flash",SdfIconType.CodeSlash, TextColor="Yellow")]
    [TabGroup("OnHit Effect","Flash")] public float flashDuration = 0.05f; 
    [TabGroup("OnHit Effect","Flash")] public SpriteRenderer FlashWhiteRenderer;

    [TabGroup("Destruction Effect","Segements",SdfIconType.CodeSlash, TextColor="Purple")]
    [TabGroup("Destruction Effect","Segements")] public List<GameObject> Segments;    

    [TabGroup("Animation","StartAnimation")] public float targetScale = 0.6f;
    [TabGroup("Animation","StartAnimation")] public float animationDuration = 0.35f;
    [TabGroup("Animation","StartAnimation")] public float startScale = 0.001f;

    private void Awake() {
        FlashWhiteRenderer = transform.Find("FlashWhite").GetComponent<SpriteRenderer>();
        foreach(Transform segment in transform.Find("Segments").transform) {
            Segments.Add(segment.gameObject);
        }
    }

    protected virtual void OnEnable() {
        Events.OnAllKnivesOnHit += DestroyTarget;
        Events.OnHitTarget += OnHitTarget;
        Events.OnGameOver += OnGameOver;
    }

    protected virtual void OnDisable() {
        Events.OnAllKnivesOnHit -= DestroyTarget;
        Events.OnHitTarget -= OnHitTarget;
        Events.OnGameOver -= OnGameOver;
    }
    protected virtual void Start() {
        gameObject.transform.position = GameManager.Instance.targetSpawnPosition;

        rotateCoroutine = StartCoroutine(RotateTargetObject());

        StartTargetAnimation();
    }


    protected void StartTargetAnimation() {
        transform.localScale = Vector3.one * startScale;
        
        DOTween.Sequence()
            .Append(transform.DOScale(targetScale, animationDuration)
                .SetEase(Ease.OutBack));
    }

    IEnumerator RotateTarget() {
        float randomZRotation = Random.Range(minRotation, maxRotation);
        float rotationDirection = Random.Range(0, 2) * 2 - 1;
        float targetRotation = transform.eulerAngles.z + rotationDirection * randomZRotation;
        float calculatedDurtaion = Mathf.Clamp(Mathf.Abs(rotationDirection * randomZRotation) / maxRotationSpeed, minDuration, maxDuration);

        rotateTween?.Kill();

        rotateTween = transform.DORotate(new Vector3(0f, 0f, targetRotation), calculatedDurtaion, RotateMode.FastBeyond360)
            .SetEase(Ease.InOutSine);
        
        yield return new WaitForSeconds(calculatedDurtaion);
    }

    IEnumerator RotateTargetObject() {
        while (true) {
            float waitTime = Random.Range(minRotateDelay, maxRotateDelay);
            yield return new WaitForSeconds(waitTime);

            yield return StartCoroutine(RotateTarget());
        }
    }

    [Button("OnHitTarget")]
    public void OnHitTarget() {
        Debug.Log($"[Target.cs] OnHitTarget : Remain Knives = {GameManager.Instance.RemainKnives}");
        transform.DOPunchPosition(Vector3.right * shakeStrength, shakeDuration, vibrato, randomness);
        FlashWhiteRenderer.DOFade(1f, flashDuration / 2)
            .OnComplete(() => FlashWhiteRenderer.DOFade(0f, flashDuration /2));

        if (GameManager.Instance.RemainKnives == 0) {
            Debug.Log("[Target.cs] OnHitTarget - OnAllKnivesOnHit Invoke");
            Events.OnAllKnivesOnHit.Invoke();
        }

        UIManager.Instance.UpdateScore();
    }

    [Button("DestroyTarget")]
    public void DestroyTarget() {
        if(rotateCoroutine != null)
            StopCoroutine(rotateCoroutine);

        rotateTween?.Kill();
        bossSequence?.Kill();

        foreach(GameObject segment in Segments) {
            segment.GetComponent<Segment>().ApplyForceToSegments();
        }

        DOTween.Sequence()
            .AppendInterval(1f)
            .OnComplete(() => {
                DOTween.Kill(gameObject);
                Events.OnFinishStage?.Invoke();
                Destroy(gameObject);
            });
    }

    public void OnGameOver() {
        if (rotateCoroutine != null)
            StopCoroutine(rotateCoroutine);

        rotateTween?.Kill();
        bossSequence?.Kill();

        DOTween.Sequence()
            .AppendInterval(1f)
            .OnComplete(() => {
            Debug.Log("[Target.cs] OnGameOver : DOTween.Kill");
                DOTween.Kill(gameObject);
                Destroy(gameObject);
            });
    }
}
