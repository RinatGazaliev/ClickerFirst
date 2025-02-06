using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YG;


public class RewDoublePoints : MonoBehaviour
{
     private bool isDoublePointsRunning = false;
    private float doublePointsDuration = 15f; 
    //private float autoClickInterval = 0.5f; // Интервал между событиями OnAutoClick
    //private float autoClickTimer = 0f;
    private Button btnSelf;
    [SerializeField] private Slider timerSlider;// Таймер для события OnAutoClick
    [SerializeField] private string YGRewardID;
    [SerializeField] private Image imgTV;
    
   // public static event Action OnRewardDoublePointsTimeFinish;
    public static event Action <bool> OnRewardTimerUpdate;
    //public static event Action OnAutoClickerClick;
    // Start is called before the first frame update
    void Start()
    {
        if (timerSlider != null)
        {
            timerSlider.minValue = 0f;
            timerSlider.maxValue = 1f;
            timerSlider.value = 1f;
        }
        btnSelf=GetComponent<Button>();
        btnSelf.onClick.AddListener(CallRewVideo);
        InitViews();
    }

    // Update is called once per frame
    void Update()
    {
   
    }
    
    private IEnumerator StartAutoClickTimer(bool _isUpdateReward)
    {
        isDoublePointsRunning = true;
        Config.SetDoublePointsRewValue(2);
        Debug.Log("Auto-click started");
        float elapsedTime = 0f;

        while (elapsedTime < doublePointsDuration)
        {
            elapsedTime += Time.deltaTime;

            // Обновляем значение слайдера
            if (timerSlider != null)
            {
                timerSlider.value = 1f - (elapsedTime / doublePointsDuration);
            }

            yield return null;
        }

        if (timerSlider != null)
        {
            timerSlider.value = 0f;
        }
        
        Config.SetDoublePointsRewValue(1);
        isDoublePointsRunning = false;
        //OnRewardDoublePointsTimeFinish();
        LeftButtZoneManager.instance.equipShop.gameObject.SetActive(true);
        OnRewardTimerUpdate(_isUpdateReward);
      
       
        
        Debug.Log("Auto-click ended");
    }
    private void OnTutAnimFinishedDoubleCoins(string tutName)
    {
        if (tutName == "Tut1")
        {
            OnRewardGain(false);
        }
    }

    private void GetRewardFinish()
    {
        OnRewardGain(true);
    }

    private void OnEnable()
    {
        YG2RewardManager.instance.RewDoubleCoinsFinish += GetRewardFinish;
        LeftButtZoneManager.OnTutAnimFinished += OnTutAnimFinishedDoubleCoins;
        //YG2RewardManager.instance.RewAutoClickStart += TouchContinue_VideoRewardClosed;
    }
    private void OnDisable()
    {
        YG2RewardManager.instance.RewDoubleCoinsFinish -= GetRewardFinish;
        LeftButtZoneManager.OnTutAnimFinished -= OnTutAnimFinishedDoubleCoins;
        //YG2RewardManager.instance.RewAutoClickStart -= TouchContinue_VideoRewardClosed;
    }
    private void CallRewVideo()
    {
        SoundManager.instance.PlaySound_ButtClick();
        MusicManager.instance.DisableMusic();
        SoundManager.instance.DisableSound();
        YG2.RewardedAdvShow(YGRewardID);
    }
    private void OnRewardGain (bool _isUpdateReward)
    {
        timerSlider.gameObject.SetActive(true);
        btnSelf.interactable = false;
        imgTV.gameObject.SetActive(false);
        LeftButtZoneManager.instance.equipShop.gameObject.SetActive(false);
        
        if (!isDoublePointsRunning) // Если таймер ещё не запущен
        {
            StartCoroutine(StartAutoClickTimer(_isUpdateReward));
        }
    }

    public void InitViews()
    {
        timerSlider.gameObject.SetActive(false);
        btnSelf.interactable = true;
        imgTV.gameObject.SetActive(true);
    }
}
