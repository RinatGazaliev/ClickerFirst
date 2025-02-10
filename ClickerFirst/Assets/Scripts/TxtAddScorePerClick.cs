using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class TxtAddScorePerClick : MonoBehaviour
{
    private Text txtAddScore;
    private Graphic objectRenderer;
    private float animDuration = 0.5f;

    [SerializeField] private float spawnOffset = 50f; // Разброс спавна в пикселях
    [SerializeField] private float minRotation = -15f; // Минимальный угол поворота
    [SerializeField] private float maxRotation = 15f;  // Максимальный угол поворота

    private static Transform scaleTargetGlobal; // 🟢 Глобальный объект для скейла (SausagoidsNumber)

    void Start()
    {
        txtAddScore = gameObject.GetComponent<Text>();
        int currScoreToAdd = Config.GetScorePerClick() * Config.GetDoublePointsRewValue();

        // Получаем случайный цвет для всего числа
        string randomColor = GetRandomColor();
        txtAddScore.text = $"<color={randomColor}>+{currScoreToAdd}</color>"; // Красим всё число

        objectRenderer = gameObject.GetComponent<Graphic>();

        FindScaleTarget(); // 🟢 Ищем объект "SausagoidsNumber"
        AnimateMoveUpDisappear();
        ScaleObject(); // 🟢 Запускаем анимацию "SausagoidsNumber"
    }

    /// 🔹 **Метод автоматического поиска "SausagoidsNumber"**
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

    /// 🔹 **Метод анимации увеличения "SausagoidsNumber"**
    private void ScaleObject()
    {
        if (scaleTargetGlobal != null)
        {
            scaleTargetGlobal.DOKill(); // Останавливаем текущую анимацию
            scaleTargetGlobal.localScale = Vector3.one; // Сбрасываем масштаб перед новой анимацией

            scaleTargetGlobal.DOScale(1.2f, 0.1f) // Увеличение на 20%
                .SetEase(Ease.OutQuad)
                .OnComplete(() =>
                {
                    scaleTargetGlobal.DOScale(1f, 0.1f).SetEase(Ease.InQuad); // ✅ Возвращение к 1.0
                });
        }
    }

    private string GetRandomColor()
    {
        string[] colors = { "#F3AF5E", "#F38E77", "#F3EC98", "#91CDF3", "#9498F3", "#9CF399", "#F37ADE" };
        return colors[Random.Range(0, colors.Length)];
    }

    private void AnimateMoveUpDisappear()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();

        // Добавляем случайное смещение
        Vector2 randomOffset = new Vector2(
            UnityEngine.Random.Range(-spawnOffset, spawnOffset),
            UnityEngine.Random.Range(-spawnOffset, spawnOffset)
        );
        rectTransform.anchoredPosition += randomOffset;

        // Добавляем случайный угол поворота
        float randomRotation = UnityEngine.Random.Range(minRotation, maxRotation);
        transform.rotation = Quaternion.Euler(0, 0, randomRotation);

        // Делаем начальный размер 0
        transform.localScale = Vector3.zero;

        // Анимация увеличения (0 -> 1), затем уменьшения (1 -> 0)
        transform.DOScale(1, animDuration * 0.5f) // Увеличение
            .OnComplete(() =>
                transform.DOScale(0, animDuration * 0.5f) // Уменьшение
                    .OnComplete(DestroyObject) // Уничтожение после анимации
            );

        // Двигаем текст вверх
        Vector2 endValue = rectTransform.anchoredPosition;
        endValue.y += 500; // Высота подъёма
        rectTransform.DOAnchorPos(endValue, animDuration);

        // Исчезновение текста
        objectRenderer.DOFade(0, animDuration);
    }

    private void DestroyObject()
    {
        Destroy(gameObject);
    }
}
