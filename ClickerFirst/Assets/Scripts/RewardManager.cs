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

    private void OnEnable()
    {
       RewAutoClicker.OnRewardAutoClickTimeFinish+=UpdateAutoClickRewardTimer;
       RewDoublePoints.OnRewardDoublePointsTimeFinish+=UpdateDoublePointsRewardTimer;
       RewMoveBoost.OnRewardMoveBoostTimeFinish+=UpdateMoveBoostRewardTimer;
    }

    private void OnDisable()
    {
        RewAutoClicker.OnRewardAutoClickTimeFinish-=UpdateAutoClickRewardTimer;
        RewDoublePoints.OnRewardDoublePointsTimeFinish-=UpdateDoublePointsRewardTimer;
        RewMoveBoost.OnRewardMoveBoostTimeFinish-=UpdateMoveBoostRewardTimer;
    }

    private void UpdateAutoClickRewardTimer()
    {
        btnReward_Autoclick.gameObject.SetActive(false);
        isTimerAutoClickRunning = true;
    }
    private void UpdateDoublePointsRewardTimer()
    {
        btnReward_DoublePoints.gameObject.SetActive(false);
        isTimerDoubleCoinsRunning = true;
    }
    private void UpdateMoveBoostRewardTimer()
    {
        btnReward_MoveBoost.gameObject.SetActive(false);
        isTimerMoveBoostRunning = true;
    }
}
