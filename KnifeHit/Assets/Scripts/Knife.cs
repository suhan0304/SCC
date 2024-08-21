using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class Knife : MonoBehaviour
{
    public float speed = 30f;

    [Button("Fire")]
    public void FireKnife() {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector3.up * speed;
    }
}
