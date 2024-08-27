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

    [TabGroup("Tab","Variables",SdfIconType.CodeSlash, TextColor="Red")]
    [TabGroup("Tab","Variables")] public int RemainKnives;
    [TabGroup("Tab","Variables")] public int stageNum;
    [TabGroup("Tab","Variables")] public int scoreNum;
    [TabGroup("Tab","Variables")] public int bestStageNum;
    [TabGroup("Tab","Variables")] public int bestScoreNum;



    [TabGroup("Tab","GameObjects",SdfIconType.CodeSlash, TextColor="Yellow")]
    [TabGroup("Tab","GameObjects")] public GameObject PlayObects;

    private void OnEnable() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else {
            Destroy(gameObject);
        }

        Events.OnTouchScreen += OnTouchScreen;
        Events.OnGameOver += OnGameOver;
        Events.OnFinishStage += NextStage;
    }

    private void OnDisable() {
        Events.OnTouchScreen -= OnTouchScreen;
        Events.OnGameOver -= OnGameOver;
        Events.OnFinishStage -= NextStage;
    }

    public void Start() {
        bestStageNum = PlayerPrefs.GetInt("BestStage", 1);
        bestScoreNum = PlayerPrefs.GetInt("BestScore", 0);

        scoreNum = 0;
        stageNum = 1;
        
        UIManager.Instance.Initialize();
        StartStage();
    }


    private void StartStage() {
        SpawnTarget();
        SpawnKnife();
        Events.OnStartStage?.Invoke(stageNum);
    }

    private void NextStage() {
        stageNum += 1;
        StartStage();
    }

    [Button("SpawnKnife")]
    public void SpawnKnife() {
        GameObject currentKnife = Instantiate(knife);
    }

    [Button("SpawnTarget")]
    public void SpawnTarget() {
        GameObject currentTarget = Instantiate(target);

        currentTarget.transform.SetParent(PlayObects.transform);
        RemainKnives = currentTarget.GetComponent<Target>().knivesToDestroy;

        UIManager.Instance.Initialize();
        UIManager.Instance.SpawnKnivesIcon(RemainKnives);
    }

    public void OnTouchScreen() {
        RemainKnives--;
        UIManager.Instance.DecreaseKnifeCount(RemainKnives);
    }

    public void OnGameOver() {

    }

    [Button("Reload MainScene")]
    public void ReloadMainScene() {
        SceneManager.LoadScene("MainScene");
    }
}
