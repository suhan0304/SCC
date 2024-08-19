using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    void Update() {
        transform.position += Vector3.up * 0.5f;
    }
}
