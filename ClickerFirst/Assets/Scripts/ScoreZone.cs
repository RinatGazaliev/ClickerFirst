using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreZone : MonoBehaviour
{
    [SerializeField] Text txtTotalScore;
    
    [Header ("ScorePerSec")]
    [SerializeField] private GameObject spawnedAddScorePerSec;
    [SerializeField] private Vector3 spawnPositionPerSec = Vector3.zero;
    [SerializeField] Text txtScorePerSecTitle;
    [SerializeField] Text txtScorePerSecValue;
    
   
    [Header ("ScorePerClick")]
    [SerializeField] private GameObject spawnedAddScorePerClick;
    
   // [SerializeField] private TxtAddScorePerClick spawnedAddScorePerClick; // Объект, который будет спавниться
    [SerializeField] private Vector3 spawnPositionPerClick = Vector3.zero; // Позиция спавна
    
    [SerializeField] private Vector3 spawnRotation = Vector3.zero; // Ротация спавна (в градусах)
    [SerializeField] Text txtScorePerClickTitle;
    [SerializeField] Text txtScorePerClickValue;
  
    
    [Header ("StepPerClick")]
    [SerializeField] Text txtStepTitle;
    [SerializeField] Text txtStepSize;
    
    private int scalePerClickKf;
    
    private float startStepSize = 0.5f;
    // Метод для вызова события спавна
  




    void OnEnable()
    {
        // Подписываемся на событие
        MainObject.OnObjectClicked += ClickTotalScore;
        RewAutoClicker.OnAutoClickerClick += AutoClickTotalScore;
        Config.OnChangeTotalScore += UpdateTotalScore;
        Config.OnChangePerClickScaleKf += UpdatePerClickForkKf;
        ForkBar.OnForkBarIsRunning += OnChangeIsWalkingBool;
        Boosters.OnBoosterClick += UpdateTextParamsValues;
        RewMoveBoost.OnRewardStarted += OnRewardKickBoost;
        RewMoveBoost.OnRewardMoveBoostTimeFinish += OnRewardKickBoost;
    }

    void OnDisable()
    {
        // Отписываемся от события
        MainObject.OnObjectClicked -= ClickTotalScore;
        RewAutoClicker.OnAutoClickerClick -= AutoClickTotalScore;
        Config.OnChangeTotalScore -= UpdateTotalScore;
        Config.OnChangePerClickScaleKf -= UpdatePerClickForkKf;
        ForkBar.OnForkBarIsRunning -= OnChangeIsWalkingBool;
        Boosters.OnBoosterClick -= UpdateTextParamsValues;
        RewMoveBoost.OnRewardStarted -= OnRewardKickBoost;
        RewMoveBoost.OnRewardMoveBoostTimeFinish -= OnRewardKickBoost;
    }
    
    

    private void ClickTotalScore(GameObject clickedObject)
    {
        Debug.Log($"{name} получил уведомление, что кликнули на {clickedObject.name}");
        // Добавьте свою логику
        if (clickedObject.name == "Character")
        {
            Debug.Log("Это мой целевой объект, запускаем действие!");
            AddTotalScoreOnClick();
            SpawnScorePerCLick();


        }
    }
    
    private void AutoClickTotalScore()
    {
        AddTotalScoreOnClick();
        SpawnScorePerCLick();
    }

    private void AddTotalScoreOnClick()
    {
        int currScore = Config.GetTotalScore();
        Debug.Log("PointsToAdd");
        Config.SetTotalScore(currScore+GetScoreToAddClick()*Config.GetDoublePointsRewValue());
        //TODO AddAnimate And Spawn Object txtPerClick
    }
    
    public void AddTotalScoreOnSec()
    {
        int currScore = Config.GetTotalScore();
        Config.SetTotalScore(currScore+GetScoreToAddSec()*Config.GetDoublePointsRewValue());
        //TODO AddAnimate And Spawn Object txtPerClick
    }

    private int GetScoreToAddClick()
    {
        return Config.GetScorePerClick();
    }
    private int GetScoreToAddSec()
    {
        return Config.GetScorePerSec();
    }

    private void UpdateTotalScore(int totalScore)
    {
        txtTotalScore.text = totalScore.ToString();
    }
    
    private void UpdatePerClickForkKf(int scaleKf)
    {
        scalePerClickKf=scaleKf;
        Debug.Log("CurscalePerClickKf"+scalePerClickKf);
    }

    // Start is called before the first frame update
    void Start()
    {
        txtTotalScore.text = Config.GetTotalScore().ToString();
        scalePerClickKf = Config.GetPerClickScaleKf();
        UpdateTextParamsValues();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void SpawnScorePerCLick()
    {
        /*if (spawnedAddScorePerClick != null)
        {*/
            // Конвертируем в Quaternion ротацию
            Quaternion rotation = Quaternion.Euler(Vector3.zero);
            Vector3 worldPosition = transform.TransformPoint(spawnPositionPerClick);
            Debug.Log("WorldCoord"+worldPosition);
            //transform
            // Спавним объект
            GameObject SpawnedObj = Instantiate(spawnedAddScorePerClick, worldPosition, rotation);
            //SpawnedObj.transform.SetParent(transform);
            //SpawnedObj.transform.localPosition = spawnPosition;
           // SpawnedObj.transform.rotation = rotation;
            Debug.Log($"Объект {spawnedAddScorePerClick.name} заспавнен в позиции {spawnPositionPerClick} с ротацией {spawnRotation}");
       // }
       // else
       // {
          //  Debug.LogError("Объект для спавна не назначен!");
      //  }
    }
    
    public void SpawnScorePerSec()
    {

        Quaternion rotation = Quaternion.Euler(Vector3.zero);
        Vector3 worldPosition = transform.TransformPoint(spawnPositionPerSec);
        Debug.Log("RorldCoord"+worldPosition);

        GameObject SpawnedObj = Instantiate(spawnedAddScorePerSec, worldPosition, rotation);

        Debug.Log($"Объект {spawnedAddScorePerSec.name} заспавнен в позиции {spawnPositionPerSec} с ротацией {spawnRotation}");

    }
    
    
    private void UpdateTextParamsValues()
    {
        float StepSize = startStepSize + (Config.GetDistanceBoostKf()-1)/2;
        txtStepSize.text = $"{StepSize:F2}";
        txtScorePerClickValue.text = Config.GetScorePerClick().ToString();
        txtScorePerSecValue.text = Config.GetScorePerSec().ToString();
    }
    
    private void OnChangeIsWalkingBool(bool _isRunning)
    {
        if (_isRunning)
        {
            float StepSize = startStepSize*Config.GetMoveBoostRewValue()*Config.GetPerClickScaleKf() + (Config.GetDistanceBoostKf()-1)/2;
            txtStepSize.text = $"{StepSize:F2}";
        }
        else
        {
            float StepSize = startStepSize*Config.GetMoveBoostRewValue() + (Config.GetDistanceBoostKf()-1)/2;
            txtStepSize.text = $"{StepSize:F2}";
        }
      
    }
    private void OnRewardKickBoost()
    {
        float StepSize = startStepSize*Config.GetMoveBoostRewValue()*Config.GetPerClickScaleKf() + (Config.GetDistanceBoostKf()-1)/2;
        txtStepSize.text = $"{StepSize:F2}";
    }
    
}
