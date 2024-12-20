using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class EquipButtons : MonoBehaviour
{
    private enum GroupOptions
    {
        Hat,
        Jewelry,
        Legs,
        Arms,
        Glasses
    }
    
    [SerializeField] private GroupOptions selectedGroup;
    private string equipGroup => selectedGroup.ToString();
    [SerializeField] private int equipN;
    //[SerializeField] private string equipGroup;

    private int currScenario;
    private string fullPlayerPrefsName;
    private string isItemEquippedName;

    private Button btnSelf;
    private Image imgSpriteEquip;
    
    public static event Action<string,int> OnItemEquipped = delegate(string _equipGroup, int _equipN) { };
    public static event Action<string,int,Image> OnNeedFindSprite = delegate(string _equipGroup, int _equipN, Image _imageSprite) { };
    // Start is called before the first frame update
    void Start()
    {
        fullPlayerPrefsName = $"Equip_{equipGroup}_N_{equipN}";
        isItemEquippedName = $"ItemEquipped_{equipGroup}_N_{equipN}";
        btnSelf = GetComponent<Button>();
        btnSelf.onClick.AddListener(SaveDataAndEquip);
        imgSpriteEquip = FindImageAmongChildren(transform);
        InitView();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private int CheckScenario()
    {

        int isEnable = PlayerPrefs.GetInt(fullPlayerPrefsName, 0);
        if (isEnable==0)
        {
            return 0; 
        }
        else
        {
            if ( PlayerPrefs.GetInt(isItemEquippedName, 0)==0)
            {
                return 1;
            }
            else
            {
                return 2;
            }
        }    
        
    }

    private void InitView()
    {
        int currScenario = CheckScenario();
        //Debug.Log("INITEDVIEWS");
        switch (currScenario)
        {
            case 0:
                Transform child = transform.Find("Scenario_1");
                child.gameObject.SetActive(true);
                btnSelf.interactable=false;
                break;
            case 1:
                Transform child1 = transform.Find("Scenario_2");
                child1.gameObject.SetActive(true);
                break;
            case 2:
                Transform child2 = transform.Find("Scenario_3");
                child2.gameObject.SetActive(true);
                btnSelf.interactable=false;
                break;
        }

        OnNeedFindSprite(equipGroup,equipN,imgSpriteEquip);
    }

    //Туут необходимо обратиться к Character и обновить в нем активный спрайт для соответствующей группы
    private void SaveDataAndEquip()
    {
        PlayerPrefs.SetInt(isItemEquippedName, 1);
        OnItemEquipped(equipGroup, equipN);
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
}
