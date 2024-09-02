using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class Boss : Target
{
    [TabGroup("Boss","Settings")] public string bossName;
    [TabGroup("Boss","Settings")] public float timeLimitSeconds = 30f;
    [TabGroup("Boss","Settings")] public float timeCircleSeconds = 10f;
    [TabGroup("Boss","Settings")] public GameObject timeCircle;

    private Sequence bossSpawnSequence;


    protected override void OnEnable() {
        base.OnEnable();
        Events.OnBossSpawn += OnBossSpawn;
        Events.OnBossDestroy += OnBossDestroy;
    }

    protected override void OnDisable() {
        base.OnDisable();
        Events.OnBossSpawn -= OnBossSpawn;
        Events.OnBossDestroy -= OnBossDestroy;
        
    }

    protected override void Start() {
        base.Start();
    }

    private void OnBossSpawn() {
        timeCircle = UIManager.Instance.bossTimeCircle;
        bossSpawnSequence = DOTween.Sequence()
            .AppendInterval(timeLimitSeconds - timeCircleSeconds)
            .AppendCallback(()=> {
                timeCircle.GetComponent<Image>().fillAmount = 1f;
                timeCircle.SetActive(true);
            })
            .Append(timeCircle.GetComponent<Image>().DOFillAmount(0f, timeCircleSeconds).SetEase(Ease.Linear)
            .OnComplete(()=> {
                GameManager.Instance.GameOver();
            }));
    }

    private void OnBossDestroy() {
        bossSpawnSequence?.Kill();
        timeCircle.SetActive(false);
    }
}
