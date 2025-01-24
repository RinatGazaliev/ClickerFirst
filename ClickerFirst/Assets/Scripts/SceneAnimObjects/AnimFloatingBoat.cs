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
        Vector3 startRotation = transform.localEulerAngles;
        startRotation.z = startRotation.z - rotationAmplitude;
        transform.localEulerAngles = startRotation;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FloatAnim()
    {
        // Анимация вертикального движения (вверх-вниз)
        transform.DOLocalMoveY(transform.localPosition.y + verticalAmplitude, verticalDuration)
            .SetDelay(startDelay)
            .SetEase(Ease.InOutSine)
            .SetLoops(-1, LoopType.Yoyo);

        // Анимация вращения (влево-вправо)
       
        float startRotationZ = transform.localEulerAngles.z; // Сохраняем начальный угол
        //LoopRotation(startRotationZ);
        transform.DOLocalRotate(new Vector3(0, 0, rotationAmplitude*2), rotationDuration, RotateMode.LocalAxisAdd)
            .SetEase(Ease.InOutSine)
            .SetLoops(-1, LoopType.Yoyo);





    }

    private void LoopRotation(float _startRotationZ)
    {
        Debug.Log("Rotation"+_startRotationZ);
        transform.DOLocalRotate(new Vector3(0, 0, rotationAmplitude*2), rotationDuration / 4, RotateMode.LocalAxisAdd)
            .SetEase(Ease.InOutSine)
            .OnComplete(() =>
            {
                transform.DOLocalRotate(new Vector3(0, 0, _startRotationZ), rotationDuration / 4, RotateMode.FastBeyond360)
                    .SetEase(Ease.InOutSine)
                    .OnComplete(() =>
                    {
                        transform.DOLocalRotate(new Vector3(0, 0, -rotationAmplitude), rotationDuration / 4,
                                RotateMode.LocalAxisAdd)
                            .SetEase(Ease.InOutSine)
                            .OnComplete(() =>
                            {
                                transform.DOLocalRotate(new Vector3(0, 0, _startRotationZ), rotationDuration / 4,
                                        RotateMode.FastBeyond360)
                                    .SetEase(Ease.InOutSine)
                                    .OnComplete(() =>
                                    {
                                        LoopRotation(_startRotationZ);

                                    });

                            });

                    });
            });
    }
}
