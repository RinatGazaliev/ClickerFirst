using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class YG2RewardManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static YG2RewardManager instance;
    //loosePopupContinue
    public Action RewAutoClickStart;
    public Action RewAutoClickFinish;
    
    //GetClues
    public Action RewDoubleCoinsStart;
    public Action RewDoubleCoinsFinish;
    //getCoinsShop
    public Action RewMoveBoosterStart;
    public Action RewMoveBoosterFinish;
    
    //GetCrystShop
    public Action RewGetEquipStart;
    public Action RewGetEquipFinish;
    
    /*
    //WinAdditionalCoins
    public Action GetWinAdditionalCoinsStart;
    public Action GetWinAdditionalCoinsFinish;
    */

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this);
    }
    private void OnEnable()
    {
        YG2.onRewardAdv += OnRewardFinish;
        YG2.onOpenRewardedAdv += OnRewardStart;
        YG2.onCloseRewaededAdv += OnRewardClose;
        YG2.onErrorRewardedAdv += OnRewardClose;
    }

    // Необходимо отписывать методы от событий при деактивации объекта
    private void OnDisable()
    {
        YG2.onRewardAdv -= OnRewardFinish;
        YG2.onOpenRewardedAdv -= OnRewardStart;
        YG2.onCloseRewaededAdv -= OnRewardClose;
        YG2.onErrorRewardedAdv -= OnRewardClose;
    }
    
    // Когда объект с данным классом станет активным, метод OnReward подпишится на событие вознаграждения

    // Вызов рекламы за вознаграждение
    public void MyRewardAdvShow(string id)
    {
        YG2.RewardedAdvShow(id);
        YG2.onOpenRewardedAdv = OnRewardStart;
    }

    // Метод подписан на событие OnReward (ивент вознаграждения)
    private void OnRewardFinish(string id)
    {
        
        // Проверяем ID вознаграждения. Если совпадает с тем ID, с которым вызывали рекламу, то вознаграждаем.
        if (id == "AutoClick")
        {
            // Получение вознаграждения
            RewAutoClickFinish?.Invoke();
            MusicManager.instance.EnableMusic();
            SoundManager.instance.EnableSound();
            MusicManager.instance.isSwapLocked = false;
        }
        if (id == "DoubleCoins")
        {
            // Получение вознаграждения
            RewDoubleCoinsFinish?.Invoke();
            MusicManager.instance.EnableMusic();
            SoundManager.instance.EnableSound();
            MusicManager.instance.isSwapLocked = false;
        }
        if (id == "MoveBoost")
        {
            // Получение вознаграждения
            RewMoveBoosterFinish?.Invoke();
            MusicManager.instance.EnableMusic();
            SoundManager.instance.EnableSound();
            MusicManager.instance.isSwapLocked = false;
        }
        if (id == "GetEquip")
        {
            // Получение вознаграждения
            RewGetEquipFinish?.Invoke();
            MusicManager.instance.EnableMusic();
            SoundManager.instance.EnableSound();
            MusicManager.instance.isSwapLocked = false;
        }
   
    }
    private void OnRewardStart(string id)
    {
        // Проверяем ID вознаграждения. Если совпадает с тем ID, с которым вызывали рекламу, то вознаграждаем.
        if (id == "AutoClick")
        {
            
            // Получение вознаграждения
            RewAutoClickStart?.Invoke();
            
        }
        if (id == "DoubleCoins")
        {
            // Получение вознаграждения
            RewDoubleCoinsStart?.Invoke();
            
        }
        if (id == "MoveBoost")
        {
            // Получение вознаграждения
            RewMoveBoosterStart?.Invoke();
            
        }
        if (id == "GetEquip")
        {
            // Получение вознаграждения
            RewGetEquipStart?.Invoke();
            
        }

    }
    
    private void OnRewardClose()
    {
        MusicManager.instance.EnableMusic();
        SoundManager.instance.EnableSound();
        MusicManager.instance.isSwapLocked = false;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
