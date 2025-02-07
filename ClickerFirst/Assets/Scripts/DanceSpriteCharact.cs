using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DanceSpriteCharact : MonoBehaviour
{
    public Image imageComponent; // Ссылка на Image
    public Sprite sprite1; // Первый спрайт
    public Sprite sprite2; // Второй спрайт
    public float switchInterval = 0.5f; // Интервал между сменами спрайтов

    private bool isSprite1Active = true;
    private Coroutine animationCoroutine;

    private void OnEnable()
    {
        StartAnimation(); // Запускаем анимацию при включении объекта (например, при открытии виджета)
    }

    private void OnDisable()
    {
        StopAnimation(); // Останавливаем анимацию при закрытии виджета
    }

    private void StartAnimation()
    {
        if (animationCoroutine != null)
        {
            StopCoroutine(animationCoroutine); // Останавливаем предыдущую корутину (если есть)
        }
        animationCoroutine = StartCoroutine(SwitchSprites());
    }

    private void StopAnimation()
    {
        if (animationCoroutine != null)
        {
            StopCoroutine(animationCoroutine);
            animationCoroutine = null;
        }
    }

    IEnumerator SwitchSprites()
    {
        while (true)
        {
            imageComponent.sprite = isSprite1Active ? sprite2 : sprite1;
            isSprite1Active = !isSprite1Active;

            yield return new WaitForSecondsRealtime(switchInterval);
        }
    }
}
