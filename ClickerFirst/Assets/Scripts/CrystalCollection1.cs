using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Для работы с UI кнопками
using DG.Tweening;

public class CrystalCollection : MonoBehaviour
{
    private int multiplierKF = 5;
    [SerializeField] private Button triggerButton; // Кнопка для запуска анимации
    [SerializeField] private GameObject PileOfCrystalParent;
    [SerializeField] private ParticleSystem vfxEffect; // 🎇 Ссылка на VFX
    [SerializeField] private Transform targetObject; // 🎯 Объект, который увеличивается
    [SerializeField] private Text txtValueReward; // 🔤 Текст награды
    [SerializeField] private Image targetImage;
    [SerializeField] private SpriteRenderer targetSprite;
    [SerializeField] private Vector3[] InitialPos1;
    [SerializeField] private Quaternion[] InitialRotation1;
    [SerializeField] private Vector3 FinalPositionVert;
    [SerializeField] private Vector3 FinalPositionHor;
    [SerializeField] private int CrystalNo;

    private int currRewValue;
    private Vector3 endWorldPos; // Конечная точка в мировых координатах
    public Ease moveEase;

    void Start()
    {
        currRewValue = Config.GetScorePerClick() * multiplierKF;
        txtValueReward.text = currRewValue.ToString();
        InitialPos1 = new Vector3[CrystalNo];
        InitialRotation1 = new Quaternion[CrystalNo];

        for (int i = 0; i < PileOfCrystalParent.transform.childCount; i++)
        {
            InitialPos1[i] = PileOfCrystalParent.transform.GetChild(i).localPosition;
            InitialRotation1[i] = PileOfCrystalParent.transform.GetChild(i).rotation;
        }

        SetStartPositionCryst();

        // ✅ Привязываем кнопку и блокируем после нажатия
        if (triggerButton != null)
        {
            triggerButton.onClick.AddListener(() =>
            {
                triggerButton.interactable = false; // ❌ Отключаем кнопку сразу после нажатия
                StartScalingEffect();
            });
        }
        else
        {
            Debug.LogWarning("Кнопка не назначена в инспекторе!");
        }
    }

    /// 🔹 1. Увеличение объекта, затем исчезновение
    private void StartScalingEffect()
    {
        if (targetObject != null)
        {
            targetObject.DOScale(targetObject.localScale * 2.0f, 0.35f)
                .SetEase(Ease.OutQuad)
                .OnComplete(() =>
                {
                    StartFadeEffect();
                });
        }
        else
        {
            Debug.LogWarning("Target Object не назначен в инспекторе!");
        }
    }

    /// 🔹 2. Исчезновение объекта, затем параллельно запустить анимации текста и монет
    private void StartFadeEffect()
    {
        if (targetImage != null)
        {
            targetImage.DOFade(0, 0.2f)
                .SetEase(Ease.InExpo)
                .OnComplete(() =>
                {
                    targetObject.gameObject.SetActive(false);
                    StartEffects();
                });
        }
        else if (targetSprite != null)
        {
            targetSprite.DOFade(0, 0.2f)
                .SetEase(Ease.InExpo)
                .OnComplete(() =>
                {
                    targetObject.gameObject.SetActive(false);
                    StartEffects();
                });
        }
        else
        {
            targetObject.gameObject.SetActive(false);
            StartEffects();
        }
    }

    /// 🔹 3. Запуск VFX, анимации монет и текста одновременно
    private void StartEffects()
    {
        if (vfxEffect != null)
        {
            vfxEffect.Play();
        }
        else
        {
            Debug.LogWarning("VFX не назначен в инспекторе!");
        }

        RewardPileOfCrystal();
        StartTextAnimation();
    }

    /// 🔹 4. Анимация текста: увеличение + движение в сторону монет + исчезновение
    private void StartTextAnimation()
    {
        if (txtValueReward != null)
        {
            Vector3 startPos = txtValueReward.transform.position;
            Vector3 moveDirection = (endWorldPos - startPos).normalized * 100f; // Двигаем текст в том же направлении, что и монеты

            // 🟢 Увеличение + движение
            txtValueReward.transform.DOScale(1.5f, 0.5f)
                .SetEase(Ease.OutElastic);

            txtValueReward.transform.DOMove(startPos + moveDirection, 0.5f, false)
                .SetEase(Ease.OutExpo);

            // 🔥 Плавное исчезновение
            txtValueReward.DOFade(0, 0.4f)
                .SetEase(Ease.InExpo)
                .OnComplete(() =>
                {
                    txtValueReward.gameObject.SetActive(false);
                });
        }
        else
        {
            Debug.LogWarning("Text Value Reward не назначен!");
        }
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
            int index = i;
            Transform crystal = PileOfCrystalParent.transform.GetChild(index);

            crystal.DOScale(1f, 0.1f)
                .SetDelay(delay)
                .SetEase(Ease.OutBack);

            crystal.DORotate(Vector3.zero, 0.3f)
                .SetDelay(delay + 0.05f)
                .SetEase(Ease.Flash);

            crystal.DOMove(endWorldPos, 0.5f)
                .SetDelay(delay + 0.2f)
                .SetEase(moveEase)
                .OnComplete(() =>
                {
                    crystal.gameObject.SetActive(false);
                    Config.SetTotalScore(Config.GetTotalScore() + currRewValue);
                });

            delay += 0.1f;
        }
    }

    public void SetStartPositionCryst()
    {
        float aspect = (float)Screen.width / Screen.height;
        endWorldPos = (aspect < 1) ? FinalPositionVert : FinalPositionHor;
    }
}
