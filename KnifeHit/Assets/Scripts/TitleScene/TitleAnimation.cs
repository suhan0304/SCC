using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class TitleAnimation : MonoBehaviour
{
    public Image titleImage1;
    public Image titleImage2;

    public float moveDistance = 60f;
    public float animationDuration = 1.5f;  

    public void Start() {
        StartTitleAnimation();
    }

    public void StartTitleAnimation() {
        titleImage1.rectTransform.DOAnchorPosX(titleImage1.rectTransform.anchoredPosition.x + moveDistance, animationDuration)
            .SetEase(Ease.InOutSine)
            .SetLoops(-1,LoopType.Yoyo);

        titleImage2.rectTransform.DOAnchorPosX(titleImage2.rectTransform.anchoredPosition.x - moveDistance, animationDuration)
            .SetEase(Ease.InOutSine)
            .SetLoops(-1,LoopType.Yoyo);
    }

    public void StopTitleAnimation() {
        DOTween.KillAll();
    }
}
