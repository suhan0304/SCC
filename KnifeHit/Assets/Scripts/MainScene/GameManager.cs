using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
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
    [TabGroup("Tab","Prefabs")] public List<GameObject> bossPrefabs;

    [TabGroup("Tab","Variables",SdfIconType.CodeSlash, TextColor="Red")]
    [TabGroup("Tab","Variables")] public int RemainKnives;
    [TabGroup("Tab","Variables")] public int stageNum;
    [TabGroup("Tab","Variables")] public int scoreNum;
    [TabGroup("Tab","Variables")] public int bestStageNum;
    [TabGroup("Tab","Variables")] public int bestScoreNum;
    [TabGroup("Tab","Variables")] public Vector3 targetSpawnPosition;

    [TabGroup("Tab","GameObject",SdfIconType.CodeSlash, TextColor="Blue")]
    [TabGroup("Tab","GameObject")] public GameObject currentKnife;
    [TabGroup("Tab","GameObject")] public GameObject currentTarget;

    private bool canContinue = true;


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
        if (targetSpawnPosition == null) {
            targetSpawnPosition = new Vector3(0, 1, 0);
        }

        StartGame();
    }

    public void StartGame() {
        canContinue = true;
        
        bestStageNum = PlayerPrefs.GetInt("BestStage", 1);
        bestScoreNum = PlayerPrefs.GetInt("BestScore", 0);

        scoreNum = 0;
        stageNum = 1;
        
        UIManager.Instance.Initialize();
        StartStage();
    }

    public void ContinueGame() {
        UIManager.Instance.Initialize();
        UIManager.Instance.InitialzieForContinue();
        StartStage();
    }

    private void StartStage() {
        
        if(currentKnife != null) {
            DOTween.Kill(currentKnife);
            Destroy(currentKnife);
        }

        if (stageNum%5 == 0) {
            SpawnBoss();
            Events.OnBossSpawn?.Invoke();
        }
        else {
            SpawnTarget();
        }

        UIManager.Instance.Initialize();
        
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
    public void SpawnBoss() {
        int bossNum = Random.Range(0, bossPrefabs.Count - 1);
        Debug.Log($"[GameManager.cs] SpawnBoss : {bossNum} Boss");
        currentTarget = Instantiate(bossPrefabs[bossNum]);
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

    public void OnContinueButton() {
        Debug.Log("[GameManager.cs] OnContinueButton");
        if (canContinue) {
            canContinue = false;
            ContinueGame();
        }
        else {
            Debug.Log("[GameManager.cs] OnContinueButton - Can't Continue!");
        }
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
