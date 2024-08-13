using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject rain;

    int totalScore = 0;

    void Awake() {
        if(Instance == null) {
            Instance = this;    
        }
        else {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        InvokeRepeating("MakeRain", 0, 0.5f);
    }

    void MakeRain() {
        Instantiate(rain, gameObject.transform);
    }

    public void AddScore(int score) {
        totalScore += score;
    }
}
