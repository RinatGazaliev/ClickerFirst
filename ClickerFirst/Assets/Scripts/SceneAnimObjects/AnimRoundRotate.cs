using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AnimRoundRotate : MonoBehaviour
{
    [SerializeField] private float fullAnimCycleTime = 360.0f; // Время в сек на 1 оборот
    [SerializeField] private float startDelay = 0.5f; // Задержка перед началом вращения 
    public static float moveSpeed = 10f; // Скорость движения
    public float moveDistance = 5f; // Дистанция одного шага
    private Tween moveTween;
    private void Start()
    {
        StartRotation();
        StartMoving();
    }

    private void StartRotation()
    {
        // Анимация вращения по часовой стрелке
        transform.DORotate(new Vector3(0, 0, -360), fullAnimCycleTime, RotateMode.FastBeyond360)
            .SetDelay(startDelay)
            .SetEase(Ease.Linear)
            .SetLoops(-1, LoopType.Incremental);
    }
    public void StartMoving()
    {
        // Если анимация уже запущена, останавливаем её
        moveTween?.Kill();

        // Бесконечное движение в локальных координатах
        moveTween = transform.DOLocalMoveX(transform.localPosition.x - moveDistance, moveDistance / moveSpeed)
            .SetEase(Ease.Linear)
            .OnComplete(() => StartMoving()); // После завершения снова запускаем движение
    }

    public void UpdateSpeed(float newSpeed)
    {
        moveSpeed = newSpeed;
        StartMoving(); // Перезапускаем с новой скоростью
    }

    private void OnDestroy()
    {
        moveTween?.Kill(); // Убиваем анимацию при уничтожении объекта
    }
}
