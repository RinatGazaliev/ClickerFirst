using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AnimZoomInOut : MonoBehaviour
{
    [SerializeField] private float scaleMultiplier = 1.5f;  // Во сколько раз увеличить масштаб
    [SerializeField] private float scaleDuration = 1.0f; 
    [SerializeField] private float startDelay = 0.5f;  
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void StartScaleAnimation()
    {
        // Анимация изменения масштаба
        transform.DOScale(scaleMultiplier, scaleDuration)
            .SetDelay(startDelay)
            .SetEase(Ease.InOutSine)
            .SetLoops(-1, LoopType.Yoyo);
    }
}
