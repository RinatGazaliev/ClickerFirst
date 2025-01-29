using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardManager : MonoBehaviour
{
    // Start is called before the first frame update
    private float autoClickLastTime;
    private float totalTimerAutoclick;
    private float totalTimerDoublePoints;
    private float totalTimerMoveBoost;
    void Start()
    {
        totalTimerAutoclick = timerAutoclick;
        totalTimerDoublePoints = timerDoublePoints;
        totalTimerMoveBoost = timerMoveBoosts;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isTimerAutoClickRunning)
        {
            timerAutoclick -= Time.deltaTime;
            if (timerAutoclick <= 0f)
            {
                //timerAutoclick = 0f;
                isTimerAutoClickRunning = false;
                timerAutoclick = totalTimerAutoclick;
                btnReward_Autoclick.gameObject.SetActive(true);
                btnReward_Autoclick.InitViews();
            }
        }
        
        if (isTimerDoubleCoinsRunning)
        {
            timerDoublePoints -= Time.deltaTime;
            if (timerDoublePoints <= 0f)
            {
                //timerDoublePoints = 0f;
                isTimerDoubleCoinsRunning = false;
                timerDoublePoints = totalTimerDoublePoints;
                btnReward_DoublePoints.gameObject.SetActive(true);
                btnReward_DoublePoints.InitViews();
            }
        }
        
        if (isTimerMoveBoostRunning)
        {
            timerMoveBoosts -= Time.deltaTime;
            if (timerMoveBoosts <= 0f)
            {
                //timerMoveBoosts = 0f;
                isTimerMoveBoostRunning = false;
                timerMoveBoosts = totalTimerMoveBoost;
                btnReward_MoveBoost.gameObject.SetActive(true);
                btnReward_MoveBoost.InitViews();
            }
        }
        if (isTimerGetEquipRunning)
        {
            timerGetEquip -= Time.deltaTime;
            if (timerGetEquip <= 0f)
            {
                //timerMoveBoosts = 0f;
                isTimerGetEquipRunning = false;
                timerGetEquip = totalTimerMoveBoost;
                btnReward_GetEquip.gameObject.SetActive(true);
                //btnReward_GetEquip.InitViews();
            }
        }
        
    }

    [Header("RewardAutoClick")] 
    [SerializeField] private RewAutoClicker btnReward_Autoclick;
    [SerializeField] private float timerAutoclick;
    private bool isTimerAutoClickRunning = false;
    
    //TODO Добавить делегат на ревард клик
    
    [Header("Reward_x2Points")] 
    [SerializeField] private RewDoublePoints btnReward_DoublePoints;
    [SerializeField] private float timerDoublePoints;
    private bool isTimerDoubleCoinsRunning = false;
    //TODO Добавить делегат на ревард клик
    
    [Header("Reward_MoveBoost")] 
    [SerializeField] private RewMoveBoost btnReward_MoveBoost;
    [SerializeField] private float timerMoveBoosts;
    private bool isTimerMoveBoostRunning = false;
    //TODO Добавить делегат на ревард кл
    
    [Header("RewardEquip")] 
    [SerializeField] private RewGetEquip btnReward_GetEquip;
    [SerializeField] private float timerGetEquip;
    private bool isTimerGetEquipRunning = false;

    private void OnEnable()
    {
       RewAutoClicker.OnRewardAutoClickTimeFinish+=UpdateAutoClickRewardTimer;
       RewDoublePoints.OnRewardTimerUpdate+=UpdateDoublePointsRewardTimer;
       RewMoveBoost.OnRewardTimerUpdate+=UpdateMoveBoostRewardTimer;
       RewGetEquip.OnRewardTimerUpdate+=UpdateGetEquipRewardTimer;
    }

    private void OnDisable()
    {
        RewAutoClicker.OnRewardAutoClickTimeFinish-=UpdateAutoClickRewardTimer;
        RewDoublePoints.OnRewardTimerUpdate-=UpdateDoublePointsRewardTimer;
        RewMoveBoost.OnRewardTimerUpdate-=UpdateMoveBoostRewardTimer;
        RewGetEquip.OnRewardTimerUpdate -= UpdateGetEquipRewardTimer;
    }

    private void UpdateAutoClickRewardTimer()
    {
        btnReward_Autoclick.gameObject.SetActive(false);
        isTimerAutoClickRunning = true;
    }
    private void UpdateDoublePointsRewardTimer(bool _isRewardUpdate)
    {
        if (_isRewardUpdate)
        {
            btnReward_DoublePoints.gameObject.SetActive(false);
            isTimerDoubleCoinsRunning = true;
            
        }
        else
        {
            btnReward_DoublePoints.gameObject.SetActive(true);
            btnReward_DoublePoints.InitViews();
        }

    }
    private void UpdateMoveBoostRewardTimer(bool _isRewardUpdate)
    {
        if (_isRewardUpdate)
        {
            btnReward_MoveBoost.gameObject.SetActive(false);
            isTimerMoveBoostRunning = true;
        }
        else
        {
            btnReward_MoveBoost.gameObject.SetActive(true);
            btnReward_MoveBoost.InitViews();
        }

    }
    
    private void UpdateGetEquipRewardTimer(bool _isRewardUpdate)
    {
        if (_isRewardUpdate)
        {
            btnReward_GetEquip.gameObject.SetActive(false);
            isTimerGetEquipRunning = true;
            
        }
        else
        {
            btnReward_GetEquip.gameObject.SetActive(true);
        }

    }
}
