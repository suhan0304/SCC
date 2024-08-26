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
    }

    private void OnDisable() {
        Events.OnTouchScreen -= OnTouchScreen;
        Events.OnCollisionBetweenKnives -= GameOver;
    }

    [Button("SpawnKnife")]
    public void SpawnKnife() {
        GameObject currentKnife = Instantiate(knife);
    }

    [Button("SpawnTarget")]
    public void SpawnTarget() {
        SpawnKnife();
        GameObject currentTarget = Instantiate(target);
        RemainKnives = currentTarget.GetComponent<Target>().knivesToDestroy;
        UIManager.Instance.Initialize();
        UIManager.Instance.SpawnKnivesIcon(RemainKnives);
    }

    public void Start() {
        SpawnTarget();
    }

    public void OnTouchScreen() {
        RemainKnives--;
        UIManager.Instance.DecreaseKnifeCount(RemainKnives);
    }

    public void GameOver() {

    }
}
