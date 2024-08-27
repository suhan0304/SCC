using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public GameObject knifeIconsContainer;
    public GameObject KnifeIconPrefab;
    public List<GameObject> KnifeIcons;

    private float fadeDuration = 0.35f;

    public TMP_Text stageText;
    public TMP_Text scoreText;

    public TMP_Text gameoverStageText;
    public TMP_Text gameoverScoreText;

    private void OnEnable() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else {
            Destroy(gameObject);
        }

        Events.OnStartStage += OnStartStage;
        Events.OnAllKnivesOnHit += OnAllKnivesOnHit;
        Events.OnHitTarget += UpdateScore;
    }

    private void OnDisable() {
        Events.OnStartStage -= OnStartStage;
        Events.OnAllKnivesOnHit -= OnAllKnivesOnHit;
        Events.OnHitTarget -= UpdateScore;
    }

    public void SpawnKnivesIcon(int cntKnives) {
        for (int i = 0; i < cntKnives; i++) {
            GameObject knifeIcon = Instantiate(KnifeIconPrefab);
            knifeIcon.transform.SetParent(knifeIconsContainer.transform);
            KnifeIcons.Add(knifeIcon);
        }
    }

    public void DecreaseKnifeCount(int RemainKnives) {
        if (RemainKnives < 0) {
            return; 
        }
        KnifeIcons[(KnifeIcons.Count - 1)- RemainKnives].GetComponent<Image>().color = Color.black;
    }

    public void Initialize() {
        foreach (GameObject icon in KnifeIcons) {
            Destroy(icon);
        }
        KnifeIcons.Clear();
        UpdateScore();
    }

    public void OnStartStage(int _stageNum) {
        stageText.text = "STAGE " + _stageNum;
        ShowFadeAnimation();
    }

    public void UpdateScore() {
        scoreText.text = GameManager.Instance.scoreNum.ToString();
    }

    public void OnAllKnivesOnHit() {
        HideFadeAnimation();
    }

    public void OnGameOver() {
        gameoverStageText.text = GameManager.Instance.stageNum.ToString();
        gameoverScoreText.text = GameManager.Instance.scoreNum.ToString();

        
    }

    private void ShowFadeAnimation() {
        stageText.DOFade(1, fadeDuration);
    }
    private void HideFadeAnimation() {
        stageText.DOFade(0, fadeDuration);
    }

} 
