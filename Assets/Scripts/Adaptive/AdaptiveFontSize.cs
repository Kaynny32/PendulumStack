using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(TextMeshProUGUI))]
public class AdaptiveFontSize : MonoBehaviour
{
    [SerializeField] 
    int referenceFontSize = 24;
    [SerializeField] 
    float minScale = 0.5f;
    [SerializeField]
    float _referenceResolutionWidth = 1920f;

    private TextMeshProUGUI _text;
     

    void Awake() => _text = GetComponent<TextMeshProUGUI>();

    void Update()
    {
        float scaleFactor = Screen.width / _referenceResolutionWidth;
        _text.fontSize = Mathf.RoundToInt(referenceFontSize * Mathf.Max(scaleFactor, minScale));
    }
}