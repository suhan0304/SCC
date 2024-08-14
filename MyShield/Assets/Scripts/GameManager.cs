using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    bool isPlay = true;

    public GameObject square;
    public Text timeText;

    float time = 0f;

    [Header("End Panel")]
    public GameObject endPanel;
    public Text nowScore;
    public Text BestScore;

    void Awake()
    {
            if(Instance == null)
            {
                    Instance = this;
            }
    }

    void Start()
    {
        InvokeRepeating("MakeSquare", 0.0f, 1.0f);
    }

    void Update() {
        if (isPlay) {
            time += Time.deltaTime;
            timeText.text = time.ToString("N2");
        }
    }

    void MakeSquare() {
        Instantiate(square);
    }

    public void GameOver() {
        isPlay = false;
        Time.timeScale = 0.0f;
        nowScore.text = time.ToString("N2");
        endPanel.SetActive(true);
    }
}
