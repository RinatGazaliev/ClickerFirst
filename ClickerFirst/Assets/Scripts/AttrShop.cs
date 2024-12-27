using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class AttrShop : MonoBehaviour
{
    public List<string> groupNames;
    public string currActiveGroup = "Hat";
    
    [SerializeField] private GameObject groupHat; // Название группы
    [SerializeField] private Button btnHatGroup;
    [SerializeField] private List<Sprite> spritesHat;

    public List<int> HatNotActiveElements;
    
    

    [SerializeField] private GameObject groupJewelry; // Название группы
    [SerializeField] private Button btnJewelryGroup;
    [SerializeField] private List<Sprite> spritesJewelry;
    public List<int> JewelryNotActiveElements;
   
    
    [SerializeField] private GameObject groupGlasses; // Название группы
    [SerializeField] private Button btnGlassesGroup;
    [SerializeField] private List<Sprite> spritesGlasses;
    public List<int> GlassesNotActiveElements;
    
    [SerializeField] private GameObject groupArms; // Название группы
    [SerializeField] private Button btnArmsGroup;
    [SerializeField] private List<Sprite> spritesArms;
    public List<int> ArmsNotActiveElements;
    
    [SerializeField] private GameObject groupLegs; // Название группы
    [SerializeField] private Button btnLegsGroup;
    [SerializeField] private List<Sprite> spritesLegs;
    public List<int> LegsNotActiveElements;
    

    // Start is called before the first frame update
    void Start()
    {
        InitFunct();
        UpdateViews();
        btnHatGroup.onClick.AddListener(BtnHatClicked);
        btnJewelryGroup.onClick.AddListener(BtnJewelryClicked);
        btnGlassesGroup.onClick.AddListener(BtnGlassesClicked);
        btnLegsGroup.onClick.AddListener(BtnLegsClicked);
        btnArmsGroup.onClick.AddListener(BtnArmsClicked);

    }

    private void Awake()
    {
        InitFunct();
        CollectChildNames();
        //  InitFunct();
        // RewGetEquip.OnEquipRewPressed  += InitFunct;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnEnable()
    {
        // Подписываемся на событие
        EquipButtons.OnNeedFindSprite += FindSprite;
        GetNewItemPopUp.OnNeedFindSprite += FindSprite;
        
    }

    void OnDisable()
    {
        // Отписываемся от события
        //RewGetEquip.OnEquipRewPressed  -= InitFunct;
        EquipButtons.OnNeedFindSprite -= FindSprite;
        GetNewItemPopUp.OnNeedFindSprite -= FindSprite;
        
    }

    public void InitFunct()
    {
        ActivateRandomElement(groupHat, HatNotActiveElements);
        ActivateRandomElement(groupJewelry, JewelryNotActiveElements);
        ActivateRandomElement(groupGlasses, GlassesNotActiveElements);
        ActivateRandomElement(groupArms, ArmsNotActiveElements);
        ActivateRandomElement(groupLegs, LegsNotActiveElements);
        CollectChildNames();
    }

    private void BtnHatClicked()
    {
        currActiveGroup = "Hat";
        UpdateViews();
    }
    
    private void BtnJewelryClicked()
    {
        currActiveGroup = "Jewelry";
        UpdateViews();
    }
    
    private void BtnGlassesClicked()
    {
        currActiveGroup = "Glasses";
        UpdateViews();
    }
    
    private void BtnArmsClicked()
    {
        currActiveGroup = "Arms";
        UpdateViews();
    }
    
    private void BtnLegsClicked()
    {
        currActiveGroup = "Legs";
        UpdateViews();
    }

    public void UpdateViews()
    {
        DisableAttrView();
        switch (currActiveGroup)
        {
            case "Hat" :
                groupHat.SetActive(true);
                btnHatGroup.transform.Find("ActiveView").gameObject.SetActive(true);
                btnHatGroup.transform.Find("InactiveView").gameObject.SetActive(false);
                break;
            case "Jewelry" :
                groupJewelry.SetActive(true);
                btnJewelryGroup.transform.Find("ActiveView").gameObject.SetActive(true);
                btnJewelryGroup.transform.Find("InactiveView").gameObject.SetActive(false);
                break;
            case "Glasses" :
                groupGlasses.SetActive(true);
                btnGlassesGroup.transform.Find("ActiveView").gameObject.SetActive(true);
                btnGlassesGroup.transform.Find("InactiveView").gameObject.SetActive(false);
                break;
            case "Legs" :
                groupLegs.SetActive(true);
                btnLegsGroup.transform.Find("ActiveView").gameObject.SetActive(true);
                btnLegsGroup.transform.Find("InactiveView").gameObject.SetActive(false);
                break;
            
            case "Arms" :
                groupArms.SetActive(true);
                btnArmsGroup.transform.Find("ActiveView").gameObject.SetActive(true);
                btnArmsGroup.transform.Find("InactiveView").gameObject.SetActive(false);
                break;
        }
    }

    private void DisableAttrView()
    {
        //Hat
        groupHat.SetActive(false);
        btnHatGroup.transform.Find("ActiveView").gameObject.SetActive(false);
        btnHatGroup.transform.Find("InactiveView").gameObject.SetActive(true);
        
        //Jewelry
        groupJewelry.SetActive(false);
        btnJewelryGroup.transform.Find("ActiveView").gameObject.SetActive(false);
        btnJewelryGroup.transform.Find("InactiveView").gameObject.SetActive(true);
        
        //Glasses
        groupGlasses.SetActive(false);
        btnGlassesGroup.transform.Find("ActiveView").gameObject.SetActive(false);
        btnGlassesGroup.transform.Find("InactiveView").gameObject.SetActive(true);
        
        //Legs
        groupLegs.SetActive(false);
        btnLegsGroup.transform.Find("ActiveView").gameObject.SetActive(false);
        btnLegsGroup.transform.Find("InactiveView").gameObject.SetActive(true);
        
        //Arms
        groupArms.SetActive(false);
        btnArmsGroup.transform.Find("ActiveView").gameObject.SetActive(false);
        btnArmsGroup.transform.Find("InactiveView").gameObject.SetActive(true);
    }

    private void SetHatActive()
    {
        //Hat
        groupHat.SetActive(true);
        btnHatGroup.transform.Find("ActiveView").gameObject.SetActive(true);
        btnHatGroup.transform.Find("InactiveView").gameObject.SetActive(true);
        
        groupHat.SetActive(true);
        btnHatGroup.transform.Find("ActiveView").gameObject.SetActive(true);
        btnHatGroup.transform.Find("InactiveView").gameObject.SetActive(true);
    }

    public void ActivateRandomElement(GameObject groupObject, List<int> elementNumbers)
    {
        if (groupObject == null)
        {
            Debug.LogWarning("Группа не задана.");
            return;
        }

        // Получаем дочерние объекты группы
        int childCount = groupObject.transform.childCount;
        if (childCount == 0)
        {
            Debug.LogWarning($"Группа \"{groupObject.name}\" не содержит дочерних объектов.");
            return;
        }

        // Очищаем текущий список перед заполнением
        elementNumbers.Clear();

        // Генерация массива чисел для дочерних объектов
        elementNumbers.AddRange(Enumerable.Range(0, childCount));

        // Исключение сохранённых чисел из PlayerPrefs
        string groupKey = $"Group_{groupObject.name}_";
        
        for (int i = elementNumbers.Count - 1; i >= 0; i--)
        {
            string nameToCheck = $"Equip_{groupObject.name}_N_{elementNumbers[i]}";
            Debug.Log("nameToCheck"+nameToCheck);
            if (PlayerPrefs.GetInt(nameToCheck,0)==1)
            {
                elementNumbers.RemoveAt(i);
               
            }
        }

        if (elementNumbers.Count == 0)
        {
            Debug.Log($"Все элементы группы \"{groupObject.name}\" уже активированы.");
            return;
        }

        Debug.Log("Количество элементов: " + elementNumbers.Count);
        /*// Выбор случайного элемента из оставшихся
        int randomElement = elementNumbers[Random.Range(0, elementNumbers.Count)];

        // Активируем выбранный дочерний объект
        Transform selectedChild = groupObject.transform.GetChild(randomElement);
        selectedChild.gameObject.SetActive(true);

        // Сохраняем данные о выбранном элементе в PlayerPrefs
        PlayerPrefs.SetInt($"{groupKey}{randomElement}", 1);
        PlayerPrefs.Save();

        Debug.Log($"Активирован элемент {randomElement} ({selectedChild.name}) из группы \"{groupObject.name}\".");*/
        
    }
    
    public void CollectChildNames()
    {
        // Очищаем список, чтобы избежать дублирования при повторном вызове
        groupNames.Clear();
        Debug.Log("CheckHatCount"+HatNotActiveElements.Count);

        // Проверяем, есть ли дочерние элементы
        if (transform.childCount == 0)
        {
            Debug.LogWarning("У объекта нет дочерних элементов.");
            return;
        }

        // Итерируемся по всем дочерним объектам
        foreach (Transform child in transform)
        {
            if (child.name!="btnClosePopup")
            {
                groupNames.Add(child.name); // Добавляем имя дочернего объекта в список
            }
            
        }
    }
    
    public void FindSprite(string equipGroup, int equipN, Image spriteToSet)
    {
        Debug.Log("FindSpriteFunct");
        switch (equipGroup)
        {
            case "Hat" :
                spriteToSet.sprite=spritesHat[equipN];
                break;
            case "Jewelry" :
                spriteToSet.sprite=spritesJewelry[equipN];
                break;
            case "Glasses" :
                spriteToSet.sprite=spritesGlasses[equipN];
                break;
            case "Legs" :
                spriteToSet.sprite=spritesLegs[equipN];
                break;
            
            case "Arms" :
                spriteToSet.sprite=spritesArms[equipN];
                break;
            
        }
   
    }
}
