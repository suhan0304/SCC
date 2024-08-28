using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using Unity.VisualScripting;
using UnityEngine;

public class Knife : MonoBehaviour
{
    public Rigidbody2D rb;
    public SpriteRenderer sr;

    public ParticleSystem particle;
    [TabGroup("Variables")] public bool hasInteracted = false;
    [TabGroup("Variables")] public float speed = 30f;
    [TabGroup("Variables")] public float knifeOffsetY = 1.25f;

    [TabGroup("Variables")] public float bounceForce = 5f;
    [TabGroup("Variables")] public float knifeGravityScale = 2f;


     [TabGroup("Animation")] private Tween animationTween;
     [TabGroup("Animation")] public Vector3 animEndPosition;
     [TabGroup("Animation")] public Vector3 animStartPosition;
     [TabGroup("Animation")] public float delayBeforeFade = 0.5f;
     [TabGroup("Animation")] public float startAnimationDuration = 0.25f;
    [TabGroup("Animation")] public float destroyDuration = 0.25f;
    [TabGroup("Animation")]  private Tween fadeAnimationTween;

    

    private void OnEnable() {
        Events.OnTouchScreen += FireKnife;    
        Events.OnAllKnivesOnHit += KnifeDestroy;    
    }
    private void OnDisable() {
        Events.OnTouchScreen -= FireKnife;  
        Events.OnAllKnivesOnHit -= KnifeDestroy;    
    }

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        rb.gravityScale = knifeGravityScale;

        StartKnifeAnimation();
    }

    private void StartKnifeAnimation() {
        animEndPosition = transform.position;
        animStartPosition = transform.position - new Vector3(0, 1f, 0);

        sr.color = new Color(1, 1, 1, 0); 
        transform.position = animStartPosition;

        animationTween = DOTween.Sequence()
            .Append(transform.DOMove(animEndPosition, startAnimationDuration))
            .Join(sr.DOFade(1, startAnimationDuration))
            .OnKill(() => {EndKnifeAnimation();
            });
    }

    private void EndKnifeAnimation() {
        sr.color = new Color(1, 1, 1, 1);
        transform.position = animEndPosition;
    }

    [Button("Fire")]
    public void FireKnife() {
        animationTween?.Kill();
        EndKnifeAnimation();
        rb.velocity = Vector3.up * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        gameObject.GetComponent<TrailRenderer>().enabled = false;
        Events.OnTouchScreen -= FireKnife;   

        if (collision.gameObject.CompareTag("Target") && !hasInteracted) {
            hasInteracted = true;

            rb.velocity = Vector3.zero;
            transform.position = new Vector3(0f, collision.gameObject.transform.position.y - knifeOffsetY, 0f);
            transform.SetParent(collision.transform, true);

            particle.Play();
            
            GameManager.Instance.scoreNum++;

            Events.OnHitTarget?.Invoke();
            
            if ( GameManager.Instance.RemainKnives > 0) {
                GameManager.Instance.SpawnKnife();
            }
        }
        else if(collision.gameObject.CompareTag("Knife") && !hasInteracted) {
            hasInteracted = true;
            
            rb.bodyType = RigidbodyType2D.Dynamic;

            Events.OnCollisionBetweenKnives?.Invoke();
            rb.velocity = Vector3.zero;

            Vector2 collisionDirection = (transform.position - collision.transform.position).normalized;
            float randBounceForce = Random.Range(bounceForce, bounceForce * 1.5f);

            float randomAngle = Random.Range(-60f, 60f);
            Vector2 forceDirection = Quaternion.Euler(0, 0, randomAngle) * collisionDirection;

            rb.AddForce(forceDirection * randBounceForce, ForceMode2D.Impulse);

            float randomTorque = Random.Range(-200f, 200f);
            rb.angularVelocity = randomTorque;

            Vector2 collisionPoint = collision.bounds.ClosestPoint(transform.position);
            SFXManager.Instance.playImpact(collisionPoint);
            
            Events.OnGameOver?.Invoke();

            KnifeDestroy();
        }
    }

    public void KnifeDestroy() {
        hasInteracted = true;
        rb.bodyType = RigidbodyType2D.Dynamic;

        float randomTorque = Random.Range(200f, 300f);
        rb.angularVelocity = (Random.Range(0, 2) * 2 - 1) *randomTorque;
        
        float randBounceForce = Random.Range(bounceForce, bounceForce * 1.2f);
        rb.AddForce(transform.up.normalized * randBounceForce, ForceMode2D.Impulse);


        DOTween.Sequence()
            .AppendInterval(delayBeforeFade)
            .Append(sr.DOFade(0, destroyDuration)
                        .OnComplete(() => {
                            DOTween.Kill(gameObject);
                            Destroy(gameObject);
                        }));
    }
}
