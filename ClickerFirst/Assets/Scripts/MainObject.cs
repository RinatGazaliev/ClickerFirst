using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.U2D.Animation;

public class MainObject : MonoBehaviour
{
    
    // Событие, на которое могут подписаться другие объекты
    [SerializeField] private Animator animContrCharacter;
    [SerializeField] private CoinsOnClick coinsOnClickPtr;
   // [SerializeField] private BoxCollider2D bodyCollider;
    public static event Action <GameObject> OnObjectClicked;
    public static event Action  OnCallCoins;

    public void  OnMouseDown()
    {
        //OnObjectClicked?.Invoke(gameObject);
   
        
    }// Start is called before the first frame update
    
    public void  CallMainObjClicked(Vector2 coordCoins)
    {
        /*Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (bodyCollider.OverlapPoint(mousePosition))
        {

        }
        else
        {
            Debug.Log("Кликнул что-то другое!");
        }*/

        // Вызываем событие и передаем объект, на который кликнули
        coinsOnClickPtr.transform.position = coordCoins;
        OnChangeForkIsRunning(Config.isRunning);
        SoundManager.instance.PlaySound_SosClick();
        OnObjectClicked?.Invoke(gameObject);
        OnCallCoins.Invoke();
        // Если клик был на спрайте
        Debug.Log("Спрайт был кликнут через родительский объект!");
        // Debug.Log("ConfiScorePerClickKF"+Config.GetPerClickScaleKf());
    }



