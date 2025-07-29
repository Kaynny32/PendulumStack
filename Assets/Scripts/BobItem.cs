using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

public class BobItem : MonoBehaviour
{
    [SerializeField] string nameBob;

    [Header("Settings")]
    [SerializeField]
    RectTransform chainEndPoint;
    [SerializeField]
    float followSpeed = 10f;
    [SerializeField]
    float dropGravityScale = 5f;
    [SerializeField]
    float initialDropSpeed = 2f;
    [SerializeField]
    float maxDropSpeed = 15f;
    [SerializeField]
    bool _isFollowing = true;

    RectTransform _rectTransform;
    Rigidbody2D _rb;    
    Vector2 _followVelocity;
    bool _shouldApplyPhysics;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _rb = GetComponent<Rigidbody2D>();
        _rb.simulated = false;
        _rb.gravityScale = 0;
    }

    private void Update()
    {
        if (_isFollowing)
        {
            FollowChainPoint();
        }
        else if (!_shouldApplyPhysics)
        {
            _shouldApplyPhysics = true;
        }
    }

    private void FixedUpdate()
    {
        if (_shouldApplyPhysics)
        {
            ApplyPhysics();
            _shouldApplyPhysics = false;
        }
        if (gameObject.GetComponent<RectTransform>().anchoredPosition.y <-1300)
        {
            Destroy(gameObject);
        }
    }

    private void FollowChainPoint()
    {
        if (chainEndPoint == null) return;

        Vector2 targetPosition = chainEndPoint.TransformPoint(chainEndPoint.rect.center);

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            _rectTransform.parent as RectTransform,
            RectTransformUtility.WorldToScreenPoint(null, targetPosition),
            null,
            out targetPosition
        );

        _rectTransform.anchoredPosition = Vector2.SmoothDamp(
            _rectTransform.anchoredPosition,
            targetPosition,
            ref _followVelocity,
            followSpeed * Time.deltaTime
        );
    }

    private void ApplyPhysics()
    {
        _rb.simulated = true;
        _rb.gravityScale = dropGravityScale;

        _rb.position = _rectTransform.position;

        _rb.velocity = Vector2.down * initialDropSpeed;

        //enabled = false;
    }

    public void SetFollow(bool follow)
    {
        if (_isFollowing == follow) 
            return;

        _isFollowing = follow;

        if (!follow)
        {
            _shouldApplyPhysics = true;
        }
        else
        {
           // enabled = true;
            _rb.simulated = false;
            _rb.gravityScale = 0;
            _rb.velocity = Vector2.zero;
            _followVelocity = Vector2.zero;
        }
    }

    public string GetBobName()
    {
        return nameBob;
    }

    public void SetIsFolowing(bool flow)
    {
        _isFollowing = flow;
    }

    public bool GetIsFolowing()
    {
        return _isFollowing;
    }

    public void SetTargeRectransform(RectTransform target)
    {
        chainEndPoint = target;
    }

    public void AnimBobDestroy()
    {
        UniversalAnimator.universalAnimator.Animate(gameObject.GetComponent<RectTransform>(), AnimationTargetType.RectSize, new Vector2(0,0),0,0.5f, Ease.InOutBack);
    }
    
}