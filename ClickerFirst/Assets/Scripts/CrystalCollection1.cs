using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CrystalCollection : MonoBehaviour
{
    [SerializeField] private GameObject PileOfCrystalParent;
    [SerializeField] private Vector3[] InitialPos1;
    [SerializeField] private Quaternion[] InitialRotation1;
    [SerializeField] private Vector3 FinalPositionVert; // Теперь в мировых координатах
    [SerializeField] private Vector3 FinalPositionHor;  // Теперь в мировых координатах
    [SerializeField] private int CrystalNo;
    private Vector3 endWorldPos; // Конечная точка в мировых координатах
    public Ease moveEase;

    void Start()
    {
        InitialPos1 = new Vector3[CrystalNo];
        InitialRotation1 = new Quaternion[CrystalNo];

        for (int i = 0; i < PileOfCrystalParent.transform.childCount; i++)
        {
            InitialPos1[i] = PileOfCrystalParent.transform.GetChild(i).localPosition;
            InitialRotation1[i] = PileOfCrystalParent.transform.GetChild(i).rotation;
            Debug.Log("Initial Position of Crystal " + i + ": " + InitialPos1[i]);
        }

        SetStartPositionCryst(); // Устанавливаем целевую точку при старте
    }

    private void Reset1()
    {
        for (int i = 0; i < PileOfCrystalParent.transform.childCount; i++)
        {
            PileOfCrystalParent.transform.GetChild(i).localPosition = InitialPos1[i];
            PileOfCrystalParent.transform.GetChild(i).rotation = InitialRotation1[i];
        }
    }

    public void RewardPileOfCrystal(int NoCrystals)
    {
        Reset1();
        float delay = 0f;
        PileOfCrystalParent.SetActive(true);

        for (int i = 0; i < PileOfCrystalParent.transform.childCount; i++)
        {
            int index = i; // Захватываем текущий индекс

            // Анимация масштабирования
            PileOfCrystalParent.transform.GetChild(index).DOScale(1f, 0.1f)
                .SetDelay(delay)
                .SetEase(Ease.OutBack);

            // Воспроизводим звук
            DOVirtual.DelayedCall(delay + 0.05f, () =>
            {
                //SoundManager.instance.PlaySound_CrystalMove();
            });

            // Вращение
            PileOfCrystalParent.transform.GetChild(index).DORotate(Vector3.zero, 0.3f)
                .SetDelay(delay + 0.05f)
                .SetEase(Ease.Flash);

            // 🟢 Перемещение в правильные мировые координаты
            PileOfCrystalParent.transform.GetChild(index).DOMove(endWorldPos, 0.5f)
                .SetDelay(delay + 0.2f)
                .SetEase(moveEase)
                .OnComplete(() =>
                {
                    PileOfCrystalParent.transform.GetChild(index).gameObject.SetActive(false);
                });

            delay += 0.1f;
        }
    }


    public void SetStartPositionCryst()
    {
        float aspect = (float)Screen.width / Screen.height;

        // Выбираем нужную точку и преобразуем её в мировые координаты
        endWorldPos = (aspect < 1) ? FinalPositionVert : FinalPositionHor;
    }
}
