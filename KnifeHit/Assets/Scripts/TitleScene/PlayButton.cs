using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour
{
    public TitleAnimation titleAnimation;
    public void Play() {
        titleAnimation.StopTitleAnimation();
        SceneManager.LoadScene("MainScene");
    }
}
