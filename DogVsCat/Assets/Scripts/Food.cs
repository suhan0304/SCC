using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    public float foodSpeed = 0.5f;
    void FixedUpdate() {
        transform.position += Vector3.up * foodSpeed;

        if (transform.position.y > 26.0f) {
            Destroy(gameObject);
        }
    }
}
