using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [ReadOnly] public static GameManager Instance;

    [TabGroup("Tab","Prefabs",SdfIconType.CodeSlash, TextColor="Green")]
    [TabGroup("Tab","Variables",SdfIconType.CodeSlash, TextColor="Red")]

    [TabGroup("Tab","Prefabs")] public GameObject knife;
    [TabGroup("Tab","Prefabs")] public GameObject target;

    [TabGroup("Tab","Variables")] public int RemainKnives;
    [TabGroup("Tab","Variables")] public int stageNum;

    private void OnEnable() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else {
            Destroy(gameObject);
        }

        Events.OnTouchScreen += OnTouchScreen;
        Events.OnCollisionBetweenKnives += GameOver;
        Events.OnFinishStage += NextStage;
    }

    private void OnDisable() {
        Events.OnTouchScreen -= OnTouchScreen;
        Events.OnCollisionBetweenKnives -= GameOver;
        Events.OnFinishStage -= NextStage;
    }

    public void Start() {
        stageNum = 1;
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
        RemainKnives = currentTarget.GetComponent<Target>().knivesToDestroy;
        UIManager.Instance.Initialize();
        UIManager.Instance.SpawnKnivesIcon(RemainKnives);
    }

    public void OnTouchScreen() {
        RemainKnives--;
        UIManager.Instance.DecreaseKnifeCount(RemainKnives);
    }

    public void GameOver() {

    }
}
