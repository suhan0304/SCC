using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject rain;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("MakeRain", 0, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void MakeRain() {
        Instantiate(rain, gameObject.transform);
    }
}
