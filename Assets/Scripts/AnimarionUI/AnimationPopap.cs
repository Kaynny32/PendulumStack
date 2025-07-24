using DG.Tweening;
using DG.Tweening.CustomPlugins;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class AnimationPopap : MonoBehaviour
{
    [Header("Ease")]
    [SerializeField]
    Ease easeScalePopapShow;
    [SerializeField]
    Ease easeScalePopapHide;

    [Space]
    [Header("Scripts")]
    [SerializeField]
    UniversalAnimator universalAnimator;


    public event Action OnAnimationStart;
    public event Action OnAnimationComplete;


    CanvasGroup _bgImage;
    RectTransform _recBgImage;
    CanvasGroup _conFade;

    private void Start()
    {
        _bgImage = GetComponent<CanvasGroup>();
        _recBgImage = GetComponent<RectTransform>();
        _conFade = transform.GetChild(0).GetComponent<CanvasGroup>();
    }

    public void AnimationScaleShowAndHidePopap(float widthMin, float widthMax, float heightMin, float heightMax, bool isActive = true)
    {
        OnAnimationStart?.Invoke();

        _bgImage.DOKill();
        _recBgImage.DOKill();
        if (isActive)
        {
            _bgImage.DOFade(1f, 0.2f).SetEase(easeScalePopapShow);
            _recBgImage.DOSizeDelta(new Vector2(widthMax, heightMin), 0.5f).SetEase(easeScalePopapShow).OnComplete(() =>
            {
                _recBgImage.DOSizeDelta(new Vector2(widthMax, heightMax), 0.5f).SetEase(easeScalePopapShow).OnComplete(() =>
                {
                    _bgImage.interactable = true;
                    _bgImage.blocksRaycasts = true;
                    ShowAndHideContent();

                    OnAnimationComplete?.Invoke();
                });
            });
        }
        else
        {
            ShowAndHideContent(false);
            _recBgImage.DOSizeDelta(new Vector2(widthMax, heightMin), 0.5f).SetEase(easeScalePopapHide).OnComplete(() =>
            {
                _recBgImage.DOSizeDelta(new Vector2(widthMin, heightMin), 0.5f).SetEase(easeScalePopapHide).OnComplete(() =>
                {
                    _bgImage.DOFade(0f, 0.2f).SetEase(easeScalePopapHide).OnComplete(() =>
                    {
                        _bgImage.interactable = false;
                        _bgImage.blocksRaycasts = false;

                        OnAnimationComplete?.Invoke();
                    });
                });
            });
        }
    }

    public void ShowAndHideContent(bool isActive = true)
    {
        if (isActive)
            universalAnimator.Animate(_conFade, AnimationTargetType.CanvasGroupAlpha, Vector2.zero, 1f, 0.5f, easeScalePopapShow);
        else
            universalAnimator.Animate(_conFade, AnimationTargetType.CanvasGroupAlpha, Vector2.zero, 0f, 0.5f, easeScalePopapHide);
    }
}