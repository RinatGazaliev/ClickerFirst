using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerShowFlag : MonoBehaviour
{
    [SerializeField] private float duration = 180f; // Длительность таймера
    private float timeRemaining;
    private bool isRunning;

    public static event Action OnTimerEnd; // Делегат события завершения

    private void Start()
    { 
       ResetTimer();
        
       int CheckState = PlayerPrefs.GetInt("FlagTimerShouldGo",0);
       if (CheckState==1)
       {
           StartTimer();
       }

      
    }

    private void Update()
    {
        if (!isRunning) return;

        timeRemaining -= Time.deltaTime;
        if (timeRemaining <= 0)
        {
            isRunning = false;
            OnTimerEnd?.Invoke();
        }
    }

    public void StartTimer()
    {
        if (!isRunning)
        {
            isRunning = true;
            timeRemaining = duration;
        }
    }

    public void ResetTimer()
    {
        isRunning = false;
        timeRemaining = duration;
    }

    private void OnEnable()
    {
        ParallaxBGMove.OnFinishFinalTut += ActivateFinishTimer;
        MovingRoad.OnFlagWgtShown += CheckStartTimer;
    }

    private void OnDisable()
    {
        ParallaxBGMove.OnFinishFinalTut -= ActivateFinishTimer;
        MovingRoad.OnFlagWgtShown -= CheckStartTimer;
    }

    private void ActivateFinishTimer()
    {
        PlayerPrefs.SetInt("FlagTimerShouldGo",1);
        StartTimer();
    }
    private void CheckStartTimer()
    {
        int CheckState = PlayerPrefs.GetInt("FlagTimerShouldGo",0);
        if (CheckState==1)
        {
            StartTimer();
        }

    }
}
