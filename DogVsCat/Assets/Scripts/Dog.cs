using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dog : MonoBehaviour
{
    public GameObject food;

    void Start() {
        InvokeRepeating("MakeFood", 0.0f, 0.2f);
    }

    void Update() {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector2(mousePos.x, transform.position.y);
    }

    void MakeFood() {
        float x = transform.position.x;
        float y = transform.position.y;
        Instantiate(food, new Vector2(x, y), Quaternion.identity);
    }
}
