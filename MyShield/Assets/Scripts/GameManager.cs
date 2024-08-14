using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject square;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("MakeSquare", 0.0f, 1.0f);
    }

    void MakeSquare() {
        Instantiate(square);
    }
}
