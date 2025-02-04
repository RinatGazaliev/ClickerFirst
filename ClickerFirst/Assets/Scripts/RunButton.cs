using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RunButton : MonoBehaviour
{
    [SerializeField] private MainObject mainChar;
    private Button btnRun;
    
    // Start is called before the first frame update
    void Start()
    {
        btnRun = GetComponent<Button>();
        btnRun.onClick.AddListener(CallClickMainObj);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CallClickMainObj()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
         
        mainChar.CallMainObjClicked(mousePosition);
    }
    
    private void OnEnable()
    {
        PartRoadCompleted.OnPartRoadCompletedClosed += ActivateMainObjClick;
        ShowWgtManager.OnDisableCharClick += InactivateMainObjClick;
        GetNewItemPopUp.OnCloseNewItemPopUp += ActivateMainObjClick;
    }
    private void OnDisable()
    {
        PartRoadCompleted.OnPartRoadCompletedClosed -= ActivateMainObjClick;
        ShowWgtManager.OnDisableCharClick -= InactivateMainObjClick;
        GetNewItemPopUp.OnCloseNewItemPopUp -= ActivateMainObjClick;
    }

    private void ActivateMainObjClick()
    {
        btnRun.interactable=true;
    }
    private void InactivateMainObjClick()
    {
        btnRun.interactable=false;
    }
    

}
