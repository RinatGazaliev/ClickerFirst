using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AnimMovingUp : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float delay;
    // Start is called before the first frame update
    void Start()
    {
        MoveUp();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    // Функция для начала анимации
    public void MoveUp()
    {
        // Рассчитываем конечное положение объекта по оси Y
        float targetY = transform.position.y + speed;

        // Запускаем анимацию перемещения вверх с заданной задержкой
        transform.DOMoveY(targetY, speed).SetDelay(delay).SetEase(Ease.Linear);
    }
}
