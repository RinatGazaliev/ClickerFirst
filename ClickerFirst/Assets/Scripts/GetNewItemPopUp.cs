using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class GetNewItemPopUp : MonoBehaviour
{
    private string currGroup;

    private int currEquipN;
    private Image imgSpriteEquip;
    private string isItemEquippedName;
    [SerializeField] private Button btnEquipItem;
    
    public static event Action<string,int> OnItemEquipped = delegate(string _equipGroup, int _equipN) { };
    public static event Action<string,int,Image> OnNeedFindSprite = delegate(string _equipGroup, int _equipN, Image _imageSprite) { };
    // Start is called before the first frame update
    void Start()
    {
        imgSpriteEquip = FindImageAmongChildren(transform);
        btnEquipItem.onClick.AddListener(SaveDataAndEquip);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowGetNewItemPopUp(string group, int equip)
    {
        currGroup = group;
        currEquipN = equip;
       
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
        isItemEquippedName = $"ItemEquipped_{currGroup}_N_{currEquipN}";
        PlayerPrefs.SetInt(isItemEquippedName, 1);
        OnItemEquipped(currGroup, currEquipN);
    }
}
