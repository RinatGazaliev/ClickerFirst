using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipButtons : MonoBehaviour
{
    [SerializeField] private int equipN;
    [SerializeField] private string equipGroup;

    private int currScenario;
    private string fullPlayerPrefsName;
    private string anyGroupItemEquippedName;

    private Button btnSelf;
    // Start is called before the first frame update
    void Start()
    {
        fullPlayerPrefsName = $"Equip_{equipGroup}_N_{equipN}";
        anyGroupItemEquippedName = $"AnyItemEquipped_{equipGroup}";
        btnSelf = GetComponent<Button>();
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
            if ( PlayerPrefs.GetInt(anyGroupItemEquippedName, 0)==0)
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
    }
}
