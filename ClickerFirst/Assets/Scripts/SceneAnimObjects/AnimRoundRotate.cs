using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AnimRoundRotate : MonoBehaviour
{
    [SerializeField] private float fullAnimCycleTime = 360.0f; // Время в сек на 1 оборот
    [SerializeField] private float startDelay = 0.5f;       // Задержка перед началом вращения

    private void Start()
    {
        StartRotation();
    }

    private void StartRotation()
    {
        // Анимация вращения по часовой стрелке
        transform.DORotate(new Vector3(0, 0, -360), fullAnimCycleTime, RotateMode.FastBeyond360)
            .SetDelay(startDelay)
            .SetEase(Ease.Linear)
            .SetLoops(-1, LoopType.Restart);
    }
}
