using System;
using System.Collections;
using System.Collections.Generic;
using CrazyGames;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ShowWgtManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static ShowWgtManager instance;
    public static event Action OnDisableCharClick;
    
    
    void Start()
    {
        instance = this;
        btnCloseAttrPopup.onClick.AddListener(TouchCloseAttr);
        btnEquipShop.onClick.AddListener(TouchEquipShop);
        //btnContinuePartRoad.onClick.AddListener(TouchContinueRoad);
        btnCLoseNewItem.onClick.AddListener(TouchCloseAttr);
        InitViews();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnEnable()
    {
        // Подписываемся на событие
        //EquipButtons.OnNeedFindSprite += FindSprite;
        GetNewItemPopUp.OnNeedFindSprite += attrShop.FindSprite;
        
    }

    void OnDisable()
    {
        // Отписываемся от события
        //RewGetEquip.OnEquipRewPressed  -= InitFunct;
        //EquipButtons.OnNeedFindSprite -= FindSprite;
        GetNewItemPopUp.OnNeedFindSprite -= attrShop.FindSprite;;
        
    }
    
    
    
    [Header("AttributesPopup")] 
    [SerializeField] private GameObject attrPopup;
    [SerializeField] private AttrShop attrShop;
    [SerializeField] Button btnCloseAttrPopup;
   // [SerializeField] private List<Vector3> HatPositions;
   
   [Header("RewardsButtZone")] 
   [SerializeField] private GameObject rewZone;
   [SerializeField] Button btnEquipShop;
   
   [Header("PartRoadWdg")] 
   [SerializeField] private PartRoadCompleted partRoadWgt;
   [SerializeField] Button btnContinuePartRoad;
   
   [Header("GetNewItemPopUp")] 
   [SerializeField] private GetNewItemPopUp newItemPopup;
   [SerializeField] Button btnCLoseNewItem;


   public void InitViews()
   {
       attrPopup.SetActive(false);
       partRoadWgt.gameObject.SetActive(false);
       rewZone.SetActive(true);
       newItemPopup.gameObject.SetActive(false);
   }

   private void TouchCloseAttr()
   {
       SoundManager.instance.PlaySound_ButtClick();
      // SoundManager.instance.PlaySound_ButtClick();
       InitViews();
   }
   
   private void TouchEquipShop()
   {
       SoundManager.instance.PlaySound_ButtClick();
       attrPopup.SetActive(true);
       partRoadWgt.gameObject.SetActive(false);
       rewZone.SetActive(false);
   }
   
   private void TouchContinueRoad()
   {

        
       Debug.Log("AddInterHere");
       partRoadWgt.gameObject.SetActive(false);
       // OnPartRoadCompletedActive();
   }
   public void StartPartRoadWdg()
   {
       OnDisableCharClick();
       CrazySDK.Game.GameplayStop();
       CrazySDK.Game.HappyTime();
       Debug.Log("setPartRoadActive");
       Time.timeScale = 0f;
       int tutN = Config.GetTutN();
       partRoadWgt.gameObject.SetActive(true);
       if (tutN==1)
       {
           //partRoadWgt.Tut1.SetActive(true);
           partRoadWgt.defaultView.SetActive(false);
           partRoadWgt.AnimateAppearance(partRoadWgt.Tut1, 900, new Vector3(-600,100,0), 0.3f);
       }
       else if (tutN==2)
       {
           // partRoadWgt.Tut2.SetActive(true);
           partRoadWgt.defaultView.SetActive(false);
           partRoadWgt.AnimateAppearance(partRoadWgt.Tut2, 900, new Vector3(-600,100,0), 0.3f);
       }
       else if (tutN==3)
       {
           //partRoadWgt.Tut3.SetActive(true);
           partRoadWgt.defaultView.SetActive(false);
           partRoadWgt.AnimateAppearance(partRoadWgt.Tut3, 900, new Vector3(-600,100,0), 0.3f);
       }
       else
       {
          // partRoadWgt.defaultView.SetActive(true); 
          partRoadWgt.defaultView.SetActive(false);
           partRoadWgt.AnimateAppearance(partRoadWgt.defaultView, 900, new Vector3(-600,100,0), 0.3f);
       }
       // OnPartRoadCompletedActive();
   }

   public void ShowNewItemPopUp(string group, int equipN)
   {
       Debug.Log("NEW ITEM POPUP SHOW");
       OnDisableCharClick();
       CrazySDK.Game.GameplayStop();
       attrPopup.gameObject.SetActive(false);
       rewZone.gameObject.SetActive(false);
       newItemPopup.gameObject.SetActive(true);
       newItemPopup.ShowGetNewItemPopUp(group,equipN);
   }

}
