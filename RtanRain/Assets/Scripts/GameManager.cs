using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject rain;
    public GameObject endPanel;

    public Text totalScoreText;
    public Text timeText;

    int totalScore = 0;
    float totalTime = 30;

    void Awake() {
        if(Instance == null) {
            Instance = this;    
        }
        else {
            Destroy(gameObject);
        }

        Time.timeScale = 1.0f;
    }

    void Start()
    {
        InvokeRepeating("MakeRain", 0, 0.5f);
    }

    void Update()
    {
        if(totalTime > 0f) {
            totalTime -= Time.deltaTime;
            timeText.text = totalTime.ToString("N2");
        }
        else {
            totalTime = 0f;
            endPanel.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    void MakeRain() {
        Instantiate(rain, gameObject.transform);
    }

    public void AddScore(int score) {
        totalScore += score;
        totalScoreText.text = totalScore.ToString();
    }
}
