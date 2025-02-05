using System;
using System.Collections;
using System.Collections.Generic;
using Gley.Localization;
using UnityEngine;
using UnityEngine.UI;

public class MyLocalizationManager : MonoBehaviour
{
    [SerializeField] private SupportedLanguages selectedLanguage = SupportedLanguages.English;
    [SerializeField] private List<Text> arrayTextToTranslate; 
    [SerializeField] private List<string> arrayNameInLocManager;
    public static event Action  OnTranslateEnds;
    
    // Start is called before the first frame update
    void Start()
    {
        API.SetCurrentLanguage(selectedLanguage);
        TranslateAllText();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void TranslateAllText()
    {
        int i = 0;
        foreach (var textToTranslate in arrayTextToTranslate)
        {
            //API.GetText(userAtrManager.EquipNameWordId[_currInfoItemUserAtr.itemN]);
            textToTranslate.text = API.GetText(arrayNameInLocManager[i]);
           
            i = i + 1;
        }

        OnTranslateEnds();
    }
}
