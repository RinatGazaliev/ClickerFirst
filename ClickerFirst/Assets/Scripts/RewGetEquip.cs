using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using YG;
using Random = UnityEngine.Random;

public class RewGetEquip : MonoBehaviour
{

    private Button btnGetEquip;
    [SerializeField] private AttrShop attrShop;
    [SerializeField] private ShowWgtManager showWgtManager;
    [SerializeField] private string YGRewardID;
    
    public static event Action OnEquipRewPressed;
    public static event Action OnRewardGetEquipTimeFinish;
    // Start is called before the first frame update
    void Start()
    {
        btnGetEquip = GetComponent<Button>();
        if (btnGetEquip!=null)
        {
           btnGetEquip.onClick.AddListener(CallRewVideo); 
        }

        
    }



    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnRewardGain()
    {
       
        Debug.Log("OpenRandomLocked");
        // Список для непустых массивов
        List<List<int>> nonEmptyArrays = new List<List<int>>();
        List<int> arrayIndices = new List<int>();
        Debug.Log("youMUSTBEHERE"+attrShop.HatNotActiveElements.Count);
        // Проверяем каждый массив на наличие элементов
        if (attrShop.HatNotActiveElements.Count > 0)
        {
            
            nonEmptyArrays.Add(attrShop.HatNotActiveElements);
            arrayIndices.Add(0); // Индекс массива 0
        }
        if (attrShop.JewelryNotActiveElements!=null&&attrShop.JewelryNotActiveElements.Count > 0)
        {
            nonEmptyArrays.Add(attrShop.JewelryNotActiveElements);
            arrayIndices.Add(1); // Индекс массива 1
        }
        if (attrShop.GlassesNotActiveElements!=null&&attrShop.GlassesNotActiveElements.Count > 0)
        {
            nonEmptyArrays.Add(attrShop.GlassesNotActiveElements);
            arrayIndices.Add(2); // Индекс массива 2
        }
        if (attrShop.LegsNotActiveElements!=null&&attrShop.LegsNotActiveElements.Count > 0)
        {
            nonEmptyArrays.Add(attrShop.LegsNotActiveElements);
            arrayIndices.Add(3); // Индекс массива 3
        }
        if (attrShop.ArmsNotActiveElements!=null&&attrShop.ArmsNotActiveElements.Count > 0)
        {
            nonEmptyArrays.Add(attrShop.ArmsNotActiveElements);
            arrayIndices.Add(4); // Индекс массива 4
        }

        
      

        // Если нет непустых массивов, выводим предупреждение и выходим
        if (nonEmptyArrays.Count == 0)
        {
            Debug.LogWarning("Нет непустых массивов.");
            return;
        }
        int selectedArrayIndex = arrayIndices[Random.Range(0, nonEmptyArrays.Count)];
        List<int> selectedArray = nonEmptyArrays[selectedArrayIndex];
        // Случайный выбор массива из непустых
        //List<int> selectedArray = nonEmptyArrays[Random.Range(0, nonEmptyArrays.Count)];

        // Случайный выбор элемента из выбранного массива
        int randomElement = selectedArray[Random.Range(0, selectedArray.Count)];

        // Выводим выбранный элемент
        Debug.Log($"Выбран элемент {randomElement} из массива.");
       // attrShop.Arr
        string groupName = attrShop.groupNames[selectedArrayIndex];
      
        string nameToSave = $"Equip_{groupName}_N_{randomElement}";
        Debug.Log("nameToSave"+nameToSave);
        PlayerPrefs.SetInt(nameToSave,1);
        attrShop.InitFunct();
        showWgtManager.ShowNewItemPopUp(groupName,randomElement);
        OnRewardGetEquipTimeFinish();
        //if (OnEquipRewPressed != null) OnEquipRewPressed();
    }
    
    public void SelectRandomElement()
    {
  
    }
    
    private void OnEnable()
    {
        YG2RewardManager.instance.RewGetEquipFinish += OnRewardGain;
        //YG2RewardManager.instance.RewAutoClickStart += TouchContinue_VideoRewardClosed;
    }
    private void OnDisable()
    {
        YG2RewardManager.instance.RewGetEquipFinish -= OnRewardGain;
        //YG2RewardManager.instance.RewAutoClickStart -= TouchContinue_VideoRewardClosed;
    }
    private void CallRewVideo()
    {
        YG2.RewardedAdvShow(YGRewardID);
    }
}
