using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class Knife : MonoBehaviour
{
    public Rigidbody2D rb;

    public ParticleSystem particle;
    [TabGroup("Variables")] public bool canMove = true;
    [TabGroup("Variables")] public float speed = 30f;
    [TabGroup("Variables")] public float knifeOffsetY = 0.7f;

    private void OnEnable() {
        Events.OnTouchScreen += FireKnife;    
    }
    private void OnDisable() {
        Events.OnTouchScreen -= FireKnife;    
    }

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    [Button("Fire")]
    public void FireKnife() {
        rb.velocity = Vector3.up * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Target") && canMove) {
            canMove = false;
            Events.OnTouchScreen -= FireKnife;   

            rb.velocity = Vector3.zero;
            transform.position = new Vector3(0f, collision.gameObject.transform.position.y - knifeOffsetY, 0f);
            transform.SetParent(collision.transform, true);

            particle.Play();
            collision.gameObject.GetComponent<Target>().OnHit();
            
            GameManager.Instance.SpawnKnife();
        }
        else if(collision.gameObject.CompareTag("Knife") && canMove) {
            canMove = false;
            Events.OnTouchScreen -= FireKnife;   

            Events.OnCollisionBetweenKnives.Invoke();
            rb.velocity = Vector3.zero;

            rb.isKinematic = false;

            Vector2 collisionDirection = (transform.position - collision.transform.position).normalized;
            float bounceForce = Random.Range(5f, 7.5f);

            float randomAngle = Random.Range(-60f, 60f);
            Vector2 forceDirection = Quaternion.Euler(0, 0, randomAngle) * collisionDirection;

            rb.AddForce(forceDirection * bounceForce, ForceMode2D.Impulse);

            float randomTorque = Random.Range(-200f, 200f);
            rb.angularVelocity = randomTorque;
        }
    }
}
