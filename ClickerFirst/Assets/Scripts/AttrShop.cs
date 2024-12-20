using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AttrShop : MonoBehaviour
{
    public List<string> groupNames;
    
    [SerializeField] private GameObject groupHat; // Название группы

    public List<int> HatNotActiveElements; 
    

    [SerializeField] private GameObject groupJewelry; // Название группы
    public List<int> JewelryNotActiveElements;
    
    [SerializeField] private GameObject groupGlasses; // Название группы
    public List<int> GlassesNotActiveElements;
    
    [SerializeField] private GameObject groupArms; // Название группы
    public List<int> ArmsNotActiveElements;
    
    [SerializeField] private GameObject groupLegs; // Название группы
    public List<int> LegsNotActiveElements;
    

    // Start is called before the first frame update
    void Start()
    {
        InitFunct();

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
        
        
    }

    void OnDisable()
    {
        // Отписываемся от события
        RewGetEquip.OnEquipRewPressed  -= InitFunct;
        
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
}
