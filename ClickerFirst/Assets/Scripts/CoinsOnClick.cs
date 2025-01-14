using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class CoinsOnClick : MonoBehaviour
{
    public GameObject coinPrefab;  // Префаб монеты
    public Transform coinsParent;  // Родительский объект для монет
    public int coinCount = 10;     // Количество монет
    //public float explosionForce = 5f;  // Сила выброса монет
    //public float gravity = -9.8f;      // Гравитационное ускорение
   // public float duration = 2f; 

    private void OnEnable()
    {
        MainObject.OnObjectClicked += ExplodeCoins;
        
    }

    private void OnDisable()
    {
        MainObject.OnObjectClicked -= ExplodeCoins;
    }

    void ExplodeCoins(GameObject clickedObject)
    {
        for (int i = 0; i < coinCount; i++)
        {
            GameObject coin = Instantiate(coinPrefab, transform.position, Quaternion.identity, coinsParent);

            Vector3 startPosition = transform.position;

            // Определяем случайное направление разлета влево или вправо
            bool isLeftSide = i % 2 == 0;
            float horizontalDistance = Random.Range(1f, 3f) * (isLeftSide ? -1f : 1f)*250;  // Отрицательная для левой стороны
            float arcHeight = Random.Range(2f, 4f)*100;  // Высота дуги
            float endY = Random.Range(-0.5f, 0)*1000;  // Рандомный Y для имитации утончения листа

            // Конечная позиция с разлетом и рандомной высотой завершения
            Vector3 endPosition = startPosition + new Vector3(horizontalDistance, endY, 0f);

            Sequence coinSequence = DOTween.Sequence();

            // Анимация траектории листа пальмы
            coinSequence.Append(DOTween.To(
                () => startPosition,
                pos => coin.transform.position = CalculatePalmLeafTrajectory(startPosition, endPosition, arcHeight, pos),
                endPosition,
                duration: 1f).SetEase(Ease.OutQuad));

            // Уменьшение масштаба для плавного исчезновения
            coinSequence.Join(coin.transform.DOScale(Vector3.zero, 0.5f).SetDelay(0.5f));

            coinSequence.OnComplete(() => Destroy(coin));
        }
    }

    Vector3 CalculatePalmLeafTrajectory(Vector3 start, Vector3 end, float height, Vector3 currentPosition)
    {
        float progress = (currentPosition - start).magnitude / (end - start).magnitude;  // Прогресс от 0 до 1
        float y = Mathf.Lerp(start.y, end.y, progress) + Mathf.Sin(progress * Mathf.PI) * height;  // Подъем и спад по дуге
        return new Vector3(Mathf.Lerp(start.x, end.x, progress), y, start.z);  // Ось Z остается фиксированной
    }
}
