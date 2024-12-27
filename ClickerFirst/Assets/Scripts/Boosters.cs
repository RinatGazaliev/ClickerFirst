using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boosters : MonoBehaviour
{
    [SerializeField] private int boosterN;
    private Text txtPrice;
    private bool isPerClick;
    private bool isMoveBoost;

    private int price;
    private int perSecBoostValue;
    private int perClickBoostValue;
    private float distanceBoostValue;

    private int pushedN;

    private Button btnBooster;
    // Start is called before the first frame update
   
    void Start()
    {
        pushedN = Config.GetBoosterPushedN(boosterN);
        GetPriceValue();
        SetTextPrice();
        
        btnBooster = GetComponentInChildren<Button>();
        //txtPrice = gameObject.tex.Find("Price");
       
        if (btnBooster!=null)
        {
            
           btnBooster.onClick.AddListener(BoosterTouch);
           CheckState(Config.GetTotalScore());
           SetStartButtParams();
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

    private void UpdateTextPriceValue()
    {
        txtPrice.text = price.ToString();
    }

    private void GetPriceValue()
    {
        pushedN = Config.GetBoosterPushedN(boosterN);
        int priceKf = (int)Math.Pow(2, (pushedN));
        price = Config.GetBoosterPrice(boosterN) * priceKf;
        Debug.Log("CurrPriceButtValue"+priceKf);
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

    private void SetStartButtParams()
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
                distanceBoostValue = 0.01f;
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
    }

    private void BoosterTouch()
    {
        
        Config.SetBoosterPushedN(boosterN);
        if (isMoveBoost)
        {
            Config.SetScorePerClick(Config.GetScorePerClick()+perClickBoostValue);
        }
        else
        {
            if (isPerClick)
            {
                //int currPerClick = Config.GetScorePerClick() + perClickBoostValue;
            
                Config.SetScorePerClick(Config.GetScorePerClick()+perClickBoostValue);
                Debug.Log("ButtPerClick");
            }
            else
            {
                Config.SetScorePerSec(Config.GetScorePerSec()+perSecBoostValue);
                Debug.Log("ButtPerSec");
            }
        }
       
        int currTotalScore = Config.GetTotalScore();
        currTotalScore = currTotalScore - price;
        Config.SetTotalScore(currTotalScore);
        
        GetPriceValue();
        UpdateTextPriceValue();
        CheckState(currTotalScore);

    }
}
