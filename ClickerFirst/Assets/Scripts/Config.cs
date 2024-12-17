using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Config : MonoBehaviour
{
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
    public static readonly int[] DistanceToChangeTextureRoad = { 25, 50, 100, 200, 400 };
    //private bool isHeavenMove = false;
    public static event Action<bool> OnChangeHeavenMove = delegate (bool _isHeaveMove) { };

    public static void SetRoadTextureCurrN(int currTextNumber)
    {
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
    
    private static List<int> boosterPrice = new List<int> {10, 10, 200, 200, 400, 400 };
    public static int GetBoosterPrice(int boosterN) {
        return boosterPrice[boosterN];
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
