using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Config : MonoBehaviour
{
    
    private const string TUTNUMBER = "TutN";
    public static void SetTutN(int scoreValue)
    {
        PlayerPrefs.SetInt(TUTNUMBER, scoreValue);
        PlayerPrefs.Save();
       
    }
    
    public static int GetTutN()
    {
        return PlayerPrefs.GetInt(TUTNUMBER,0);
    }

    public static bool isRunning = false;
    
    // Start is called before the first frame update

    #region TotalScore

    private const string TOTALSCORE = "TotalScore";
    public static event Action<int> OnChangeTotalScore = delegate(int _scoreValue) { };

    public static void SetTotalScore(int scoreValue)
    {
        PlayerPrefs.SetInt(TOTALSCORE, scoreValue);
        PlayerPrefs.Save();
        OnChangeTotalScore(scoreValue);
    }

    public static int GetTotalScore()
    {
        return PlayerPrefs.GetInt(TOTALSCORE, 0);
    }

    #endregion

    #region ScorePerClick

    private const string SCOREPERCLICK = "Score_click";
   
    //public static event Action<int> OnChangeTotalScore = delegate (int _scoreValue) { };

    public static void SetScorePerClick(int scoreValue)
    {
        PlayerPrefs.SetInt(SCOREPERCLICK, scoreValue);
        PlayerPrefs.Save();
        //OnChangeTotalScore(scoreValue);
    }

    public static int GetScorePerClick()
    {
        Debug.Log("ScorePerClick" + PlayerPrefs.GetInt(SCOREPERCLICK, 1));
        int currScorePerClick = PlayerPrefs.GetInt(SCOREPERCLICK, 1);
        //int currScorePerClick = PlayerPrefs.GetInt(SCOREPERCLICK, 1) * PerClickScaleKf;
        return currScorePerClick;

    }

    #endregion
    #region DistanceBooster
    private const string DISTANCEBOOST = "Distance_boost";
    
    public static float GetDistanceBoostKf()
    {
        float currScorePerClick = PlayerPrefs.GetFloat(DISTANCEBOOST, 1);
        return currScorePerClick;

    }
    
    public static void SetDistanceBoostKf(float scoreValue)
    {
        PlayerPrefs.SetFloat(DISTANCEBOOST, scoreValue);
        PlayerPrefs.Save();
        //OnChangeTotalScore(scoreValue);
    }
    #endregion

    #region ScorePerSec

    public const string SCOREPERSEC = "Score_sec";
    //public static event Action<int> OnChangeTotalScore = delegate (int _scoreValue) { };

    public static void SetScorePerSec(int scoreValue)
    {
        PlayerPrefs.SetInt(SCOREPERSEC, scoreValue);
        PlayerPrefs.Save();
        //OnChangeTotalScore(scoreValue);
    }

    public static int GetScorePerSec()
    {
        Debug.Log("ScorePerClick" + PlayerPrefs.GetInt(SCOREPERSEC, 1));
        return PlayerPrefs.GetInt(SCOREPERSEC, 1);

    }

    #endregion


    #region TotalDistance

    private const string TOTALDISTANCE = "Distance_total";
    //public static event Action<int> OnChangeTotalScore = delegate (int _scoreValue) { };

    public static void SetTotalDistance(float distanceValue)
    {
        PlayerPrefs.SetFloat(TOTALDISTANCE, distanceValue);
        PlayerPrefs.Save();
        //OnChangeTotalScore(scoreValue);
    }

    public static float GetTotalDistance()
    {
        //Debug.Log("ScorePerClick" + PlayerPrefs.GetInt(TOTALDISTANCE, 0));
        return PlayerPrefs.GetFloat(TOTALDISTANCE, 0);

    }

    #endregion

    #region ChangeRoadAbdHeavenTexture
    private const string CURRENTTEXTUREROADNUMBER = "TextureRoad_N";
    public static readonly int[] DistanceToChangeTextureRoad = { 150, 300, 500, 800, 1050, 1200, 1300, 1400, 1750, 2250, 2750, 3300 };
    //private bool isHeavenMove = false;
    public static event Action<bool> OnChangeHeavenMove = delegate (bool _isHeaveMove) { };

    public static void SetRoadTextureCurrN(int currTextNumber)
    {
        //string CurrName = CURRENTTEXTUREROADNUMBER + LayerN;
        PlayerPrefs.SetInt(CURRENTTEXTUREROADNUMBER, currTextNumber);
        PlayerPrefs.Save();
        //OnChangeTotalScore(scoreValue);
    }
    public static int GetRoadTextureCurrN()
    {
        //Debug.Log("ScorePerClick" + PlayerPrefs.GetInt(TOTALDISTANCE, 0));
        return PlayerPrefs.GetInt(CURRENTTEXTUREROADNUMBER, 0);

    }
    public static void SetHeavenMove(bool _isHeavenMove)
    {
        //isHeavenMove = _isHeavenMove;
        OnChangeHeavenMove(_isHeavenMove);
        //OnChangeTotalScore(scoreValue);
    }
    
    

    #endregion
    #region ParallaxMove
    private const string CURRENTTEXTUREPARALLAX = "TextureParallax_LayerN";
    
    public static void SetParallaxTextureCurrN(int currTextNumber, int LayerN)
    {
        string CurrName = CURRENTTEXTUREPARALLAX + LayerN;
        PlayerPrefs.SetInt(CurrName, currTextNumber);
        PlayerPrefs.Save();
        //OnChangeTotalScore(scoreValue);
    }
    public static int GetParallaxTextureCurrN(int LayerN)
    {
        //Debug.Log("ScorePerClick" + PlayerPrefs.GetInt(TOTALDISTANCE, 0));
        string CurrName = CURRENTTEXTUREPARALLAX + LayerN;
        return PlayerPrefs.GetInt(CurrName, 0);

    }
    #endregion
    #region ForkParams

    public const string FORKADD = "ForkAddValue";

    public static void SetForkAddValue(float scoreValue)
    {
        PlayerPrefs.SetFloat(FORKADD, scoreValue);
        PlayerPrefs.Save();
        //OnChangeTotalScore(scoreValue);
    }

    public static float GetForkAddValue()
    {
        return PlayerPrefs.GetFloat(FORKADD, 0.05f);
    }


    private static int PerClickScaleKf = 1;
    public static event Action<int> OnChangePerClickScaleKf = delegate(int _scaleValue) { };

    public static int GetPerClickScaleKf()
    {
        return PerClickScaleKf;
    }

    public static void SetPerClickScaleKf(int scaleValue)
    {
        PerClickScaleKf = scaleValue;
        OnChangePerClickScaleKf(scaleValue);
    }
    
    
    

    #endregion

    #region Boosters

    private const string PUSHEDBOOSTER = "PushedBoosterN_";
    

    public static void SetBoosterPushedN(int boosterN) {
        string key = PUSHEDBOOSTER + boosterN ;
        int curPushedN=PlayerPrefs.GetInt(key, 0);
        curPushedN = curPushedN + 1;
        Debug.Log("CurrPriceButtValuePushedN"+curPushedN);
        PlayerPrefs.SetInt(key, curPushedN);
        PlayerPrefs.Save();
        //OnChangeTotalScore(scoreValue);
    }
    
    public static int GetBoosterPushedN(int boosterN) {
        string key = PUSHEDBOOSTER + boosterN ; // Формируем ключ
        return PlayerPrefs.GetInt(key, 0);
        
    }
    
    private static List<int> boosterPrice = new List<int> {50, 50, 200, 500, 500, 1000, 10000, 10000, 100000, 100000, 100000, 1000000, };
    public static int GetBoosterPrice(int boosterN) {
        return boosterPrice[boosterN];
    }
    
    
    #endregion
    
    #region RewardDoublePoints

    private static int rewDoublePoints = 1;

    public static void SetDoublePointsRewValue(int value)
    {
        rewDoublePoints = value;
    }

    public static int GetDoublePointsRewValue()
    {
        return rewDoublePoints;
    }

    #endregion
    
    #region RewarMoveBoost

    private static float rewMoveBoost = 1f;

    public static void SetMoveBoostRewValue(float value)
    {
        rewMoveBoost = value;
    }

    public static float GetMoveBoostRewValue()
    {
        return rewMoveBoost;
    }

    #endregion
    
    #region SoundMusic
    
    public const string SOUND = "sound";
    public static bool isSound = true;
    public static void SetSound(bool _isSound) {
        isSound = _isSound;
        if (_isSound)
        {
            PlayerPrefs.SetInt(SOUND, 1);
        }
        else {
            PlayerPrefs.SetInt(SOUND, 0);
        }
        PlayerPrefs.Save();
    }

    public static void GetSound() {
        int soundInt = PlayerPrefs.GetInt(SOUND, 1);
        if (soundInt == 1)
        {
            isSound = true;
        }
        else {
            isSound = false;
        }
    }


    public const string MUSIC = "music";
    public static bool isMusic = true;
    public static void SetMusic(bool _isMusic)
    {
        isMusic = _isMusic;
        if (_isMusic)
        {
            PlayerPrefs.SetInt(MUSIC, 1);
        }
        else
        {
            PlayerPrefs.SetInt(MUSIC, 0);
        }
        PlayerPrefs.Save();
    }

    public static void GetMusic()
    {
        int musicInt = PlayerPrefs.GetInt(MUSIC, 1);
        if (musicInt == 1)
        {
            isMusic = true;
        }
        else
        {
            isMusic = false;
        }
    }
    
    #endregion
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
