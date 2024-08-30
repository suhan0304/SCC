using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [ReadOnly] public static GameManager Instance;

    [TabGroup("Tab","Prefabs",SdfIconType.CodeSlash, TextColor="Green")]
    [TabGroup("Tab","Prefabs")] public GameObject knife;
    [TabGroup("Tab","Prefabs")] public GameObject target;
    [TabGroup("Tab","Prefabs")] public GameObject boss;

    [TabGroup("Tab","Variables",SdfIconType.CodeSlash, TextColor="Red")]
    [TabGroup("Tab","Variables")] public int RemainKnives;
    [TabGroup("Tab","Variables")] public int stageNum;
    [TabGroup("Tab","Variables")] public int scoreNum;
    [TabGroup("Tab","Variables")] public int bestStageNum;
    [TabGroup("Tab","Variables")] public int bestScoreNum;

    [TabGroup("Tab","GameObject",SdfIconType.CodeSlash, TextColor="Blue")]
    [TabGroup("Tab","GameObject")] public GameObject currentKnife;
    [TabGroup("Tab","GameObject")] public GameObject currentTarget;


    private void OnEnable() {
        if (Instance == null) {
            Instance = this;
        }
        else {
            Destroy(gameObject);
        }

        Events.OnTouchScreen += OnTouchScreen;
        Events.OnGameOver += OnGameOver;
        Events.OnFinishStage += NextStage;
        Events.OnRestartButton += OnRestartButton;
    }

    private void OnDisable() {
        Events.OnTouchScreen -= OnTouchScreen;
        Events.OnGameOver -= OnGameOver;
        Events.OnFinishStage -= NextStage;
        Events.OnRestartButton -= OnRestartButton;
    }

    public void Start() {
        StartGame();
    }

    public void StartGame() {
        bestStageNum = PlayerPrefs.GetInt("BestStage", 1);
        bestScoreNum = PlayerPrefs.GetInt("BestScore", 0);

        scoreNum = 0;
        stageNum = 1;
        
        UIManager.Instance.Initialize();
        StartStage();
    }

    private void StartStage() {
        if (stageNum%5 == 0) {
            SpawnBoss(stageNum%5);
            Events.OnBossSpawn?.Invoke();
        }
        else {
            SpawnTarget();
        }

        UIManager.Instance.Initialize();
        UIManager.Instance.SpawnKnivesIcon(RemainKnives);
        
        SpawnKnife();
        Events.OnStartStage?.Invoke(stageNum);
    }

    private void NextStage() {
        stageNum += 1;
        StartStage();
    }

    [Button("SpawnKnife")]
    public void SpawnKnife() {
        currentKnife = Instantiate(knife);
    }

    [Button("SpawnTarget")]
    public void SpawnTarget() {
        currentTarget = Instantiate(target);
        RemainKnives = currentTarget.GetComponent<Target>().knivesToDestroy;
    }

    [Button("SpawnBoss")]
    public void SpawnBoss(int bossNum) {
        currentTarget = Instantiate(boss);
        RemainKnives = currentTarget.GetComponent<Target>().knivesToDestroy;
    }

    public void OnTouchScreen() {
        RemainKnives--;
        UIManager.Instance.DecreaseKnifeCount(RemainKnives);
    }

    public void OnGameOver() {
        if (scoreNum > bestScoreNum) {
            PlayerPrefs.SetInt("BestScore", scoreNum);
            PlayerPrefs.SetInt("BestStage", stageNum);

            Events.OnNewBestScore?.Invoke();
        }
    }

    public void OnRestartButton() {
        StartGame();
    }

    [Button("Reload MainScene")]
    public void ReloadMainScene() {
        SceneFader.Instance.FadeTo("MainScene");
    }

    [Button("GameOver")]
    public void GameOver() {
        Events.OnGameOver?.Invoke();
    }
}
