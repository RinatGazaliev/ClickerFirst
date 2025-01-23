using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AnimRoundRotate : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 360.0f; // Скорость вращения (градусы в секунду)
    [SerializeField] private float startDelay = 0.5f;       // Задержка перед началом вращения

    private void Start()
    {
        StartRotation();
    }

    private void StartRotation()
    {
        // Анимация вращения по часовой стрелке
        transform.DORotate(new Vector3(0, 0, -360), rotationSpeed, RotateMode.FastBeyond360)
            .SetDelay(startDelay)
            .SetEase(Ease.Linear)
            .SetLoops(-1, LoopType.Restart);
    }
}
