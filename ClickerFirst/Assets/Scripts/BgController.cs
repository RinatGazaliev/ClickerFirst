using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgController : MonoBehaviour
{
    public GameObject gradientPrefab; // Префаб градиента
    public RectTransform canvas; // Канвас
    public float moveSpeed = 50f; // Скорость движения
    public int gradientCount = 10; // Количество градиентов
    private List<GameObject> gradients = new List<GameObject>(); // Список градиентов
    private float gradientHeight; // Высота градиента
    private bool isMoving = false; // Флаг для контроля движения

    // Start is called before the first frame update
    void Start()
    {
        // Получаем высоту градиента
        gradientHeight = gradientPrefab.GetComponent<RectTransform>().rect.height;

        // Создаем градиенты
        for (int i = 0; i < gradientCount; i++)
        {
            GameObject gradient = Instantiate(gradientPrefab, canvas); // Создаем объект из префаба
            gradient.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, i * gradientHeight);
            gradients.Add(gradient);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            MoveBackground();
        }
    }

    public void StartMoving()
    {
        isMoving = true;
    }

    public void StopMoving()
    {
        isMoving = false;
    }

    private void MoveBackground()
    {
        // Смещаем градиенты вниз
        foreach (GameObject gradient in gradients)
        {
            RectTransform rect = gradient.GetComponent<RectTransform>();
            rect.anchoredPosition += Vector2.down * moveSpeed * Time.deltaTime;

            // Если градиент вышел за пределы экрана, перемещаем его наверх
            if (rect.anchoredPosition.y < -gradientHeight)
            {
                rect.anchoredPosition += new Vector2(0, gradientHeight * gradientCount);
                UpdateGradient(gradient); // Обновляем спрайт градиента
            }
        }
    }

    private void UpdateGradient(GameObject gradient)
    {
        // Здесь вы можете обновить спрайт градиента
        var image = gradient.GetComponent<UnityEngine.UI.Image>();
        // image.sprite = ...; // Добавьте сюда логику выбора нового спрайта
    }
}
