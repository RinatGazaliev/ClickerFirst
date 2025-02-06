using System;
using System.Collections;
using System.Collections.Generic;
using Gley.Localization;
using Gley.Localization.Internal;
using UnityEngine;
using UnityEngine.UI;

public class MyLocalizationManager : MonoBehaviour
{
   // [SerializeField] private SupportedLanguages selectedLanguage = SupportedLanguages.English;
    [SerializeField] private List<Text> arrayTextToTranslate; 
    [SerializeField] private List<string> arrayNameInLocManager;
    public static event Action  OnTranslateEnds;
    
    // Start is called before the first frame update
    void Start()
    {
       // API.SetCurrentLanguage(API.GetCurrentLanguage());
        TranslateAllText();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void TranslateAllText()
    {
        Debug.Log("StartTranslation");
        int i = 0;
        foreach (var textToTranslate in arrayTextToTranslate)
        {
            //API.GetText(userAtrManager.EquipNameWordId[_currInfoItemUserAtr.itemN]);
            textToTranslate.text = API.GetText(arrayNameInLocManager[i]);
           
            i = i + 1;
        }

        OnTranslateEnds();
    }

    private void OnEnable()
    {
        //LocalizationManager.OnTranslateStarts += TranslateAllText;
    }
    private void OnDisable()
    {
        //LocalizationManager.OnTranslateStarts -= TranslateAllText;
    }
}
