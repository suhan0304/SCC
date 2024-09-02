using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Boss : Target
{
    [TabGroup("Boss","Settings")] public string bossName;
    [TabGroup("Boss","Settings")] public float timeLimitSeconds = 10f;
    [TabGroup("Boss","Settings")] public GameObject timeCircle;


    private void OnEnable() {
        Events.OnBossSpawn += OnBossSpawn;
        Events.OnBossDestroy += OnBossDestroy;
    }

    private void OnDisable() {
        Events.OnBossSpawn -= OnBossSpawn;
        Events.OnBossDestroy -= OnBossDestroy;
        
    }

    private void Start() {
        timeCircle.SetActive(false);
    }

    private void OnBossSpawn() {
        StartCoroutine(StartTimeCircle());
    }

    private void OnBossDestroy() {
        DOTween.Kill(timeCircle);
        StopCoroutine(StartTimeCircle());
        timeCircle.SetActive(false);
    }

    private IEnumerator StartTimeCircle() {
        yield return new WaitForSeconds(20f);

        timeCircle.gameObject.SetActive(true);
        
        timeCircle.GetComponent<Image>().DOFillAmount(0f, timeLimitSeconds).SetEase(Ease.Linear)
            .OnComplete(()=> {
                Debug.Log("Time Over!");
            });
    }
}
