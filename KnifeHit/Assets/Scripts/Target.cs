using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

public class Target : MonoBehaviour
{
    public float minRotation = 0f;
    public float maxRotation = 720f;
    public float duration = 1f;

    [Button("Rotate Target")]
    public void RotateTarget() {
        float randomZRotation = Random.Range(minRotation, maxRotation);

        transform.DORotate(new Vector3(0f, 0f, randomZRotation), duration, RotateMode.FastBeyond360)
            .SetEase(Ease.InOutSine)
            .OnComplete(() => Debug.Log("Rotation Complete!"));
    }
}
