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
    public Animator anim;

    float time = 0f;

    [Header("End Panel")]
    public GameObject endPanel;
    public Text nowScore;
    public Text bestScore;

    void Awake()
    {
            if(Instance == null)
            {
                    Instance = this;
            }
    }

    void Start()
    {
        Time.timeScale =  1.0f;
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
        anim.SetBool("isDie", true);
        Invoke("TimeStop", 0.5f);

        nowScore.text = time.ToString("N2");

        string key = "bestScore";
        
        if (PlayerPrefs.HasKey(key)) {
            float best = PlayerPrefs.GetFloat(key);
            if (best < time) {
                PlayerPrefs.SetFloat(key, time);
                bestScore.text = time.ToString("N2");
            }
            else {
                bestScore.text = best.ToString("N2");
            }
        }
        else {
            PlayerPrefs.SetFloat(key, time);
            bestScore.text = time.ToString("N2");
        }
        endPanel.SetActive(true);
    }

    void TimeStop() {
        Time.timeScale = 0.0f;
    }
}
