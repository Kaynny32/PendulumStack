using DG.Tweening;
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


    private void Start()
    {
        MenuAnimationOpenOrClose();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            MenuAnimationOpenOrClose();
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            MenuAnimationOpenOrClose(false);
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
                    _menuPanel.DOFade(0f, 0.5f).SetEase(_hideEase);
                    _menuPanel.interactable = IsActive;
                    _menuPanel.blocksRaycasts = IsActive;
                });
            });
        }
    }

    public void GameAnimationOpen()
    {
        
    }

    public void GameAnimationClose()
    {

    }

}
