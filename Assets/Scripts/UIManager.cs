using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("Text")]
    [SerializeField]
    TextMeshProUGUI _scoreTxt;

    int _score = 0;

    public void SetScoreTxt(int sc)
    {
        _score = sc;
        _scoreTxt.text = $"Score: {_score}";
    }    
}
