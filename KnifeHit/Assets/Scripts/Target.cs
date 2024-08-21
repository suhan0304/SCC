using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using JetBrains.Annotations;
using Sirenix.OdinInspector;
using UnityEngine;

public class Target : MonoBehaviour
{
    [TabGroup("Tap","Rotate",SdfIconType.CodeSlash, TextColor="Green")]
    
    [TabGroup("Tap","Rotate")] public float minRotation = 90f;

    [TabGroup("Tap","Rotate")] public float maxRotation = 720f;

    
    [TabGroup("Tap","Rotate")] public float minDuration = 1f;
    [TabGroup("Tap","Rotate")]  public float maxDuration = 3f;

    
    [TabGroup("Tap","Rotate")] public float maxRotationSpeed = 360f;

    [TabGroup("Tap","ReadOnly"), ReadOnly] public float targetRotation = 0;

    [TabGroup("Tap","Rotate")] 
    [Button("Rotate Target")]
    public void RotateTarget() {
        float randomZRotation = Random.Range(minRotation, maxRotation);

        float rotationDirection = Random.Range(0, 2) * 2 - 1;

        targetRotation = transform.eulerAngles.z + rotationDirection * (randomZRotation - transform.eulerAngles.z);
        

        float rotationDistance = Mathf.Abs(Mathf.DeltaAngle(transform.eulerAngles.z, targetRotation));
        float calculatedDurtaion = Mathf.Clamp(rotationDistance / maxRotationSpeed, minDuration, maxDuration);

        transform.DORotate(new Vector3(0f, 0f, targetRotation), calculatedDurtaion, RotateMode.FastBeyond360)
            .SetEase(Ease.InOutSine);
            //.OnComplete(() => Debug.Log("Rotation Complete!"));
    }
}
