using System.Collections;
using System.Collections.Generic;
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
        RotateBossObject();
    }

    public void RotateBossObject() {
        switch(bossType) {
            case 0:
                
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
    }
}
