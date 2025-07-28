using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pendulum : MonoBehaviour
{
    [SerializeField]
    float _swingAngle = 30f;
    [SerializeField]
    float _swingDuration = 2f;
    [SerializeField]
    Ease _easeType = Ease.InOutSine;

    private void Start()
    {
        //StartSwingAnimation();
    }

    public void StartSwingAnimation()
    {
        transform.DORotate(new Vector3(0, 0, -_swingAngle), _swingDuration / 2).SetEase(_easeType).OnComplete(() =>
        {
            transform.DORotate(new Vector3(0, 0, _swingAngle), _swingDuration)
                .SetEase(_easeType)
                .SetLoops(-1, LoopType.Yoyo);
        });
    }
}
