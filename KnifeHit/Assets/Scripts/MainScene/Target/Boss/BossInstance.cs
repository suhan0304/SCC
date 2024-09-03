using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UIElements;

public class BossInstance : Boss
{
    protected override void Start() {
        gameObject.transform.position = GameManager.Instance.targetSpawnPosition;

        StartTargetAnimation();

        if(bossType == -1) {
            Debug.LogError($"[Boss.cs] bossType is not assigned.");
        }
        else {
            RotateBossObject();
        }
    }

    public void RotateBossObject() {
        bossSequence?.Kill();

        switch(bossType) {
            case 0:
                bossSequence = DOTween.Sequence();
                bossSequence.Append(transform.DORotate(new Vector3(0f, 0f, 60f), 1f, RotateMode.FastBeyond360).SetEase(Ease.InOutSine));
                bossSequence.AppendInterval(1f);
                break;
            case 1:
                break;
            case 2:
                break;
            case 3:
                break;
            case 4:
                break;
            case 5:
                break;
        }

        bossSequence.OnComplete(()=>{RotateBossObject();});
    }
}
