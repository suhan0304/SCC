using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using TMPro;

public class StageScoreText : MonoBehaviour
{
    private int bestStage;
    private int bestScore;

    public TMP_Text stageText;
    public TMP_Text scoreText;
    

    private void Start() {
        InitailizeTexts();
    }

    private void InitailizeTexts() {
        bestStage = PlayerPrefs.GetInt("BestStage", 9);
        bestScore = PlayerPrefs.GetInt("BestScore", 9);

        stageText.text = "STAGE " + bestStage;
        scoreText.text = "SCORE " + bestScore;
    }
}
