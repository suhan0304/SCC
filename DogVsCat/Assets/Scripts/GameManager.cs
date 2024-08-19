using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    [Header("Cat Prefab")]
    public GameObject normalCat;
    public GameObject fatCat;
    public GameObject pirateCat;

    public GameObject retryBtn;

    int level = 0;
    int score = 0;

    public Text levelText;
    public GameObject levelFront;


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

        if (level == 1) {
            float p = Random.Range(0, 10);
            if (p < 2) Instantiate(normalCat);
        }
        else if (level == 2) {
            float p = Random.Range(0, 10);
            if (p < 5) Instantiate(normalCat);
        }
        else if (level == 3) {
            Instantiate(fatCat);
        }
        else if (level >= 4) {
            Instantiate(pirateCat);
        }
    }

    public void GameOver() {
        retryBtn.SetActive(true);
        Time.timeScale = 0.0f;
    }

    public void AddScore() {
        score++;
        level = score / 5;

        levelText.text = level.ToString();
        levelFront.transform.localScale = new Vector3((score - level * 5) / 5.0f, 1.0f, 1.0f);
    }
}
