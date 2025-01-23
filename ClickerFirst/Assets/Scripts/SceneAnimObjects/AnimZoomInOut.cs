using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AnimZoomInOut : MonoBehaviour
{
    [SerializeField] private float scaleMultiplier = 1.5f;  // Во сколько раз увеличить масштаб
    [SerializeField] private float scaleDuration = 1.0f; 
    [SerializeField] private float startDelay = 0.5f;

    private Vector3 currScale;
    // Start is called before the first frame update
    void Start()
    {
        currScale = transform.localScale;
        StartScaleAnimation();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void StartScaleAnimation()
    {
        float endScale = currScale.x * scaleMultiplier;
        // Анимация изменения масштаба
        transform.DOScale(endScale, scaleDuration)
            .SetDelay(startDelay)
            .SetEase(Ease.InOutSine)
            .SetLoops(-1, LoopType.Yoyo);
    }
}
