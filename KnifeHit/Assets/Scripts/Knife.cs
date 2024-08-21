using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEditor.Callbacks;
using UnityEngine;

public class Knife : MonoBehaviour
{
    public float speed = 30f;
    public Rigidbody2D rb;

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
        if (collision.gameObject.CompareTag("Target")) {
            rb.velocity = Vector3.zero;
            transform.position = new Vector3(0f, -0.5f, 0f);
            transform.SetParent(collision.transform, true);

            collision.gameObject.GetComponent<Target>().OnHit();
            
            Events.OnTouchScreen -= FireKnife;    

            GameManager.Instance.SpawnKnife();
        }
        else if(collision.gameObject.CompareTag("Knife")) {
            //TODO - GameOver
        }
    }
}
