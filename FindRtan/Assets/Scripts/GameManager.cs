using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public Text timeText;
    float time = 0.00f;


    public int cardCount = 0;

    public GameObject endText;

    void Update() {
        time += Time.deltaTime;
        timeText.text = time.ToString("N2");
        if (time >= 30.0f) { 
            EndGame();
        }
    }
        void Awake() {
        if(Instance == null)  {
            Instance = this;
        }
        else {
            Destroy(gameObject);
        }
    }

    public void EndGame() {
        endText.SetActive(true);
        Time.timeScale = 0.0f;
    }
}
