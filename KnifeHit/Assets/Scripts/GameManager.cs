using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private void OnEnable() {
        if (Instance == null) {
            Instance = this;
        }
        else {
            Destroy(gameObject);
        }
    }

    [Header("Prefabs")]
    public GameObject knife;
    public GameObject target;

    [Button("SpawnKnife")]
    public void SpawnKnife() {
        GameObject currentKnife = Instantiate(knife);
    }

    [Button("SpawnTarget")]
    public void SpawnTarget() {
        GameObject currentTarget = Instantiate(target);
    }

    public void Start() {
        new WaitForSeconds(0.5f);
        SpawnKnife();
    }
}
