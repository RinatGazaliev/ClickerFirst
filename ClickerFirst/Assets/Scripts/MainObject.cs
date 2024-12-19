using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using System;
using UnityEngine;

public class MainObject : MonoBehaviour, IPointerClickHandler
{
    
    // Событие, на которое могут подписаться другие объекты
    public static event Action <GameObject> OnObjectClicked;

    public void  OnMouseDown()
    {
        // Вызываем событие и передаем объект, на который кликнули
        OnObjectClicked?.Invoke(gameObject);
       // Debug.Log("ConfiScorePerClickKF"+Config.GetPerClickScaleKf());
    }// Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnPointerClick()
    {
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("MainObjClicked");
        OnObjectClicked?.Invoke(gameObject);
        
    }

    [Header("Head")] 
    [SerializeField] private GameObject HeadBone;
    [SerializeField] private GameObject HeadObject;
    [SerializeField] private List<Sprite> HeadSprites;
    [SerializeField] private List<Vector3> HeadScales; // Масштабы
    [SerializeField] private List<Vector3> HeadPositions; // Позиции
    [SerializeField] private List<Quaternion> HeadRotations; // Повороты
    [SerializeField] int CurrentIndex;
    private void OnValidate()
    {
        // Обновляем визуализацию при изменении CurrentIndex
        UpdateVisualization();
    }

    private void UpdateVisualization()
    {
        // Проверяем корректность индекса
        if (HeadSprites == null || HeadSprites.Count <= CurrentIndex ||
            HeadScales == null || HeadScales.Count <= CurrentIndex ||
            HeadPositions == null || HeadPositions.Count <= CurrentIndex ||
            HeadRotations == null || HeadRotations.Count <= CurrentIndex)
        {
            Debug.LogWarning("Индекс выходит за пределы списка или списки не заданы.");
            return;
        }

        // Устанавливаем спрайт для HeadObject
        if (HeadObject != null)
        {
            var spriteRenderer = HeadObject.GetComponent<SpriteRenderer>();
            if (spriteRenderer == null)
            {
                spriteRenderer = HeadObject.AddComponent<SpriteRenderer>();
            }
            spriteRenderer.sprite = HeadSprites[CurrentIndex];
        }

        // Настройка трансформации для HeadBone
        if (HeadBone != null)
        {
            HeadBone.transform.localScale = HeadScales[CurrentIndex];
            HeadBone.transform.localPosition = HeadPositions[CurrentIndex];
            HeadBone.transform.rotation = HeadRotations[CurrentIndex];
        }
    }

}