    public void OnPointerClick()
    {
        Debug.Log("MainObjClicked");
        Debug.Log("ISRunning"+Config.isRunning);
        OnChangeForkIsRunning(Config.isRunning);
        OnObjectClicked?.Invoke(gameObject);
        OnCallCoins.Invoke();

    }
    void Start()
    {
        //animContrCharacter = GetComponent<Animator>();
        CheckEquippedItems();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnValidate()
    {
        // Обновляем визуализацию при изменении CurrentIndex
        // UpdateHeadVisualization();
        UpdateHatGroupVision();
        UpdateGlassesGroupVision();
        UpdateJewelryGroupVision();
        UpdateArmsGroupVision();
        UpdateLegsGroupVision();
    }

    private void OnEnable()
    {
        EquipButtons.OnItemEquipped += EquipNewItem;
        GetNewItemPopUp.OnItemEquipped += EquipNewItem;
        MovingRoad.OnIsWalkingChange += OnChangeIsWalking;
        ForkBar.OnForkBarIsRunning += OnChangeForkIsRunning;
        RewMoveBoost.OnKickCalled += CallKick;
        


    }

    private void OnDisable()
    {
        EquipButtons.OnItemEquipped -= EquipNewItem;
        GetNewItemPopUp.OnItemEquipped -= EquipNewItem;
        MovingRoad.OnIsWalkingChange -= OnChangeIsWalking;
        ForkBar.OnForkBarIsRunning -= OnChangeForkIsRunning;
        RewMoveBoost.OnKickCalled -= CallKick;
    }


    private void OnChangeIsWalking(bool _isWalking)
    {
       animContrCharacter.SetBool("isWalking", _isWalking);
       if (_isWalking==false)
       {
           animContrCharacter.SetBool("isRunning", _isWalking);
       }
    }
    
    private void OnChangeForkIsRunning(bool _isRunning)
    {
        animContrCharacter.SetBool("isRunning", _isRunning);
    }

    private void CheckEquippedItems()
    {
        CurrentHatIndex = PlayerPrefs.GetInt("ItemEquipped_Hat_N_", 0);
        CurrentJewelryIndex = PlayerPrefs.GetInt("ItemEquipped_Jewelry_N_", 0);
        CurrentGlassesIndex = PlayerPrefs.GetInt("ItemEquipped_Glasses_N_", 0);
        CurrentLegsIndex = PlayerPrefs.GetInt("ItemEquipped_Legs_N_", 0);
        CurrentArmsIndex = PlayerPrefs.GetInt("ItemEquipped_Arms_N_", 0);
        UpdateHatGroupVision();
        UpdateGlassesGroupVision();
        UpdateJewelryGroupVision();
        UpdateArmsGroupVision();
        UpdateLegsGroupVision();
    }

    private void CallKick()
    {
        animContrCharacter.SetTrigger("KickAnim");
    }


    private void EquipNewItem(string equipGroup, int equipN)
    {
        switch (equipGroup)
        {
            case "Hat":
                
                CurrentHatIndex = equipN;
                UpdateHatGroupVision();
                break;
            
            case "Glasses":
                CurrentGlassesIndex = equipN;
                UpdateGlassesGroupVision();
                break;
            case "Jewelry":
                CurrentJewelryIndex = equipN;
                UpdateJewelryGroupVision();
                break;
            case "Legs":
                CurrentLegsIndex = equipN;
                UpdateLegsGroupVision();
                break;
            case "Arms":
                CurrentArmsIndex = equipN;
                UpdateArmsGroupVision();
                break;
                
        }
    }
    
    private void FindSprite(string equipGroup, int equipN, Image spriteToSet)
    {
         spriteToSet.sprite=HatGroup.GetComponent<SpriteResolver>().spriteLibrary.GetSprite(equipGroup, $"Entry_{equipN}");
    }


    [Header("Hat")] 
    [SerializeField] private GameObject HatGroup;
    [SerializeField] int CurrentHatIndex;
    //[SerializeField] private List<Vector3> HatPositions;
    private void UpdateHatGroupVision()
    {
        HatGroup.GetComponent<SpriteResolver>().SetCategoryAndLabel("Hat", $"Entry_{CurrentHatIndex}");
        //HatGroup.transform.localPosition = HatPositions[CurrentHatIndex];

    }
    
    [Header("Glasses")] 
    [SerializeField] private GameObject GlassesGroup;
    [SerializeField] int CurrentGlassesIndex;
   // [SerializeField] private List<Vector3> GlassesPositions;
    private void UpdateGlassesGroupVision()
    {
        GlassesGroup.GetComponent<SpriteResolver>().SetCategoryAndLabel("Glasses", $"Entry_{CurrentGlassesIndex}");
       // GlassesGroup.transform.localPosition = GlassesPositions[CurrentGlassesIndex];

    }
    
    
    [Header("Jewerly")] 
    [SerializeField] private GameObject JewelryGroup;
    [SerializeField] int CurrentJewelryIndex;
   // [SerializeField] private List<Vector3> JewelryPositions;
    private void UpdateJewelryGroupVision()
    {
        JewelryGroup.GetComponent<SpriteResolver>().SetCategoryAndLabel("Jewelry", $"Entry_{CurrentJewelryIndex}");
       // JewelryGroup.transform.localPosition = JewelryPositions[CurrentJewelryIndex];

    }
    
    
    [Header("Arms")] 
    [SerializeField] private GameObject LeftArmGroup;
    [SerializeField] private GameObject RightArmGroup;
    [SerializeField] int CurrentArmsIndex;
    /*[SerializeField] private List<Vector3> LeftArmPostion;
    [SerializeField] private List<Vector3> RightArmPostion;*/
    
    private void UpdateArmsGroupVision()
    {
        LeftArmGroup.GetComponent<SpriteResolver>().SetCategoryAndLabel("LeftArm", $"Entry_{CurrentArmsIndex}");
        RightArmGroup.GetComponent<SpriteResolver>().SetCategoryAndLabel("RightArm", $"Entry_{CurrentArmsIndex}");
        
        //LeftArmGroup.transform.localPosition = LeftArmPostion[CurrentArmsIndex];
       // RightArmGroup.transform.localPosition = RightArmPostion[CurrentArmsIndex];

    }
    
    [Header("Legs")] 
    [SerializeField] private GameObject LeftLegGroup;
    [SerializeField] private GameObject RightLegGroup;
    [SerializeField] int CurrentLegsIndex;
    /*[SerializeField] private List<Vector3> LeftLegPostion;
    [SerializeField] private List<Vector3> RightLegPostion;*/
    
    private void UpdateLegsGroupVision()
    {
        LeftLegGroup.GetComponent<SpriteResolver>().SetCategoryAndLabel("LeftLeg", $"Entry_{CurrentLegsIndex}");
        RightLegGroup.GetComponent<SpriteResolver>().SetCategoryAndLabel("RightLeg", $"Entry_{CurrentLegsIndex}");
        
       // LeftLegGroup.transform.localPosition = LeftLegPostion[CurrentLegsIndex];
       // RightLegGroup.transform.localPosition = RightLegPostion[CurrentLegsIndex];
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
