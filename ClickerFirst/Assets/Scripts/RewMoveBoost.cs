using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class RewMoveBoost : MonoBehaviour
{
     private bool isMoveBoostRunning = false;
    private float moveBoostDuration = 15f; 
    //private float autoClickInterval = 0.5f; // Интервал между событиями OnAutoClick
    //private float autoClickTimer = 0f;
    private Button btnSelf;
    [SerializeField] private Image imgTV;

    [SerializeField] private float kickInterval = 5f;
    [SerializeField] private Slider timerSlider;// Таймер для события OnAutoClick
    [SerializeField] private Animator animContrCharacter;
    [SerializeField] private string YGRewardID;
    
    public static event Action OnRewardMoveBoostTimeFinish;
    public static event Action OnRewardStarted;
    public static event Action <bool> OnRewardTimerUpdate;
    public static event Action OnKickCalled;
    
    
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
    
    private IEnumerator StartAutoClickTimer(bool _isRewardUpdate)
    {
        //animContrCharacter.speed=1.3f;
        animContrCharacter.SetFloat("walkSpeed",1.3f);
        animContrCharacter.SetFloat("runSpeed",1.3f);
       // animContrCharacter.SetBool("isKicked", true);
        isMoveBoostRunning = true;
        Config.SetMoveBoostRewValue(1.5f);
        Debug.Log("Auto-click started");
        float elapsedTime = 0f;
        float timerKick = 0f;
        OnRewardStarted();
        
        while (elapsedTime < moveBoostDuration)
        {
            elapsedTime += Time.deltaTime;
            timerKick += Time.deltaTime;

            // Обновляем значение слайдера
            if (timerSlider != null)
            {
                timerSlider.value = 1f - (elapsedTime / moveBoostDuration);
            }
            if (timerKick >= kickInterval)
            {
                timerKick = 0f; // Сброс таймера
                //animContrCharacter.SetTrigger("KickAnim");
                OnKickCalled();
            }

            yield return null;
        }

        if (timerSlider != null)
        {
            timerSlider.value = 0f;
        }
        
        Config.SetMoveBoostRewValue(1f);
        isMoveBoostRunning = false;
        //animContrCharacter.SetBool("isKicked", false);
       // animContrCharacter.Play("1_Idle_1");
        //animContrCharacter.speed=1f;
        if (Config.GetTutN()>=3)
        {
            LeftButtZoneManager.instance.equipShop.gameObject.SetActive(true);  
        }
        OnRewardMoveBoostTimeFinish(); 
        OnRewardTimerUpdate(_isRewardUpdate);
        
       
        
        Debug.Log("Auto-click ended");
    }
    
    private void OnRewardGain (bool _isRewardUpdate)
    {
        timerSlider.gameObject.SetActive(true);
        btnSelf.interactable = false;
        imgTV.gameObject.SetActive(false);
        LeftButtZoneManager.instance.equipShop.gameObject.SetActive(false);
        
        if (!isMoveBoostRunning) // Если таймер ещё не запущен
        {
            StartCoroutine(StartAutoClickTimer(_isRewardUpdate));
        }
    }
    private void OnTutAnimFinishedMoveBoost(string tutName)
    {
        if (tutName == "Tut2")
        {
            OnRewardGain(false);
        }
    }
    public void InitViews()
    {
        timerSlider.gameObject.SetActive(false);
        btnSelf.interactable = true;
        imgTV.gameObject.SetActive(true);
    }
    private void GetRewardFinish()
    {
        OnRewardGain(true);
    }
    private void OnEnable()
    {
        YG2RewardManager.instance.RewMoveBoosterFinish += GetRewardFinish;
        LeftButtZoneManager.OnTutAnimFinished += OnTutAnimFinishedMoveBoost;
        //YG2RewardManager.instance.RewAutoClickStart += TouchContinue_VideoRewardClosed;
    }
    private void OnDisable()
    {
        YG2RewardManager.instance.RewMoveBoosterFinish -= GetRewardFinish;
        LeftButtZoneManager.OnTutAnimFinished -= OnTutAnimFinishedMoveBoost;
        //YG2RewardManager.instance.RewAutoClickStart -= TouchContinue_VideoRewardClosed;
    }
    private void CallRewVideo()
    {
        SoundManager.instance.PlaySound_ButtClick();
        MusicManager.instance.DisableMusic();
        SoundManager.instance.DisableSound();
        MusicManager.instance.isSwapLocked = true;
        YG2.RewardedAdvShow(YGRewardID);
    }

}
