using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreZone : MonoBehaviour
{

    [SerializeField] Text txtTotalScore;
    [SerializeField] private GameObject spawnedAddScorePerClick;
    [SerializeField] private GameObject spawnedAddScorePerSec;
   // [SerializeField] private TxtAddScorePerClick spawnedAddScorePerClick; // Объект, который будет спавниться
    [SerializeField] private Vector3 spawnPositionPerClick = Vector3.zero; // Позиция спавна
    [SerializeField] private Vector3 spawnPositionPerSec = Vector3.zero; 
    [SerializeField] private Vector3 spawnRotation = Vector3.zero; // Ротация спавна (в градусах)

    // Метод для вызова события спавна
  
    

    void OnEnable()
    {
        // Подписываемся на событие
        MainObject.OnObjectClicked += ClickTotalScore;
        Config.OnChangeTotalScore += UpdateTotalScore;
    }

    void OnDisable()
    {
        // Отписываемся от события
        MainObject.OnObjectClicked -= ClickTotalScore;
        Config.OnChangeTotalScore -= UpdateTotalScore;
    }

    private void ClickTotalScore(GameObject clickedObject)
    {
        Debug.Log($"{name} получил уведомление, что кликнули на {clickedObject.name}");
        // Добавьте свою логику
        if (clickedObject.name == "MainClickObject")
        {
            Debug.Log("Это мой целевой объект, запускаем действие!");
            AddTotalScoreOnClick();
            SpawnScorePerCLick();


        }
    }

    private void AddTotalScoreOnClick()
    {
        int currScore = Config.GetTotalScore();
        Config.SetTotalScore(currScore+GetScoreToAddClick());
        //TODO AddAnimate And Spawn Object txtPerClick
    }
    
    public void AddTotalScoreOnSec()
    {
        int currScore = Config.GetTotalScore();
        Config.SetTotalScore(currScore+GetScoreToAddSec());
        //TODO AddAnimate And Spawn Object txtPerClick
    }

    private int GetScoreToAddClick()
    {
        return Config.GetScorePerClick();
    }
    private int GetScoreToAddSec()
    {
        return Config.GetScorePerClick();
    }

    private void UpdateTotalScore(int totalScore)
    {
        txtTotalScore.text = totalScore.ToString();
    }

    // Start is called before the first frame update
    void Start()
    {
        txtTotalScore.text = Config.GetTotalScore().ToString();
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
            Debug.Log("RorldCoord"+worldPosition);
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
}
