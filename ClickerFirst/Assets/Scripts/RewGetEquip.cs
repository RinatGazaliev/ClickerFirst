using System;
using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.UI;
using YG;
using Random = UnityEngine.Random;

public class RewGetEquip : MonoBehaviour
{

    private Button btnGetEquip;
    [SerializeField] private AttrShop attrShop;
    [SerializeField] private GameObject attrButtonPtr;
    [SerializeField] private ShowWgtManager showWgtManager;
    [SerializeField] private string YGRewardID;
    [SerializeField] public Image imgTV;
    public List<int> arrayIndices = new List<int>();
    public static event Action OnEquipRewPressed;
   // public static event Action OnRewardGetEquipTimeFinish;
    public static event Action <bool> OnRewardTimerUpdate;
    // Start is called before the first frame update
    void Start()
    {
        int isRewardEnded=PlayerPrefs.GetInt("AllEquipWatched", 0);
        if (isRewardEnded==0)
        {
            btnGetEquip = GetComponent<Button>();
            if (btnGetEquip!=null)
            {
                btnGetEquip.onClick.AddListener(CallRewVideo); 
            }
            attrShop.InitFunct();
            attrShop.CollectChildNames();
        }
        else
        {
            //gameObject.SetActive(false);
        }
        
    }

    private void Awake()
    {
        LeftButtZoneManager.OnTutAnimFinished += OnTutAnimFinishedGetEquip;
        
    }

    private void OnDestroy()
    {
        LeftButtZoneManager.OnTutAnimFinished -= OnTutAnimFinishedGetEquip;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnRewardGain(bool _isRewardUpdate)
    {
   
        //attrButtonPtr.SetActive(true);
        attrShop.InitFunct();
       
        Debug.Log("OpenRandomLocked");
        // Список для непустых массивов
        List<List<int>> nonEmptyArrays = new List<List<int>>();
       
        arrayIndices.Clear();
        nonEmptyArrays.Clear();
        Debug.Log("youMUSTBEHERE"+attrShop.HatNotActiveElements.Count);
        // Проверяем каждый массив на наличие элементов
        if (attrShop.HatNotActiveElements.Count > 1)
        {
            
            nonEmptyArrays.Add(attrShop.HatNotActiveElements);
            arrayIndices.Add(0); // Индекс массива 0
            Debug.Log("HatAdded"+attrShop.HatNotActiveElements.Count);
        }
        if (attrShop.LegsNotActiveElements.Count > 1)
        {
            nonEmptyArrays.Add(attrShop.LegsNotActiveElements);
            arrayIndices.Add(1); // Индекс массива 3
            Debug.Log("LegAdded"+attrShop.LegsNotActiveElements.Count);
        }
        if (attrShop.ArmsNotActiveElements.Count > 1)
        {
            nonEmptyArrays.Add(attrShop.ArmsNotActiveElements);
            arrayIndices.Add(2); // Индекс массива 4
            Debug.Log("ArmAdded"+attrShop.ArmsNotActiveElements.Count);
        }

        if (attrShop.GlassesNotActiveElements.Count > 1)
        {
            nonEmptyArrays.Add(attrShop.GlassesNotActiveElements);
            arrayIndices.Add(3); // Индекс массива 2
            Debug.Log("GlassAdded"+attrShop.GlassesNotActiveElements.Count);
        }

        if (attrShop.JewelryNotActiveElements.Count > 1)
        {
            nonEmptyArrays.Add(attrShop.JewelryNotActiveElements);
            arrayIndices.Add(4);// Индекс массива 1
            Debug.Log("JewAdded"+attrShop.JewelryNotActiveElements.Count);
        }
        
      

        // Если нет непустых массивов, выводим предупреждение и выходим
        if (nonEmptyArrays.Count == 0)
        {
            Debug.LogWarning("Нет непустых массивов.");
            PlayerPrefs.SetInt("AllEquipWatched", 1);
            return;
        }

        int randomArrayIndiciesNum = Random.Range(0, arrayIndices.Count);
        int selectedArrayIndex = arrayIndices[randomArrayIndiciesNum];
        List<int> selectedArray = nonEmptyArrays[randomArrayIndiciesNum];
        // Случайный выбор массива из непустых
        //List<int> selectedArray = nonEmptyArrays[Random.Range(0, nonEmptyArrays.Count)];
        if (selectedArray.Count==1)
        {
            Debug.LogWarning("Нет доступного эвкипа в группе");
            return;
        }
        // Случайный выбор элемента из выбранного массива
        int randomElement = selectedArray[Random.Range(1, selectedArray.Count)];

        // Выводим выбранный элемент
        Debug.Log($"Выбран элемент {randomElement} из массива., а его длина {selectedArray.Count}, а имя массива {selectedArray}");
       // attrShop.Arr
        string groupName = attrShop.groupNames[selectedArrayIndex];
      
        string nameToSave = $"Equip_{groupName}_N_{randomElement}";
        Debug.Log("nameToSave"+nameToSave);
        PlayerPrefs.SetInt(nameToSave,1);
        
        showWgtManager.ShowNewItemPopUp(groupName,randomElement);
       // OnRewardGetEquipTimeFinish();
       if (nonEmptyArrays.Count==0&&selectedArray.Count<=2)
       {
           PlayerPrefs.SetInt("AllEquipWatched", 1);
       }

        OnRewardTimerUpdate(_isRewardUpdate);
      
        
        //if (OnEquipRewPressed != null) OnEquipRewPressed();
    }

    private void OnTutAnimFinishedGetEquip(string tutName)
    {
        if (tutName == "Tut3")
        {
            Debug.Log("GetRewardTut3");
            attrButtonPtr.SetActive(true);
            OnRewardGain(false);
        }
    }

    public void SelectRandomElement()
    {
  
    }
    private void GetRewardFinish()
    {
        OnRewardGain(true);
    }
    private void OnEnable()
    {
        YG2RewardManager.instance.RewGetEquipFinish += GetRewardFinish;
       // LeftButtZoneManager.OnTutAnimFinished += OnTutAnimFinishedGetEquip;
        //YG2RewardManager.instance.RewAutoClickStart += TouchContinue_VideoRewardClosed;
    }
    private void OnDisable()
    {
        YG2RewardManager.instance.RewGetEquipFinish -= GetRewardFinish;
       // LeftButtZoneManager.OnTutAnimFinished -= OnTutAnimFinishedGetEquip;
        //YG2RewardManager.instance.RewAutoClickStart -= TouchContinue_VideoRewardClosed;
    }
    private void CallRewVideo()
    {
        SoundManager.instance.PlaySound_ButtClick();
        MusicManager.instance.DisableMusic();
        SoundManager.instance.DisableSound();
        MusicManager.instance.isSwapLocked = true;
        YG2.RewardedAdvShow(YGRewardID);
    }
}
