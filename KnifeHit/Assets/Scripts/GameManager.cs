using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Prefabs")]
    public GameObject knife;
    public GameObject target;

    [Header("Knives")]
    public int RemainKnives;

    private void OnEnable() {
        if (Instance == null) {
            Instance = this;
        }
        else {
            Destroy(gameObject);
        }

        Events.OnTouchScreen += OnTouchScreen;
    }

    private void OnDisable() {
        Events.OnTouchScreen -= OnTouchScreen;
    }

    [Button("SpawnKnife")]
    public void SpawnKnife() {
        GameObject currentKnife = Instantiate(knife);
    }

    [Button("SpawnTarget")]
    public void SpawnTarget() {
        GameObject currentTarget = Instantiate(target);
        RemainKnives = currentTarget.GetComponent<Target>().knivesToDestroy;
        UIManager.Instance.SpawnKnivesIcon(RemainKnives);
    }

    public void Start() {
        SpawnTarget();
        SpawnKnife();
    }

    public void OnTouchScreen() {
        RemainKnives--;
        UIManager.Instance.DecreaseKnifeCount(RemainKnives);
        if (RemainKnives == 0) {
            Events.OnAllKnivesOnHit.Invoke();
        }
    }
}
