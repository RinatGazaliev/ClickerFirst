using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Для работы с UI кнопками
using DG.Tweening;

public class CrystalCollection : MonoBehaviour
{
    private int multiplierKF = 5;
    [SerializeField] private Button triggerButton;
    [SerializeField] private GameObject PileOfCrystalParent;
    [SerializeField] private ParticleSystem vfxEffect;
    [SerializeField] private Transform targetObject;
    [SerializeField] private Text txtValueReward;
    [SerializeField] private Image targetImage;
    [SerializeField] private SpriteRenderer targetSprite;
    [SerializeField] private Vector3[] InitialPos1;
    [SerializeField] private Quaternion[] InitialRotation1;
    [SerializeField] private Vector3 FinalPositionVert;
    [SerializeField] private Vector3 FinalPositionHor;
    [SerializeField] private int CrystalNo;

    [SerializeField] private Transform scaleTarget;
    private static Transform scaleTargetGlobal;

    private int currRewValue;
    private Vector3 endWorldPos;
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
        FindScaleTarget();

        if (triggerButton != null)
        {
            triggerButton.onClick.AddListener(() =>
            {
                triggerButton.interactable = false;
                StartScalingEffect();
            });
        }
        else
        {
            Debug.LogWarning("Кнопка не назначена в инспекторе!");
        }
    }

    private void FindScaleTarget()
    {
        if (scaleTargetGlobal == null)
        {
            Transform canvas = GameObject.Find("Canvas_UI")?.transform;
            if (canvas != null)
            {
                Transform scoreZone = canvas.Find("ScoreZone");
                if (scoreZone != null)
                {
                    scaleTargetGlobal = scoreZone.Find("SausagoidsNumber");
                    if (scaleTargetGlobal != null)
                        Debug.Log($"✅ Найден объект SausagoidsNumber: {scaleTargetGlobal.name}");
                    else
                        Debug.LogWarning("❌ Не найден объект 'SausagoidsNumber' внутри ScoreZone!");
                }
                else
                {
                    Debug.LogWarning("❌ Не найден объект 'ScoreZone' внутри Canvas_UI!");
                }
            }
            else
            {
                Debug.LogWarning("❌ Не найден Canvas_UI!");
            }
        }
    }

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

    private void StartTextAnimation()
    {
        if (txtValueReward != null)
        {
            Vector3 startPos = txtValueReward.transform.position;
            Vector3 moveDirection = (endWorldPos - startPos).normalized * 100f;

            txtValueReward.transform.DOScale(1.5f, 0.5f)
                .SetEase(Ease.OutElastic);

            txtValueReward.transform.DOMove(startPos + moveDirection, 0.5f, false)
                .SetEase(Ease.OutExpo);

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
                    ScaleObject();
                    crystal.gameObject.SetActive(false);
                });

            delay += 0.1f;
        }
        Config.SetTotalScore(Config.GetTotalScore() + currRewValue);
    }

    private void ScaleObject()
    {
        Transform target = scaleTarget != null ? scaleTarget : scaleTargetGlobal;

        if (target != null)
        {
            target.DOKill();
            target.localScale = Vector3.one;

            target.DOScale(1.2f, 0.1f)
                .SetEase(Ease.OutQuad)
                .OnComplete(() =>
                {
                    target.DOScale(1f, 0.1f).SetEase(Ease.InQuad);
                });
        }
        else
        {
            Debug.LogWarning("❌ scaleTarget не найден!");
        }
    }

    public void SetStartPositionCryst()
    {
        float aspect = (float)Screen.width / Screen.height;
        endWorldPos = (aspect < 1) ? FinalPositionVert : FinalPositionHor;
    }
}
