using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AnimationManagerUI : MonoBehaviour
{
    [Space]
    [Header("Canvas Group")]
    [SerializeField]
    CanvasGroup _menuPanel;
    [SerializeField]
    CanvasGroup _gamePanel;
    [SerializeField]
    CanvasGroup _resultPanel;

    [Space]
    [Header("Ease")]
    [SerializeField]
    Ease _showEase;
    [SerializeField]
    Ease _hideEase;

    [Space]
    [Header("Menu Panel Elements")]
    [SerializeField]
    TextMeshProUGUI _logoText;
    [SerializeField]
    CanvasGroup _startBtn;

    [Space]
    [Header("Game Panel Elements")]
    [SerializeField]
    TextMeshProUGUI _scoreTxt;
    [SerializeField]
    RectTransform _lineOne;
    [SerializeField]
    RectTransform _lineTwo;
    [SerializeField]
    RectTransform _lineThree;
    [SerializeField]
    CanvasGroup _pendulum;

    [Space]
    [Header("Scripts")]
    [SerializeField]
    AnimationPopap animationPopap;


    private void Start()
    {
        MenuAnimationOpenOrClose();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            GameAnimationOpenorClose();
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            GameAnimationOpenorClose(false);
        }
    }

    public void MenuAnimationOpenOrClose(bool IsActive = true)
    {
        _menuPanel.DOKill();
        _logoText.DOKill();
        _startBtn.DOKill();
        if (IsActive)
        {
            _menuPanel.DOFade(1f, 0.5f).SetEase(_showEase).OnComplete(() =>
            {
                _menuPanel.interactable = IsActive;
                _menuPanel.blocksRaycasts = IsActive;
                _logoText.DOFade(1f, 0.5f).SetEase(_showEase).OnComplete(() =>
                {
                    _startBtn.interactable = IsActive;
                    _startBtn.blocksRaycasts = IsActive;
                    _startBtn.DOFade(1f, 0.5f).SetEase(_showEase);
                });
            });
        }
        else
        {
            _startBtn.interactable = IsActive;
            _startBtn.blocksRaycasts = IsActive;
            _startBtn.DOFade(0f, 0.5f).SetEase(_hideEase).OnComplete(() =>
            {
                _logoText.DOFade(0f, 0.5f).SetEase(_hideEase).OnComplete(() =>
                {
                    _menuPanel.DOFade(0f, 0.5f).SetEase(_hideEase).OnComplete(() =>
                    {
                        GameAnimationOpenorClose();
                    });
                    _menuPanel.interactable = IsActive;
                    _menuPanel.blocksRaycasts = IsActive;
                });
            });
        }
    }

    public void GameAnimationOpenorClose(bool isActve = true)
    {
        _gamePanel.DOKill();
        _scoreTxt.DOKill();
        _lineThree.DOKill();
        _lineTwo.DOKill();
        _lineOne.DOKill();
        if (isActve)
        {
            _gamePanel.interactable = isActve;
            _gamePanel.blocksRaycasts = isActve;
            _gamePanel.DOFade(1f, 0.5f).SetEase(_showEase).OnComplete(() =>
            {
                _scoreTxt.GetComponent<RectTransform>().DOAnchorPosY(-100, 0.75f).SetEase(_showEase);
                _scoreTxt.DOFade(1f, 0.75f).SetEase(_showEase).OnComplete(() =>
                {
                    _pendulum.interactable = isActve;
                    _pendulum.DOFade(1f, 0.75f).SetEase(_showEase).OnComplete(() =>
                    {
                        _lineThree.DOAnchorPosY(800f, 0.75f).SetEase(_showEase);
                        _lineThree.GetComponent<CanvasGroup>().interactable = isActve;
                        _lineThree.GetComponent<CanvasGroup>().DOFade(1f, 0.75f).SetEase(_showEase).OnComplete(() =>
                        {
                            _lineTwo.DOAnchorPosY(800f, 0.75f).SetEase(_showEase);
                            _lineTwo.GetComponent<CanvasGroup>().interactable = isActve;
                            _lineTwo.GetComponent<CanvasGroup>().DOFade(1f, 0.75f).SetEase(_showEase).OnComplete(() =>
                            {
                                _lineOne.DOAnchorPosY(800f, 0.75f).SetEase(_showEase);
                                _lineOne.GetComponent<CanvasGroup>().interactable = isActve;
                                _lineOne.GetComponent<CanvasGroup>().DOFade(1f, 0.75f).SetEase(_showEase);
                            });
                        });
                    });
                });
            });
        }
        else
        {
            _lineOne.DOAnchorPosY(0f, 0.55f).SetEase(_hideEase);
            _lineOne.GetComponent<CanvasGroup>().interactable = isActve;
            _lineOne.GetComponent<CanvasGroup>().DOFade(0f, 0.5f).SetEase(_hideEase).OnComplete(() =>
            {
                _lineTwo.DOAnchorPosY(0f, 0.5f).SetEase(_hideEase);
                _lineTwo.GetComponent<CanvasGroup>().interactable = isActve;
                _lineTwo.GetComponent<CanvasGroup>().DOFade(0f, 0.5f).SetEase(_hideEase).OnComplete(() =>
                {
                    _lineThree.DOAnchorPosY(0f, 0.5f).SetEase(_hideEase);
                    _lineThree.GetComponent<CanvasGroup>().interactable = isActve;
                    _lineThree.GetComponent<CanvasGroup>().DOFade(0f, 0.5f).SetEase(_hideEase).OnComplete(() =>
                    {
                        _pendulum.interactable = isActve;
                        _pendulum.DOFade(0f, 0.5f).SetEase(_hideEase).OnComplete(() =>
                        {
                            _scoreTxt.GetComponent<RectTransform>().DOAnchorPosY(0, 0.5f).SetEase(_hideEase);
                            _scoreTxt.DOFade(0f, 0.5f).SetEase(_hideEase).OnComplete(() =>
                            {
                                _gamePanel.interactable = isActve;
                                _gamePanel.blocksRaycasts = isActve;
                                _gamePanel.DOFade(0f, 0.5f).SetEase(_hideEase);
                                ResultPanelCloseOrOpen();
                            });
                        });
                    });
                });
            });
        }
    }

    public void ResultPanelCloseOrOpen(bool isActive = true)
    {
        _resultPanel.DOKill();
        if (isActive)
        {
            _resultPanel.interactable = isActive;
            _resultPanel.blocksRaycasts = isActive;
            _resultPanel.DOFade(1f, 0.5f).SetEase(_showEase).OnComplete(() => {
                animationPopap.AnimationScaleShowAndHidePopap(10, 650, 3, 900);
            });            
        }
        else
        {
            _resultPanel.interactable = isActive;
            _resultPanel.blocksRaycasts = isActive;
            animationPopap.AnimationScaleShowAndHidePopap(10, 650, 900, 3,false);
            _resultPanel.DOFade(0f, 0.5f).SetEase(_hideEase);
        }
    }
}
