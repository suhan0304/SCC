using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;

public class Cat : MonoBehaviour
{
    float Speed = 0.25f;

    float full = 5.0f;
    float energy = 0.0f;

    public Transform front;
    public GameObject hungryCat;
    public GameObject fullCat;

    bool isFull = false;
    public int type;

    void Start()
    {
        float x = Random.Range(-9.0f, 9.0f);
        float y = 30.0f;
        transform.position = new Vector2(x, y);

        switch(type) {
            case 1 :
                Speed = 0.05f;
                full = 5f;
                break;
            case 2 :
                Speed = 0.02f;
                full = 10f;
                break;
            case 3 :
                Speed = 0.1f;
                full = 5f;
                break;
        }
    }

    void FixedUpdate()
    {
        if (energy < full) {
            transform.position += Vector3.down * Speed;

            if (transform.position.y < -16.0f) {
                GameManager.Instance.GameOver();
            }
        }
        else {
            if(transform.position.magnitude > 0) {
                transform.position += Vector3.right * 0.05f;
            }
            else
            {
                transform.position += Vector3.left * 0.05f;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Food")) {
            if (energy < full) {
                energy += 1.0f;
                Destroy(collision.gameObject);
                front.localScale = new Vector3(energy / full, 1.0f, 1.0f);
                
                if (energy == full) {
                    if(!isFull) {
                        isFull = true;
                        hungryCat.SetActive(false);
                        fullCat.SetActive(true);
                        Destroy(gameObject, 3.0f);
                        GameManager.Instance.AddScore();
                    }
                }
            }
        }
    }
}
