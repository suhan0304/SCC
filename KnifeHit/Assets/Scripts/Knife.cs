using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class Knife : MonoBehaviour
{
    public Rigidbody2D rb;

    public ParticleSystem particle;
    [TabGroup("Variables")] public bool hasInteracted = false;
    [TabGroup("Variables")] public float speed = 30f;
    [TabGroup("Variables")] public float knifeOffsetY = 0.7f;

    [TabGroup("Variables")] public float bounceForce = 5f;
    [TabGroup("Variables")] public float delayDestroyTime = 5f;
    [TabGroup("Variables")] public float knifeGravityScale = 1f;

    

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
        rb.gravityScale = knifeGravityScale;
    }

    [Button("Fire")]
    public void FireKnife() {
        rb.velocity = Vector3.up * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Target") && !hasInteracted) {
            hasInteracted = true;
            Events.OnTouchScreen -= FireKnife;   

            rb.velocity = Vector3.zero;
            transform.position = new Vector3(0f, collision.gameObject.transform.position.y - knifeOffsetY, 0f);
            transform.SetParent(collision.transform, true);

            particle.Play();
            collision.gameObject.GetComponent<Target>().OnHit();
            
            if ( GameManager.Instance.RemainKnives > 0) {
                GameManager.Instance.SpawnKnife();
            }
        }
        else if(collision.gameObject.CompareTag("Knife") && !hasInteracted) {
            hasInteracted = true;
            Events.OnTouchScreen -= FireKnife;   
            
            rb.bodyType = RigidbodyType2D.Dynamic;

            Events.OnCollisionBetweenKnives.Invoke();
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
        }
    }

    public void KnifeDestroy() {
        hasInteracted = true;
        StartCoroutine(KnifeDestroyCoroutine());
    }

    IEnumerator KnifeDestroyCoroutine() {
        rb.bodyType = RigidbodyType2D.Dynamic;

        float randBounceForce = Random.Range(bounceForce, bounceForce * 1.5f);
        rb.AddForce(Vector3.up * randBounceForce, ForceMode2D.Impulse);

        float randomTorque = Random.Range(-200f, 200f);
        rb.angularVelocity = randomTorque;

        yield return new WaitForSeconds(delayDestroyTime);

        Destroy(gameObject);
    }
}
