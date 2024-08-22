using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class BackgroundEffect : MonoBehaviour
{
    public GameObject Background_White;

    private SpriteRenderer spriteRenderer;

    public float flashDuration = 0.1f;

    private void Start() {
        spriteRenderer = Background_White.GetComponent<SpriteRenderer>();
    }

    private void OnEnable() {
        Events.OnCollisionBetweenKnives += BackgroundFlash;
    }

    private void OnDisable() {
        Events.OnCollisionBetweenKnives -= BackgroundFlash;
    }

    public void BackgroundFlash() {
        spriteRenderer.DOFade(1f, flashDuration / 2)
            .OnComplete(() => spriteRenderer.DOFade(0f, flashDuration/2));
    }

}
