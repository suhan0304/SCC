using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : MonoBehaviour
{
    float Speed = 0.5f;

    float full = 5.0f;
    float energy = 0.0f;

    public Transform front;
    public GameObject hungryCat;
    public GameObject fullCat;

    void Start()
    {
        float x = Random.Range(-9.0f, 9.0f);
        float y = 30.0f;
        transform.position = new Vector2(x, y);
    }

    void Update()
    {
        if (energy < full) {
            transform.position += Vector3.down * Speed;
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

    private void OnTriggerEnter(Collider collision) {
        if (collision.gameObject.CompareTag("Food")) {
            if (energy < full) {
                energy += 1.0f;
                front.localScale = new Vector3(energy / full, 1.0f, 1.0f);
                Destroy(collision.gameObject);
            }
            else 
            {
                hungryCat.SetActive(false);
                fullCat.SetActive(true);
            }
        }
    }
}
