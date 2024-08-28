using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [TabGroup("UI","KnifeIcon",SdfIconType.CodeSlash, TextColor="Green")]
    [TabGroup("UI","KnifeIcon")] public GameObject knifeIconsContainer;
    [TabGroup("UI","KnifeIcon")] public GameObject KnifeIconPrefab;
    [TabGroup("UI","KnifeIcon")] public List<GameObject> KnifeIcons;

    [TabGroup("UI","GameOver",SdfIconType.CodeSlash, TextColor="Yellow")]
    [TabGroup("UI","GameOver")] public GameObject gameOverUI;
    [TabGroup("UI","GameOver")] public GameObject NewBestUI;
    [TabGroup("UI","GameOver"), ReadOnly] private float gameOverFadeDuration = 0.5f;
    [TabGroup("UI","GameOver")] public TMP_Text gameoverScoreText;
    [TabGroup("UI","GameOver")] public TMP_Text gameoverStageText;

    [TabGroup("UI","Stage",SdfIconType.CodeSlash, TextColor="Red")]
    [TabGroup("UI","Stage")] public List<GameObject> stageIcons;
    [TabGroup("UI","Stage")] public List<Image> stageIconsImage;
    [TabGroup("UI","Stage")] public int stageIdx;
    [TabGroup("UI","Stage")] public Color stageInitColor;
    [TabGroup("UI","Stage")] public Color stageCurrentColor;

    [TabGroup("Base","In-Game UI",SdfIconType.CodeSlash, TextColor="Blue")]
    [TabGroup("Base","In-Game UI"), ReadOnly] private float fadeDuration = 0.35f;
    [TabGroup("Base","In-Game UI")] public TMP_Text stageText;
    [TabGroup("Base","In-Game UI")] public TMP_Text scoreText;
    [TabGroup("Boss","Boss UI")] public Color bossTextColor;

    private void OnEnable() {
        if (Instance == null) {
            Instance = this;
        }
        else {
            Destroy(gameObject);
        }

        Events.OnStartStage += OnStartStage;
        Events.OnAllKnivesOnHit += OnAllKnivesOnHit;
        Events.OnHitTarget += UpdateScore;
        Events.OnGameOver += OnGameOver;
        Events.OnNewBestScore += OnNewBestScore;
    }

    private void OnDisable() {
        Events.OnStartStage -= OnStartStage;
        Events.OnAllKnivesOnHit -= OnAllKnivesOnHit;
        Events.OnHitTarget -= UpdateScore;
        Events.OnGameOver -= OnGameOver;
        Events.OnNewBestScore -= OnNewBestScore;
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
        KnifeIcons[KnifeIcons.Count - 1 - RemainKnives].GetComponent<Image>().color = Color.black;
    }

    private void Start() {
        gameOverUI.SetActive(false);
        NewBestUI.SetActive(false);

        foreach (GameObject stageIcon in stageIcons) {
            stageIconsImage.Add(stageIcon.GetComponent<Image>());
        }

        InitializeStageIcons();
    }

    public void Initialize() {
        foreach (GameObject icon in KnifeIcons) {
            Destroy(icon);
        }
        KnifeIcons.Clear();
        UpdateScore();
    }

    public void OnStartStage(int _stageNum) {
        if(_stageNum%5 != 0) {
            stageText.text = "STAGE " + _stageNum;
            ShowStageTextFadeAnimation();
        }
    }

    public void SetBossNameText() {
        stageText.color = bossTextColor;
        stageText.text = "BOSS: " + GameManager.Instance.currentTarget.GetComponent<Boss>().bossName;
    }

    public void UpdateScore() {
        scoreText.text = GameManager.Instance.scoreNum.ToString();
    }

    public void OnAllKnivesOnHit() {
        if(stageIdx > 4) {
            InitializeStageIcons();
        }
        else if (stageIdx == 4){
            // TODO - Boss Animation
            //stageIdx++;
        }
        else {
            stageIcons[stageIdx].transform.DOPunchScale(new Vector3(0.25f, 0.25f, 0), 0.5f, 5, 1f)
                .OnComplete(()=> {
                    stageIconsImage[stageIdx++].color = stageCurrentColor;
                });
        }
        HideStageTextFadeAnimation();
    }

    private void InitializeStageIcons() {
        foreach(var stageIconImage in stageIconsImage) {
            stageIconImage.color = stageInitColor;
        }
        stageIdx = 0;
        stageIconsImage[stageIdx++].color = stageCurrentColor;
    }

    public void OnGameOver() {
        CanvasGroup canvasGroup = gameOverUI.GetComponent<CanvasGroup>();

        gameoverScoreText.text = GameManager.Instance.scoreNum.ToString();
        gameoverStageText.text = "STAGE " + GameManager.Instance.stageNum;
        
        gameOverUI.SetActive(true);
        canvasGroup.DOFade(1f, gameOverFadeDuration); 
    }

    public void OnNewBestScore() {
        NewBestUI.SetActive(true);
    }

    private void ShowStageTextFadeAnimation() {
        stageText.DOFade(1, fadeDuration);
    }
    private void HideStageTextFadeAnimation() {
        stageText.DOFade(0, fadeDuration);
    }

    public void RestartButton() {
        InitializeStageIcons();
        CanvasGroup canvasGroup = gameOverUI.GetComponent<CanvasGroup>();
        canvasGroup.DOFade(0f, gameOverFadeDuration)
            .OnComplete(() => {
                gameOverUI.SetActive(false);
                NewBestUI.SetActive(false);

                Events.OnRestartButton?.Invoke();
            }); 

    }

    public void HomeButton() {
        SceneFader.Instance.FadeTo("TitleScene");
    }

    public void Continue() {
        //TODO - Continue Logic
    }

} 
