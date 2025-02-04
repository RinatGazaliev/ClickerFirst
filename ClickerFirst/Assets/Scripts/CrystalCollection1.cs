using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Для работы с UI кнопками
using DG.Tweening;

public class CrystalCollection : MonoBehaviour
{
    private int multiplierKF;
    [SerializeField] private Button triggerButton; // Кнопка для запуска анимации
    [SerializeField] private GameObject PileOfCrystalParent;
    [SerializeField] private ParticleSystem vfxEffect; // 🎇 Ссылка на VFX
    [SerializeField] private Transform targetObject; // 🎯 Объект, который увеличивается
    [SerializeField] private Image targetImage; // 🎭 UI-элемент для Fading (если это UI)
    [SerializeField] private SpriteRenderer targetSprite; // 🖼 Спрайт для Fading (если это 2D)
    [SerializeField] private Vector3[] InitialPos1;
    [SerializeField] private Quaternion[] InitialRotation1;
    [SerializeField] private Vector3 FinalPositionVert; // Теперь в мировых координатах
    [SerializeField] private Vector3 FinalPositionHor;  // Теперь в мировых координатах
    [SerializeField] private int CrystalNo;
    [SerializeField] private Text txtValueReward;
    private int currRewValue;

    private Vector3 endWorldPos; // Конечная точка в мировых координатах
    public Ease moveEase;

    void Start()
    {
        multiplierKF = Random.Range(1,5);
        currRewValue = Config.GetScorePerClick() * multiplierKF;
        txtValueReward.text = currRewValue.ToString();
        InitialPos1 = new Vector3[CrystalNo];
        InitialRotation1 = new Quaternion[CrystalNo];

        for (int i = 0; i < PileOfCrystalParent.transform.childCount; i++)
        {
            InitialPos1[i] = PileOfCrystalParent.transform.GetChild(i).localPosition;
            InitialRotation1[i] = PileOfCrystalParent.transform.GetChild(i).rotation;
        }

        SetStartPositionCryst(); // Устанавливаем целевую точку при старте

        // ✅ Привязываем кнопку и нажатие
        if (triggerButton != null)
        {
            triggerButton.onClick.AddListener(() => StartScalingEffect());
        }
        else
        {
            Debug.LogWarning("Кнопка не назначена в инспекторе!");
        }
    }

    /// 🔹 1. Увеличение объекта и его исчезновение
    private void StartScalingEffect()
    {
        if (targetObject != null)
        {
            targetObject.DOScale(targetObject.localScale * 2.0f, 0.35f) // Увеличиваем в 2 раза за 0.35 сек
                .SetEase(Ease.OutQuad)
                .OnComplete(() =>
                {
                    // 🔥 Начинаем анимацию исчезновения
                    StartFadeEffect();
                });
        }
        else
        {
            Debug.LogWarning("Target Object не назначен в инспекторе!");
        }
    }

    /// 🔹 2. Плавное исчезновение объекта, затем запуск VFX и монет
    private void StartFadeEffect()
    {
        if (targetImage != null) // Если объект - UI Image
        {
            targetImage.DOFade(0, 0.2f)
                .SetEase(Ease.InExpo)
                .OnComplete(() =>
                {
                    targetObject.gameObject.SetActive(false);
                    StartEffects(); // После исчезновения запускаем VFX + кристаллы
                });
        }
        else if (targetSprite != null) // Если объект - SpriteRenderer (2D)
        {
            targetSprite.DOFade(0, 0.2f)
                .SetEase(Ease.InExpo)
                .OnComplete(() =>
                {
                    targetObject.gameObject.SetActive(false);
                    StartEffects(); // После исчезновения запускаем VFX + кристаллы
                });
        }
        else
        {
            // Если у объекта нет изображения, просто отключаем его и продолжаем
            targetObject.gameObject.SetActive(false);
            StartEffects();
        }
    }

    /// 🔹 3. Запуск VFX и анимации монет
    private void StartEffects()
    {
        // 🎇 Запускаем VFX (если он назначен)
        if (vfxEffect != null)
        {
            vfxEffect.Play();
        }
        else
        {
            Debug.LogWarning("VFX не назначен в инспекторе!");
        }

        // 💎 Запускаем анимацию кристаллов
        RewardPileOfCrystal();
    }

    private void Reset1()
    {
        for (int i = 0; i < PileOfCrystalParent.transform.childCount; i++)
        {
            PileOfCrystalParent.transform.GetChild(i).localPosition = InitialPos1[i];
            PileOfCrystalParent.transform.GetChild(i).rotation = InitialRotation1[i];
        }
    }

    public void RewardPileOfCrystal()
    {
        Reset1();
        float delay = 0f;
        PileOfCrystalParent.SetActive(true);

        for (int i = 0; i < PileOfCrystalParent.transform.childCount; i++)
        {
            int index = i; // Захватываем текущий индекс
            Transform crystal = PileOfCrystalParent.transform.GetChild(index);

            // Анимация масштабирования (начало)
            crystal.DOScale(1f, 0.1f)
                .SetDelay(delay)
                .SetEase(Ease.OutBack);

            // Вращение
            crystal.DORotate(Vector3.zero, 0.3f)
                .SetDelay(delay + 0.05f)
                .SetEase(Ease.Flash);

            // 🟢 Перемещение в правильные мировые координаты
            crystal.DOMove(endWorldPos, 0.5f)
                .SetDelay(delay + 0.2f)
                .SetEase(moveEase)
                .OnComplete(() =>
                {
                    crystal.gameObject.SetActive(false);
                    
                });

            delay += 0.1f;
        }
        Config.SetTotalScore(Config.GetTotalScore()+currRewValue);
    }

    public void SetStartPositionCryst()
    {
        float aspect = (float)Screen.width / Screen.height;
        endWorldPos = (aspect < 1) ? FinalPositionVert : FinalPositionHor;
    }
}
