using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Line")]
    [SerializeField]
    GameDateLine _lineOne;
    [SerializeField]
    GameDateLine _lineTwo;
    [SerializeField]
    GameDateLine _lineThree;

    [Space]
    [Header("Scripts")]
    [SerializeField]
    UIManager _uiManager;
    [SerializeField]
    AnimationManagerUI _animationManagerUI;
    [SerializeField]
    Pendulum pendulum;
    [SerializeField]
    SpawnManager spawnManager;

    int _score = 0;
    BobItem bobItem;
    bool isGameStart;
    bool _isTouchProcessed;
    int time = 4;


    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    private void Update()
    {
        if (isGameStart && bobItem != null)
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Began && !_isTouchProcessed)
                {
                    bobItem.SetIsFolowing(false);
                    spawnManager.SpawnBob();
                    _isTouchProcessed = true;
                }
                else if (touch.phase == TouchPhase.Ended)
                {
                    _isTouchProcessed = false;
                }
            }
        }
    }


    public void StartBtn()
    {
        if (time != 0)
        {
            StartCoroutine(Starttimer());

        }
        else
        {
            _animationManagerUI.HideTimeText();
            StartGame();
            time = 4;
        }
    }

    IEnumerator Starttimer()
    {
        time -= 1;
        _uiManager.SetTimeText(time);
        yield return new WaitForSeconds(1f);
        StartBtn();
    }

    public void StartGame()
    {
        isGameStart = true;
        spawnManager.SpawnBob();
        pendulum.StartSwingAnimation();
    }

    public void SetScore()
    {
        _score += 10;
        _uiManager.SetScoreTxt(_score);
    }

    public void CheckLine(int indexLine)
    {
        switch (indexLine)
        {
            case 1:
                _lineOne._isActive = false;
                break;
            case 2:
                _lineTwo._isActive = false;
                break;
            case 3:
                _lineThree._isActive = false;
                break;
        }
        if (_lineOne._isActive == false && _lineTwo._isActive == false && _lineThree._isActive == false)
        {
            Destroy(bobItem.gameObject);
            _animationManagerUI.GameAnimationOpenorClose(false);
            pendulum.StopAnimPendel();
            _uiManager.SetResult(_score);
        }
    }

    public void SetBobItem(BobItem bobItem)
    {
        this.bobItem = bobItem;
    }

    public void RestGame()
    {
        isGameStart = false;
        _score = 0;
        _uiManager.SetScoreTxt(_score);

        _lineOne._isActive = true;
        _lineTwo._isActive = true;
        _lineThree._isActive = true;

        _lineOne._line.ResetLine();
        _lineTwo._line.ResetLine();
        _lineThree._line.ResetLine();

        //StartBtn();
    }
}
[Serializable]
public class GameDateLine
{
    public Line _line;
    public bool _isActive = true;
}
