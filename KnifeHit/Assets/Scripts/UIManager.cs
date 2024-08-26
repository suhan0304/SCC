using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public GameObject knifeIconsContainer;
    public GameObject KnifeIconPrefab;
    public List<GameObject> KnifeIcons;

    public TextMeshPro stageText;

    private void OnEnable() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else {
            Destroy(gameObject);
        }

        Events.OnStartStage += OnStartStage;
    }

    private void OnDisable() {
        Events.OnStartStage -= OnStartStage;
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
    }

    public void OnStartStage(int _stageNum) {
        stageText.text = "STAGE " + _stageNum;
    }
} 
