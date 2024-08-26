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

    private float fadeDuration = 0.2f;

    public TMP_Text stageText;

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
    }

    private void OnDisable() {
        Events.OnStartStage -= OnStartStage;
        Events.OnAllKnivesOnHit -= OnAllKnivesOnHit;
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
        ShowFadeAnimation();
    }

    public void OnAllKnivesOnHit() {
        HideFadeAnimation();
    }

    private void ShowFadeAnimation() {
        stageText.DOFade(1, fadeDuration);
    }
    private void HideFadeAnimation() {
        stageText.DOFade(0, fadeDuration);
    }

} 
