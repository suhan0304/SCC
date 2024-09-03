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

        bossSequence = DOTween.Sequence();

        switch(bossType) {
            case 0:
                bossSequence
                    .Append(transform.DORotate(new Vector3(0f, 0f, 35f), 0.25f, RotateMode.FastBeyond360)
                        .SetEase(Ease.InOutSine))
                    .AppendInterval(0.25f)
                    .SetLoops(-1, LoopType.Incremental); 
                break;
            case 1:
                bossSequence
                    .Append(transform.DORotate(new Vector3(0f, 0f, 70f), 0.5f, RotateMode.FastBeyond360).SetEase(Ease.InOutCubic))
                    .Append(transform.DORotate(new Vector3(0f, 0f, -140f), 1f, RotateMode.FastBeyond360).SetEase(Ease.InOutCubic).SetLoops(-1, LoopType.Yoyo));
                break;
            case 2:
                bossSequence
                    .Append(transform.DORotate(new Vector3(0f, 0f, 360f), 2f, RotateMode.FastBeyond360).SetEase(Ease.Linear))
                    .SetLoops(-1, LoopType.Incremental); 
                break;
            case 3:
                bossSequence
                    .Append(transform.DORotate(new Vector3(0f, 0f, Random.Range(180f, 240f) * (Random.value > 0.5f ? 1f : -1f)), 1f, RotateMode.FastBeyond360)
                        .SetEase(Ease.InOutSine))
                    .AppendInterval(0.5f)
                    .SetLoops(-1, LoopType.Incremental); 
                break;
            case 4:
                bossSequence
                    .Append(transform.DORotate(new Vector3(0f, 0f, -360f * 4.3f), 4.5f, RotateMode.FastBeyond360).SetEase(Ease.InOutSine))
                    .AppendInterval(0.5f)
                    .SetLoops(-1, LoopType.Incremental); 
                break;
            case 5:
                bossSequence
                    .Append(transform.DORotate(new Vector3(0f, 0f, Random.Range(60f, 100f) * (Random.value > 0.5f ? 1f : -1f)), 0.5f, RotateMode.FastBeyond360)
                        .SetEase(Ease.Linear)
                        .SetLoops(Random.Range(1,5), LoopType.Yoyo))
                    .SetLoops(-1, LoopType.Incremental);
                break;
        }
    }
}
