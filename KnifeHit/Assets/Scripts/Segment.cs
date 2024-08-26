using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

public class Segment : MonoBehaviour
{
    [TabGroup("Destruction Effect","Segements")] public float forceMagnitude = 10f; 
    [TabGroup("Destruction Effect","Segements")] public float upwardForceMultiplier = 2f;
    [TabGroup("Destruction Effect","Segements")] public float destroyDuration = 0.5f; 
    [TabGroup("Destruction Effect","Segements")] public float delayBeforeFade = 0.5f; 
    [TabGroup("Destruction Effect","Segements")] public float torqueMagnitude = 300f;
    [TabGroup("Destruction Effect","Segements")] public float segmentGravityScale = 2f;
    
    Tween fadeAnimationTween;

    public void Start() {
        Debug.Log(transform.position);
    }
    
    public void ApplyForceToSegments() {

        Vector3 parentPosition = transform.position;
        Rigidbody2D rb = transform.GetComponent<Rigidbody2D>();
        
        float RandomUpwardForceMultiplier = Random.Range(upwardForceMultiplier - 2 , upwardForceMultiplier + 2);
        Vector3 direction = ((transform.position - parentPosition).normalized + Vector3.up * RandomUpwardForceMultiplier).normalized;

        float RandomforceMagnitude = Random.Range(forceMagnitude - 2 , forceMagnitude + 2);
        Vector3 force = direction * RandomforceMagnitude;

        rb.gravityScale = segmentGravityScale;
        rb.bodyType = RigidbodyType2D.Dynamic;

        rb.AddForce(force, ForceMode2D.Impulse);
        
        float randomTorque = Random.Range(-torqueMagnitude, torqueMagnitude);
        rb.angularVelocity = randomTorque;

        SpriteRenderer sr = transform.GetComponent<SpriteRenderer>();

        fadeAnimationTween = DOTween.Sequence()
            .AppendInterval(delayBeforeFade)
            .Append(sr.DOFade(0, destroyDuration)
                        .OnComplete(() => {
                            fadeAnimationTween?.Kill();
                            Destroy(gameObject);
                        }));
    }
}
