using UnityEngine;
using UnityEngine.UI;

[ExecuteAlways]
public class UIResolutionAdapter : MonoBehaviour
{
    public enum AdaptationMode
    {
        ScaleWithCanvas,
        MatchWidth,        
        MatchHeight,       
        MatchBoth,         
        FixedAspectRatio   
    }

    [Header("Основные настройки")]
    [SerializeField] private AdaptationMode adaptationMode = AdaptationMode.ScaleWithCanvas;
    [SerializeField] private Vector2 referenceResolution = new Vector2(1920, 1080);
    [SerializeField] private Vector2 sizeInReferenceResolution = new Vector2(200, 100);

    [Header("Ограничения")]
    [SerializeField] private Vector2 minSize = new Vector2(50, 50);
    [SerializeField] private Vector2 maxSize = new Vector2(2000, 2000);
    [SerializeField] private bool useSafeArea = true;

    private RectTransform _rectTransform;
    private Canvas _parentCanvas;
    private Vector2 _lastCanvasSize;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _parentCanvas = GetComponentInParent<Canvas>();
    }

    private void Start()
    {
        UpdateUI();
    }

    private void Update()
    {
#if UNITY_EDITOR
        if (!Application.isPlaying) UpdateUI();
#endif

        if (CanvasSizeChanged())
        {
            UpdateUI();
        }
    }

    private bool CanvasSizeChanged()
    {
        if (_parentCanvas == null) return false;

        Vector2 currentSize = _parentCanvas.pixelRect.size;
        if (currentSize != _lastCanvasSize)
        {
            _lastCanvasSize = currentSize;
            return true;
        }
        return false;
    }

    private void UpdateUI()
    {
        if (_rectTransform == null || _parentCanvas == null) return;

        Vector2 canvasSize = _parentCanvas.pixelRect.size;
        Vector2 safeAreaSize = GetSafeAreaSize(canvasSize);

        Vector2 scaleFactor = new Vector2(
            safeAreaSize.x / referenceResolution.x,
            safeAreaSize.y / referenceResolution.y
        );

        Vector2 newSize = sizeInReferenceResolution;

        switch (adaptationMode)
        {
            case AdaptationMode.ScaleWithCanvas:
                float uniformScale = Mathf.Min(scaleFactor.x, scaleFactor.y);
                newSize *= uniformScale;
                break;

            case AdaptationMode.MatchWidth:
                newSize *= scaleFactor.x;
                break;

            case AdaptationMode.MatchHeight:
                newSize *= scaleFactor.y;
                break;

            case AdaptationMode.MatchBoth:
                newSize = Vector2.Scale(newSize, scaleFactor);
                break;

            case AdaptationMode.FixedAspectRatio:
                float aspectScale = Mathf.Min(scaleFactor.x, scaleFactor.y);
                newSize *= aspectScale;
                break;
        }

        newSize.x = Mathf.Clamp(newSize.x, minSize.x, maxSize.x);
        newSize.y = Mathf.Clamp(newSize.y, minSize.y, maxSize.y);

        _rectTransform.sizeDelta = newSize;

        if (TryGetComponent(out LayoutGroup layoutGroup))
        {
            LayoutRebuilder.MarkLayoutForRebuild(_rectTransform);
        }
    }

    private Vector2 GetSafeAreaSize(Vector2 canvasSize)
    {
        if (!useSafeArea) return canvasSize;

        Rect safeArea = Screen.safeArea;
        float scaleX = canvasSize.x / Screen.width;
        float scaleY = canvasSize.y / Screen.height;

        return new Vector2(
            safeArea.width * scaleX,
            safeArea.height * scaleY
        );
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (_rectTransform == null) _rectTransform = GetComponent<RectTransform>();
        UpdateUI();
    }
#endif
}