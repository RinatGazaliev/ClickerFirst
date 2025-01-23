using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AnimMovingUp : MonoBehaviour
{
    [SerializeField] private float speed = 5f; // Задается в инспекторе скорость движения
    [SerializeField] private float delay = 1f; // Задержка перед началом движения

    private bool isMoving = false;

    void Start()
    {
        // Включаем движение с задержкой
        Invoke(nameof(StartMoving), delay);
    }

    void Update()
    {
        if (isMoving)
        {
            // Двигаем объект вверх в локальных координатах
            transform.Translate(Vector3.up * speed * Time.deltaTime, Space.Self);
        }
    }

    void StartMoving()
    {
        isMoving = true; // Запускаем движение
    }
}
