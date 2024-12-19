using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using System;
using UnityEngine;
using UnityEngine.U2D.Animation;

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
    private void OnValidate()
    {
        // Обновляем визуализацию при изменении CurrentIndex
        // UpdateHeadVisualization();
        UpdateHutGroupVision();
        UpdateGlassesGroupVision();
        UpdateJewerlyGroupVision();
        UpdateArmsGroupVision();
        UpdateLegsGroupVision();
    }
    
    


    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("MainObjClicked");
        OnObjectClicked?.Invoke(gameObject);
        
    }
    
    [Header("Hut")] 
    [SerializeField] private GameObject HutGroup;
    [SerializeField] int CurrentHutIndex;
    [SerializeField] private List<Vector3> HutPositions;
    private void UpdateHutGroupVision()
    {
        HutGroup.GetComponent<SpriteResolver>().SetCategoryAndLabel("Hat", $"Entry_{CurrentHutIndex}");
        HutGroup.transform.localPosition = HutPositions[CurrentHutIndex];

    }
    
    [Header("Glasses")] 
    [SerializeField] private GameObject GlassesGroup;
    [SerializeField] int CurrentGlassesIndex;
    [SerializeField] private List<Vector3> GlassesPositions;
    private void UpdateGlassesGroupVision()
    {
        GlassesGroup.GetComponent<SpriteResolver>().SetCategoryAndLabel("Hat", $"Entry_{CurrentGlassesIndex}");
        GlassesGroup.transform.localPosition = GlassesPositions[CurrentGlassesIndex];

    }
    
    
    [Header("Jewerly")] 
    [SerializeField] private GameObject JewerlyGroup;
    [SerializeField] int CurrentJewerlyIndex;
    [SerializeField] private List<Vector3> JewerlyPositions;
    private void UpdateJewerlyGroupVision()
    {
        JewerlyGroup.GetComponent<SpriteResolver>().SetCategoryAndLabel("Jewerly", $"Entry_{CurrentJewerlyIndex}");
        JewerlyGroup.transform.localPosition = JewerlyPositions[CurrentJewerlyIndex];

    }
    
    
    [Header("Arms")] 
    [SerializeField] private GameObject LeftArmGroup;
    [SerializeField] private GameObject RightArmGroup;
    [SerializeField] int CurrentArmsIndex;
    [SerializeField] private List<Vector3> LeftArmPostion;
    [SerializeField] private List<Vector3> RightArmPostion;
    
    private void UpdateArmsGroupVision()
    {
        LeftArmGroup.GetComponent<SpriteResolver>().SetCategoryAndLabel("LeftArm", $"Entry_{CurrentArmsIndex}");
        RightArmGroup.GetComponent<SpriteResolver>().SetCategoryAndLabel("RightArm", $"Entry_{CurrentArmsIndex}");
        
        LeftArmGroup.transform.localPosition = LeftArmPostion[CurrentArmsIndex];
        RightArmGroup.transform.localPosition = RightArmPostion[CurrentArmsIndex];

    }
    
    [Header("Legs")] 
    [SerializeField] private GameObject LeftLegGroup;
    [SerializeField] private GameObject RightLegGroup;
    [SerializeField] int CurrentLegsIndex;
    [SerializeField] private List<Vector3> LeftLegPostion;
    [SerializeField] private List<Vector3> RightLegPostion;
    
    private void UpdateLegsGroupVision()
    {
        LeftLegGroup.GetComponent<SpriteResolver>().SetCategoryAndLabel("LeftLeg", $"Entry_{CurrentLegsIndex}");
        RightLegGroup.GetComponent<SpriteResolver>().SetCategoryAndLabel("RightLeg", $"Entry_{CurrentLegsIndex}");
        
        LeftLegGroup.transform.localPosition = LeftLegPostion[CurrentLegsIndex];
        RightLegGroup.transform.localPosition = RightLegPostion[CurrentLegsIndex];

    }

  
    //[SerializeField] private GameObject HeadBone;
   // [SerializeField] private GameObject HeadObject;
    //[SerializeField] private List<Sprite> HeadSprites;
    //[SerializeField] private List<Vector3> HeadScales; // Масштабы
    // Позиции
   // [SerializeField] private List<Quaternion> HeadRotations; // Повороты
   


    /*
    private void UpdateHeadVisualization()
    {
        // Проверяем корректность индекса
        if (HeadSprites == null || HeadSprites.Count <= CurrentHeadIndex ||
            HeadScales == null || HeadScales.Count <= CurrentHeadIndex ||
            HeadPositions == null || HeadPositions.Count <= CurrentHeadIndex ||
            HeadRotations == null || HeadRotations.Count <= CurrentHeadIndex)
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

            spriteRenderer.sprite = HeadSprites[CurrentHeadIndex];
        }

        // Настройка трансформации для HeadBone
        if (HeadBone != null)
        {
            HeadObject.transform.localScale = HeadScales[CurrentHeadIndex];
            HeadObject.transform.localPosition = HeadPositions[CurrentHeadIndex];
            HeadObject.transform.rotation = HeadRotations[CurrentHeadIndex];
        }
    }
    */
  
    /*
    [Header("Arms")] 

    [SerializeField] private List<Sprite> ArmsSprites;
    [SerializeField] int CurrentArmsIndex;
    [Header("LeftArm")] 
    [SerializeField] private GameObject LeftArmBone;
    [SerializeField] private GameObject LeftArmObject;
    [SerializeField] private List<Vector3> LeftArmScales; // Масштабы
    [SerializeField] private List<Vector3> LeftArmPositions; // Позиции
    [SerializeField] private List<Quaternion> LeftArmRotations; // Повороты
    
    [Header("RightArm")] 
    [SerializeField] private GameObject RightArmBone;
    [SerializeField] private GameObject RightArmObject;
    [SerializeField] private List<Vector3> RightArmScales; // Масштабы
    [SerializeField] private List<Vector3> RightArmPositions; // Позиции
    [SerializeField] private List<Quaternion> RightArmRotations; // Повороты


    private void UpdateLeftArmVisualization()
    {
        // Проверяем корректность индекса
        if (ArmsSprites == null || ArmsSprites.Count <= CurrentArmsIndex ||
            LeftArmScales == null || LeftArmScales.Count <= CurrentArmsIndex ||
            LeftArmPositions == null || LeftArmPositions.Count <= CurrentArmsIndex ||
            LeftArmRotations == null || LeftArmRotations.Count <= CurrentArmsIndex)
        {
            Debug.LogWarning("Индекс выходит за пределы списка или списки не заданы.");
            return;
        }

        // Устанавливаем спрайт для LeftArmObject
        if (LeftArmObject != null)
        {
            var spriteRenderer = LeftArmObject.GetComponent<SpriteRenderer>();
            if (spriteRenderer == null)
            {
                spriteRenderer = LeftArmObject.AddComponent<SpriteRenderer>();
            }
            spriteRenderer.sprite = ArmsSprites[CurrentArmsIndex];
        }

        // Настройка трансформации для LeftArmBone
        if (LeftArmBone != null)
        {
            LeftArmBone.transform.localScale = LeftArmScales[CurrentArmsIndex];
            LeftArmBone.transform.localPosition = LeftArmPositions[CurrentArmsIndex];
            LeftArmBone.transform.rotation = LeftArmRotations[CurrentArmsIndex];
        }
    }
    
    private void UpdateRightArmVisualization()
    {
        // Проверяем корректность индекса
        if (ArmsSprites == null || ArmsSprites.Count <= CurrentArmsIndex ||
            RightArmScales == null || RightArmScales.Count <= CurrentArmsIndex ||
            RightArmPositions == null || RightArmPositions.Count <= CurrentArmsIndex ||
            RightArmRotations == null || RightArmRotations.Count <= CurrentArmsIndex)
        {
            Debug.LogWarning("Индекс выходит за пределы списка или списки не заданы.");
            return;
        }

        // Устанавливаем спрайт для RightArmObject
        if (RightArmObject != null)
        {
            var spriteRenderer = RightArmObject.GetComponent<SpriteRenderer>();
            if (spriteRenderer == null)
            {
                spriteRenderer = RightArmObject.AddComponent<SpriteRenderer>();
            }
            spriteRenderer.sprite = ArmsSprites[CurrentArmsIndex];
        }

        // Настройка трансформации для RightArmBone
        if (RightArmBone != null)
        {
            RightArmBone.transform.localScale = RightArmScales[CurrentArmsIndex];
            RightArmBone.transform.localPosition = RightArmPositions[CurrentArmsIndex];
            RightArmBone.transform.rotation = RightArmRotations[CurrentArmsIndex];
        }
    }*/

}
