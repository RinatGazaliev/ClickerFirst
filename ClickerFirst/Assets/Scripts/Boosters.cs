using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boosters : MonoBehaviour
{
    [SerializeField] private Text txtDescrBooster;
    public static event Action OnBoosterClick = delegate() { };
    
    
    [SerializeField] private int boosterN;
    //[SerializeField] private bool isMoveBoost;
    //[SerializeField] private bool isPerClick;
    
    
    private Text txtPrice;
    //private bool isPerClick;
    
[Header ("BoostParams")]
    private int price;
    [SerializeField] private int perSecBoostValue;
    [SerializeField] private int perClickBoostValue;
    [SerializeField] private float distanceBoostValue;

    private int pushedN;

    private Button btnBooster;
    // Start is called before the first frame update
   
    void Start()
    {
        pushedN = Config.GetBoosterPushedN(boosterN);
        GetPriceValue();
        SetTextPrice();
        SetDescrText();
        
        btnBooster = GetComponentInChildren<Button>();
        //txtPrice = gameObject.tex.Find("Price");
       
        if (btnBooster!=null)
        {
            
           btnBooster.onClick.AddListener(BoosterTouch);
           CheckState(Config.GetTotalScore());
          // SetStartButtParams();
        }
    }

    private void OnEnable()
    {
        Config.OnChangeTotalScore += CheckState;
    }
    
    private void OnDisable()
    {
        Config.OnChangeTotalScore -= CheckState;
    }
    
    
    private void SetTextPrice()
    {

            // Находим дочерний объект с именем "Price"
            Transform priceTransform = transform.Find("Price");
            if (priceTransform != null)
            {
                // Пытаемся получить стандартный Text
                txtPrice = priceTransform.GetComponent<Text>();
                if (txtPrice != null)
                {
                    UpdateTextPriceValue();
                    Debug.Log($"Найден стандартный Text");
                }
                else
                {
                    Debug.LogWarning("Объект 'Price' найден, но не содержит компонента Text или TMP_Text!");

                }
            }

    }
    
    private void SetDescrText()
    {

        if (perSecBoostValue>0)
        {
            txtDescrBooster.text = $"+{perSecBoostValue*(pushedN+1)} sousagoids per second";
        }
        if (perClickBoostValue>0)
        {
            txtDescrBooster.text = $"+{perClickBoostValue*(pushedN+1)} sousagoids per click";
        }
        if (distanceBoostValue>0f)
        {
            txtDescrBooster.text = $"+{distanceBoostValue*(pushedN+1)*100} cm to step lengh";
        }

    }

    private void UpdateTextPriceValue()
    {
        txtPrice.text = price.ToString();
    }

    private void GetPriceValue()
    {
        pushedN = Config.GetBoosterPushedN(boosterN);
       // int priceKf = (int)Math.Pow(2, (pushedN));
        price =  Config.GetBoosterPrice(boosterN)*(pushedN+1) + GetBoosterPriceCumulative()*100;
        //Debug.Log("CurrPriceButtValue"+priceKf);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CheckState(int totalScore)
    {
        if (price <= totalScore)
        {
            btnBooster.interactable = true;
            Debug.Log("ButtIsActive"+boosterN);
        }
        else
        {
            btnBooster.interactable = false;
            Debug.Log("ButtIsNotActive"+boosterN);
        }
        //btnBooster.interactable = price <= totalScore;
    }

    /*private void SetStartButtParams()
    {
        switch (boosterN)
        {
            case 0:
                isPerClick = true;
                isMoveBoost = false;
                perClickBoostValue = 1;
                break;
            case 1:
                isPerClick = false;
                isMoveBoost = false;
                perSecBoostValue = 1;
                break;
            case 2:
                isPerClick = false;
                isMoveBoost = true;
                distanceBoostValue = 0.91f;
                break;
            case 3:
                isPerClick = true;
                isMoveBoost = false;
                perClickBoostValue = 10;
                break;
            case 4:
                isPerClick = false;
                isMoveBoost = false;
                perSecBoostValue = 10;
                break;
            case 5:
                isPerClick = false;
                isMoveBoost = true;
                distanceBoostValue = 0.1f;
                break;
                
        }
    }*/

    private void BoosterTouch()
    {
        SoundManager.instance.PlaySound_ButtClick();
        pushedN = pushedN + 1;
        Config.SetBoosterPushedN(boosterN);
        Config.SetDistanceBoostKf(Config.GetDistanceBoostKf()+distanceBoostValue);

       Config.SetScorePerClick(Config.GetScorePerClick()+perClickBoostValue*pushedN);

        Config.SetScorePerSec(Config.GetScorePerSec()+perSecBoostValue*pushedN);
        
        

       
        int currTotalScore = Config.GetTotalScore();
        currTotalScore = currTotalScore - price;
        Config.SetTotalScore(currTotalScore);
        
        GetPriceValue();
        UpdateTextPriceValue();
        CheckState(currTotalScore);
        SetDescrText();

        OnBoosterClick();

    }

    private int GetBoosterPriceCumulative()
    {
        int step = 0;
        if (perClickBoostValue>0)
        {
            step = perClickBoostValue;
        }
        else if (perSecBoostValue>0)
        {
            step = perSecBoostValue;
        }
        else if (distanceBoostValue>0)
        {
            step = (int)(distanceBoostValue*100f);
        }
        
        int cumulativeSum = CumulativeSumStepCalculator.GetCumulativeSum(step,pushedN);
        
        Debug.Log($"Кумулятивная сумма для последовательности с шагом 5 до индекса {pushedN} равна {cumulativeSum}");
        return cumulativeSum;
    }
}
