using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ShowWgtManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        btnCloseAttrPopup.onClick.AddListener(TouchCloseAttr);
        btnEquipShop.onClick.AddListener(TouchEquipShop);
        btnContinuePartRoad.onClick.AddListener(TouchContinueRoad);
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


   private void InitViews()
   {
       attrPopup.SetActive(false);
       partRoadWgt.gameObject.SetActive(false);
       rewZone.SetActive(true);
       newItemPopup.gameObject.SetActive(false);
   }

   private void TouchCloseAttr()
   {
       InitViews();
   }
   
   private void TouchEquipShop()
   {
       attrPopup.SetActive(true);
       partRoadWgt.gameObject.SetActive(false);
       rewZone.SetActive(false);
   }
   
   private void TouchContinueRoad()
   {
       Time.timeScale = 1f;
        
       Debug.Log("AddInterHere");
       partRoadWgt.gameObject.SetActive(false);
       // OnPartRoadCompletedActive();
   }
   public void StartPartRoadWdg()
   {
       Time.timeScale = 0f; 
       partRoadWgt.gameObject.SetActive(true);
       // OnPartRoadCompletedActive();
   }

   public void ShowNewItemPopUp(string group, int equipN)
   {
       rewZone.gameObject.SetActive(false);
       newItemPopup.gameObject.SetActive(true);
       newItemPopup.ShowGetNewItemPopUp(group,equipN);
   }

}
