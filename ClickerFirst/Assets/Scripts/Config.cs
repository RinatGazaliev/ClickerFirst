using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Config : MonoBehaviour
{
    // Start is called before the first frame update
    
    #region TotalScore
    public const string TOTALSCORE = "TotalScore";
    public static event Action<int> OnChangeTotalScore = delegate (int _scoreValue) { };
    
    public static void SetTotalScore(int scoreValue) {
        PlayerPrefs.SetInt(TOTALSCORE, scoreValue);
        PlayerPrefs.Save();
        OnChangeTotalScore(scoreValue);
    }
    
    public static int GetTotalScore() {
        return PlayerPrefs.GetInt(TOTALSCORE, 0);
    }
    
    #endregion
    
    #region ScorePerClick
    public const string SCOREPERCLICK = "Score_click";
    //public static event Action<int> OnChangeTotalScore = delegate (int _scoreValue) { };
    
    public static void SetScorePerClick(int scoreValue) {
        PlayerPrefs.SetInt(SCOREPERCLICK, scoreValue);
        PlayerPrefs.Save();
        //OnChangeTotalScore(scoreValue);
    }
    
    public static int GetScorePerClick() {
        Debug.Log("ScorePerClick"+PlayerPrefs.GetInt(SCOREPERCLICK, 1));
        int currScorePerClick = PlayerPrefs.GetInt(SCOREPERCLICK, 1) * PerClickScaleKf;
        return currScorePerClick;
        
    }
    
    #endregion
    
    #region ScorePerSec
    public const string SCOREPERSEC = "Score_sec";
    //public static event Action<int> OnChangeTotalScore = delegate (int _scoreValue) { };
    
    public static void SetScorePerSec(int scoreValue) {
        PlayerPrefs.SetInt(SCOREPERSEC, scoreValue);
        PlayerPrefs.Save();
        //OnChangeTotalScore(scoreValue);
    }
    
    public static int GetScorePerSec() {
        Debug.Log("ScorePerClick"+PlayerPrefs.GetInt(SCOREPERSEC, 1));
        return PlayerPrefs.GetInt(SCOREPERSEC, 1);
        
    }
    
    #endregion
    
    #region ForkParams
    
    public const string FORKADD = "ForkAddValue";
    
    public static void SetForkAddValue(float scoreValue) {
        PlayerPrefs.SetFloat(FORKADD, scoreValue);
        PlayerPrefs.Save();
        //OnChangeTotalScore(scoreValue);
    }
    
    public static float GetForkAddValue() {
        return PlayerPrefs.GetFloat(FORKADD, 0.05f);
    }


    private static int PerClickScaleKf = 1;
    public static event Action<int> OnChangePerClickScaleKf = delegate (int _scaleValue) { };
    public static int GetPerClickScaleKf() {
        return PerClickScaleKf;
    }
    
    public static void SetPerClickScaleKf(int scaleValue)
    {
        PerClickScaleKf = scaleValue;
        OnChangePerClickScaleKf(scaleValue);
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
