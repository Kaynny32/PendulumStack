using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UniversalAnimator : MonoBehaviour
{
    public static UniversalAnimator universalAnimator;

    private void Awake()
    {
        if (universalAnimator == null)
            universalAnimator = this;
    }

    public void Animate(
         Component target,
         AnimationTargetType animationType,
         Vector2 targetVector,
         float targetFloat,
         float duration,
         Ease ease = Ease.Linear,
         TweenCallback onComplete = null
     )
    {
        Tween tween = null;

        switch (animationType)
        {
            case AnimationTargetType.CanvasGroupAlpha:
                if (target is CanvasGroup canvasGroup)
                {
                    target.DOKill();
                    tween = canvasGroup.DOFade(targetFloat, duration).SetEase(ease);
                }
                break;
            case AnimationTargetType.FadeImage:
                if (target is Image image)
                {
                    target.DOKill();
                    tween = image.DOFade(targetFloat, duration).SetEase(ease);
                }
                break;
            case AnimationTargetType.FadeText:
                if (target is TextMeshProUGUI text)
                {
                    target.DOKill();
                    tween = text.DOFade(targetFloat, duration).SetEase(ease);
                }
                break;

            case AnimationTargetType.RectSize:
                if (target is RectTransform rectTransform)
                {
                    target.DOKill();
                    tween = rectTransform.DOSizeDelta(targetVector, duration).SetEase(ease);
                }
                break;

            case AnimationTargetType.Scale:
                if (target is Transform tf)
                {
                    target.DOKill();
                    tween = tf.DOScale(targetVector, duration).SetEase(ease);
                }
                break;
        }
        tween?.OnComplete(onComplete);
    }
}

public enum AnimationTargetType
{
    CanvasGroupAlpha,
    FadeImage,
    FadeText,
    RectSize,
    Scale
}