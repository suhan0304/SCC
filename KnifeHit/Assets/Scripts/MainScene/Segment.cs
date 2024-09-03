using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

public class Segment : MonoBehaviour
{
    [TabGroup("Destruction Effect","Segements")] public float forceMagnitude = 10f; 
    [TabGroup("Destruction Effect","Segements")] public float upwardForceMultiplier = 1f;
    [TabGroup("Destruction Effect","Segements")] public float destroyDuration = 0.5f; 
    [TabGroup("Destruction Effect","Segements")] public float delayBeforeFade = 0.5f; 
    [TabGroup("Destruction Effect","Segements")] public float torqueMagnitude = 200f;
    [TabGroup("Destruction Effect","Segements")] public float segmentGravityScale = 2f;
    
    Tween fadeAnimationTween;
    
    public void ApplyForceToSegments() {
        Rigidbody2D rb = transform.GetComponent<Rigidbody2D>();
        
        float RandomUpwardForceMultiplier = Random.Range(upwardForceMultiplier, upwardForceMultiplier + 3);

        Vector3 direction = Random.insideUnitCircle.normalized;
        direction += (Vector3.up * RandomUpwardForceMultiplier).normalized;

        Vector3 force = direction * forceMagnitude;

        rb.gravityScale = segmentGravityScale;

        rb.bodyType = RigidbodyType2D.Dynamic;

        rb.AddForce(force, ForceMode2D.Impulse);
        
        float randomTorque = (Random.Range(0, 2) * 2 - 1) * Random.Range(torqueMagnitude, torqueMagnitude * 1.5f);
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
