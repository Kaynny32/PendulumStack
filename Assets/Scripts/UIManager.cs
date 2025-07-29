using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("Text")]
    [SerializeField]
    TextMeshProUGUI _scoreTxt;
    [SerializeField]
    TextMeshProUGUI _timrText;
    [SerializeField]
    TextMeshProUGUI _resultTxt;

    int _score = 0;
    int _time = 0;
    int _result = 0;

    public void SetScoreTxt(int sc)
    {
        _score = sc;
        _scoreTxt.text = $"Score: {_score}";
    }

    public void SetTimeText(int i)
    {
        _time = i;
        _timrText.text = $"{_time}";
    }

    public void SetResult(int res)
    {
        _result = res;
        _resultTxt.text = $"Your Score: {_result}";
    }
}
