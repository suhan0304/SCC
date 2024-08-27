using System.Collections;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneFader : MonoBehaviour
{
    public static SceneFader Instance;

    
    private void OnEnable() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else {
            Destroy(gameObject);
        }
    }

    public Image img;
    public float fadeDuration = 0.25f;
    private void Start() {
        img = GetComponentInChildren<Image>();
        FadeIn();
    }

    public void FadeIn() {
        img.DOFade(0f, fadeDuration);
    }

    public void FadeTo(string scene) {
        img.DOFade(1f, 0.25f)
            .OnComplete(() => {
                DOTween.Kill(gameObject);
                SceneManager.LoadScene(scene);
                FadeIn();
            }
            );

        
    }
}