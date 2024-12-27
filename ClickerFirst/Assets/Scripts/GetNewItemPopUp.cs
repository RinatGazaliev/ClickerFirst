using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class GetNewItemPopUp : MonoBehaviour
{
    private string currGroup;

    private int currEquipN;

    [SerializeField] private Image imgSpriteEquip; // Теперь можно задать через инспектор
    [SerializeField] private Button btnEquipItem;

    private string isItemEquippedName;

    public static event Action<string, int> OnItemEquipped = delegate (string _equipGroup, int _equipN) { };
    public static event Action<string, int, Image> OnNeedFindSprite = delegate (string _equipGroup, int _equipN, Image _imageSprite) { };

    // Start is called before the first frame update
    void Start()
    {
        // Если imgSpriteEquip не задан через инспектор, ищем его среди дочерних объектов
        if (imgSpriteEquip == null)
        {
            imgSpriteEquip = FindImageAmongChildren(transform);

            if (imgSpriteEquip == null)
            {
                Debug.LogError("Image не найден среди дочерних объектов и не задан в инспекторе.");
            }
        }

        // Назначаем слушатель кнопки
        if (btnEquipItem != null)
        {
            btnEquipItem.onClick.AddListener(SaveDataAndEquip);
        }
        else
        {
            Debug.LogError("btnEquipItem не назначен в инспекторе.");
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void ShowGetNewItemPopUp(string group, int equip)
    {
        currGroup = group;
        currEquipN = equip;

        // Вызываем событие для обработки отображения
        OnNeedFindSprite(currGroup, currEquipN, imgSpriteEquip);
    }

    private Image FindImageAmongChildren(Transform parent)
    {
        // Перебираем только первый уровень дочерних объектов
        for (int i = 0; i < parent.childCount; i++)
        {
            Transform child = parent.GetChild(i);
            Image image = child.GetComponent<Image>();
            if (image != null)
            {
                return image; // Возвращаем первый найденный компонент Image
            }
        }
        return null; // Если Image не найден
    }

    private void SaveDataAndEquip()
    {
        isItemEquippedName = $"ItemEquipped_{currGroup}_N_";
        PlayerPrefs.SetInt(isItemEquippedName, currEquipN);
        OnItemEquipped(currGroup, currEquipN);
    }
}
