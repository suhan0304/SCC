using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject square;
    public Text timeText;

    float time = 0f;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("MakeSquare", 0.0f, 1.0f);
    }

    void Update() {
        time += Time.deltaTime;
        timeText.text = time.ToString("N2");
    }

    void MakeSquare() {
        Instantiate(square);
    }
}
