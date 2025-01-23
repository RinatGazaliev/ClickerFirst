using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AnimFloatingBoat : MonoBehaviour
{
    [SerializeField] private float verticalAmplitude = 1.0f; // Амплитуда движения вверх-вниз
    [SerializeField] private float verticalDuration = 2.0f;  // Время на один цикл движения вверх-вниз
    [SerializeField] private float rotationAmplitude = 15.0f; // Амплитуда вращения (градусы)
    [SerializeField] private float rotationDuration = 2.0f;  // Время на один цикл вращения
    [SerializeField] private float startDelay = 0.5f;        // Задержка перед запуском анимации
    // Start is called before the first frame update
    void Start()
    {
        FloatAnim();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FloatAnim()
    {
        // Анимация вертикального движения (вверх-вниз)
        transform.DOMoveY(transform.position.y + verticalAmplitude, verticalDuration)
            .SetDelay(startDelay)
            .SetEase(Ease.InOutSine)
            .SetLoops(-1, LoopType.Yoyo);

        // Анимация вращения (влево-вправо)
        transform.DORotate(new Vector3(0, 0, rotationAmplitude), rotationDuration, RotateMode.LocalAxisAdd)
            .SetDelay(startDelay)
            .SetEase(Ease.InOutSine)
            .SetLoops(-1, LoopType.Yoyo);  
    }
}
