using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    public GameObject normalCat;
    public GameObject retryBtn;

    int level = 0;
    int score = 0;

    void Awake() {
        Application.targetFrameRate = 60;

        if (Instance == null) {
            Instance = this;
        }
        else {
            Destroy(gameObject);
        }
    }

    void Start() {
        Time.timeScale = 1.0f;
        InvokeRepeating("MakeCat", 0.0f, 1.0f);
    }

    void MakeCat() {
        Instantiate(normalCat);
    }

    public void GameOver() {
        retryBtn.SetActive(true);
        Time.timeScale = 0.0f;
    }
    public void AddScore() {
        score++;
        level = score / 5;
    }
}
