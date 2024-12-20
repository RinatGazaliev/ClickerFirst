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
        InitViews();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    [Header("AttributesPopup")] 
    [SerializeField] private GameObject attrPopup;
    [SerializeField] Button btnCloseAttrPopup;
   // [SerializeField] private List<Vector3> HatPositions;
   
   [Header("RewardsButtZone")] 
   [SerializeField] private GameObject rewZone;
   [SerializeField] Button btnEquipShop;
   
   [Header("PartRoadWdg")] 
   [SerializeField] private PartRoadCompleted partRoadWgt;
   [SerializeField] Button btnContinuePartRoad;


   private void InitViews()
   {
       attrPopup.SetActive(false);
       partRoadWgt.gameObject.SetActive(false);
       rewZone.SetActive(true);
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

}
