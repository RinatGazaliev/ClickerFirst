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
        isItemEquippedName = $"ItemEquipped_{equipGroup}_N_";
        Debug.Log("fullPlayerPrefsName"+fullPlayerPrefsName);
        btnSelf = GetComponent<Button>();
        btnSelf.onClick.AddListener(SaveDataAndEquip);
        imgSpriteEquip = FindImageAmongChildren(transform);
        InitView();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
       // btnSelf = GetComponent<Button>();
       ShowScenarioVisibility();
       OnItemEquipped += ShopAttrEquipped;
        RewGetEquip.OnEquipRewPressed += InitView;
        // RewGetEquip.OnEquipRewPressed += InitView;

    }

    private void OnDisable()
    {
        RewGetEquip.OnEquipRewPressed -= InitView;
        OnItemEquipped -= ShopAttrEquipped;
    }

    private int CheckScenario()
    {

        int isEnable = PlayerPrefs.GetInt(fullPlayerPrefsName, 0);
        if (equipN==0)
        {
            isEnable = 1;
        }
        if (isEnable==0)
        {
            return 0; 
        }
        else
        {
            if ( PlayerPrefs.GetInt(isItemEquippedName, 0)!=equipN)
            {
                return 1;
            }
            else
            {
                return 2;
            }
        }    
        
    }

    private void ShowScenarioVisibility()
    {
        int currScenario = CheckScenario();
        SetDefaultVisibility();
        Debug.Log("INITEDVIEWS"+equipN);
        switch (currScenario)
        {
            case 0:
                Transform child = transform.Find("Scenario_1");
                child.gameObject.SetActive(true);
                if (btnSelf)
                {
                    btnSelf.interactable=false;
                }
                break;
            case 1:
                Transform child1 = transform.Find("Scenario_2");
                child1.gameObject.SetActive(true);
                if (btnSelf)
                {
                    btnSelf.interactable = true;
                }

                break;
            case 2:
                Transform child2 = transform.Find("Scenario_3");
                child2.gameObject.SetActive(true);
                if (btnSelf)
                {
                    btnSelf.interactable = false;
                }

                break;
        }

    }

    private void ShopAttrEquipped(string _equipGroup, int _equipN)
    {
        if (equipGroup==_equipGroup)
        {
            ShowScenarioVisibility();
        }
        
    }

    private void InitView()
    {
        ShowScenarioVisibility();
        OnNeedFindSprite(equipGroup,equipN,imgSpriteEquip);
    }

    private void SetDefaultVisibility()
    {
       transform.Find("Scenario_1").gameObject.SetActive(false);
       transform.Find("Scenario_2").gameObject.SetActive(false);
       transform.Find("Scenario_3").gameObject.SetActive(false);
    }

    //Туут необходимо обратиться к Character и обновить в нем активный спрайт для соответствующей группы
    private void SaveDataAndEquip()
    {
        PlayerPrefs.SetInt(isItemEquippedName, equipN);
        //InitView();
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
