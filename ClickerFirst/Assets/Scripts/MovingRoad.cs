using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovingRoad : MonoBehaviour
{
    // Start is called before the first frame update
    private float currSpeedKf = 500f;
    private bool isWalkActive = false;
    [SerializeField] private ParticleSystem vfxFinal;
    
    [SerializeField] ShowWgtManager showWdgManager;
   
    [SerializeField] Text txtTotalDistance;
    [SerializeField] GameObject Part1;
    [SerializeField] GameObject Part2;
    [SerializeField] GameObject Flag;
    private float startPositionPart1;
    private float startPositionPart2;
    //public Transform object1; // Первый объект
    //public Transform object2; 
   
    private float timeSinceLastSave = 0f; // Таймер для сохранения в PlayerPrefs
    private float elapsedTime = 0f; // Общий накопленный таймер
    private float totalDistance;
    [SerializeField] private List<Sprite> SpriteRoadUp;
    [SerializeField] private List<Color> SpriteRoadDown;
    private bool valueClickAdded = false;
    
    private int currRoadTextureN_1;
    private int currRoadTextureN_2;

    private int maxTextN;
    public bool isTutLocked = false;
    public static event Action<bool> OnIsWalkingChange;

    void Start()
    {
        /*startPositionPart1 = Part1.transform.localPosition;
        startPositionPart2 = Part2.transform.localPosition;*/
        startPositionPart1 = Part1.transform.localPosition.x;
        startPositionPart2 = Part2.transform.localPosition.x;
        Debug.Log("startPositionPart1"+startPositionPart1);

        maxTextN = SpriteRoadUp.Count;
        totalDistance = Config.GetTotalDistance();
        txtTotalDistance.text = $"{totalDistance:F2} m";
        currRoadTextureN_1 = Config.GetRoadOneTextureCurrN();
        currRoadTextureN_2 = Config.GetRoadTwoTextureCurrN();
        SetSpriteTextureN();
        GetCurrLocalPosition();

    }

    // Update is called once per frame
    void Update()
    {
        
        if (isWalkActive)
        {
            MoveRoad();
            UpdateDistanceValue();
            if (Flag.activeSelf)
            {
                if (Part2.transform.localPosition.x*2<=startPositionPart2&&!isTutLocked)
                {
                    int tutN = Config.GetTutN();
                    Debug.Log("tutN"+tutN);
                    if (tutN<=3)
                    {
                        tutN = tutN+1;
                        Config.SetTutN(tutN);
                        Debug.Log("tutN"+tutN);
                    }
                    isTutLocked = true;
                    showWdgManager.StartPartRoadWdg();
                    //Flag.SetActive(false);
                }
            }

        }  // Двигаем объект 2 в сторону начальной позиции объекта 1
 
    }

    private void StartTimerWalkCoroutine(GameObject clickedObject)
    {
        StopAllCoroutines();
        //float distanceTotal = Config.GetTotalDistance();
        //totalDistance += Time.deltaTime*Config.GetPerClickScaleKf()*Config.GetMoveBoostRewValue()*Config.GetDistanceBoostKf()+ 0.1f;
        //totalDistance = Config.GetTotalDistance() + 0.1f;
        //Debug.Log("totalDistanceCheck"+Config.GetDistanceBoostKf());
        //Config.SetTotalDistance(distanceTotal);
        valueClickAdded = false;
        StartCoroutine(HandleBoolChange());
    }

    private IEnumerator HandleBoolChange()
    {
        // Устанавливаем переменную в true
        isWalkActive = true;
        OnIsWalkingChange(true);
        
        Debug.Log("myBool is now TRUE");

        // Ждем 1 секунду
        yield return new WaitForSeconds(1f);

        // Устанавливаем переменную в false
        OnIsWalkingChange(false);
        isWalkActive = false;
        Debug.Log("myBool is now FALSE");
    }
    
    void OnEnable()
    {
        // Подписываемся на событие
        MainObject.OnObjectClicked += StartTimerWalkCoroutine;
        PartRoadCompleted.OnPartRoadCompletedClosed += InactivateFlag;
        Config.OnChangeTotalDistance += SaveCurrLocalPosition;
        LeftButtZoneManager.OnTutAnimFinished += OnTutWgtAnimFinished;
        ParallaxBGMove.OnStartFinalTut += StartFinalTut;
        ParallaxBGMove.OnFinishFinalTut += FinishFinalTut;
    }

    void OnDisable()
    {
        // Отписываемся от события
        MainObject.OnObjectClicked -= StartTimerWalkCoroutine;
        PartRoadCompleted.OnPartRoadCompletedClosed -= InactivateFlag;
        Config.OnChangeTotalDistance -= SaveCurrLocalPosition;
        LeftButtZoneManager.OnTutAnimFinished -= OnTutWgtAnimFinished;
        ParallaxBGMove.OnStartFinalTut -= StartFinalTut;
        ParallaxBGMove.OnFinishFinalTut -= FinishFinalTut;
    }

    private void StartFinalTut()
    {
     //   currSpeedKf = currSpeedKf / 3;
    }
    private void FinishFinalTut()
    {
      //  currSpeedKf = currSpeedKf * 3 * 3;
        vfxFinal.gameObject.SetActive(true);
    }

    private void OnTutWgtAnimFinished(string noMatter)
    {
        InactivateFlag();
        //isTutLocked = false;
    }

    private void InactivateFlag()
    {
        Flag.SetActive(false);
        isTutLocked = false;
        Time.timeScale = 1f;
    }

    private void SetSpriteTextureN()
    {
        SetTextureObjectOne();
        SetTextureObjectTwo();
      //  currRoadTextureN
    }
    
    private void SaveCurrLocalPosition()
    {
        Vector3 currPart1LocPosition = Part1.transform.transform.localPosition;
        Vector3 currPart2LocPosition = Part2.transform.transform.localPosition;
        
        Config.SetRoadLastPosition(currPart1LocPosition.x, currPart2LocPosition.x);
        Debug.Log("currPart1LocPosition"+currPart1LocPosition);
    }
    
    private void GetCurrLocalPosition()
    {
        float currPart1LocPosition = Config.GetRoadLastPositionPart1();
        float currPart2LocPosition = Config.GetRoadLastPositionPart2();

        Part1.transform.transform.localPosition = new Vector3 (currPart1LocPosition,-409,0);
        Part2.transform.transform.localPosition = new Vector3 (currPart2LocPosition,-409,0);
    }
    
    private void SetTextureObjectOne()
    {
        if (currRoadTextureN_1>=maxTextN)
        {
            currRoadTextureN_1 = maxTextN-1;
        }
        
        Transform child = Part1.transform.Find("SpriteDown");

        if (child != null)
        {

            child.GetComponent<Image>().color = SpriteRoadDown[currRoadTextureN_1];
        }
        else
        {
            Debug.Log("Объект с именем 'SpriteDown' не найден.");
        }
        Transform child2 = Part1.transform.Find("SpriteUp");

        if (child2 != null)
        {
            child2.GetComponent<Image>().sprite = SpriteRoadUp[currRoadTextureN_1];
        }
        else
        {
            Debug.Log("Объект с именем 'SpriteDown' не найден.");
        }
    }
    private void SetTextureObjectTwo()
    {
        if (currRoadTextureN_2>=maxTextN)
        {
            currRoadTextureN_2 = maxTextN-1;
        }
        
        Transform child3 = Part2.transform.Find("SpriteDown");

        if (child3 != null)
        {
            child3.GetComponent<Image>().color = SpriteRoadDown[currRoadTextureN_2];
        }
        else
        {
            Debug.Log("Объект с именем 'SpriteDown' не найден.");
        }
        
        Transform child4 = Part2.transform.Find("SpriteUp");

        if (child4 != null)
        {
            child4.GetComponent<Image>().sprite = SpriteRoadUp[currRoadTextureN_2];
        }
        else
        {
            Debug.Log("Объект с именем 'SpriteDown' не найден.");
        }
    }

    private void MoveRoad()
    {
        var vector3 = Part2.transform.localPosition;
        Debug.Log("Part2.transform.localPosition"+vector3.x);
        vector3.x = vector3.x - currSpeedKf * Time.deltaTime*Config.GetPerClickScaleKf()*Config.GetMoveBoostRewValue()*Config.GetMoveBoostTut();
        Part2.transform.localPosition = vector3;
        // Debug.Log("Part2.transform.localPosition"+vector3.x);

        // Двигаем объект 1 с такой же разницей
        var position = Part1.transform.localPosition;
        position.x = position.x - currSpeedKf * Time.deltaTime*Config.GetPerClickScaleKf()*Config.GetMoveBoostRewValue()*Config.GetMoveBoostTut();
        Part1.transform.localPosition = position;
        //Debug.Log("Part2.transform.localPosition"+Part2.transform.localPosition);

        // float currDistanceLeft = Vector3.Distance(object2.position, startPositionPart1);
            
      
        // Проверяем, достиг ли объект 2 начальной позиции объекта 1
        if (Part2.transform.localPosition.x<= startPositionPart1)
        {
               
            // Перемещаем объекты на начальные позиции
            var object1Position = Part1.transform.localPosition;
            object1Position.x = startPositionPart1;
            Part1.transform.localPosition = object1Position;
                
            var object2Position = Part2.transform.localPosition;
            object2Position.x = startPositionPart2;
            Part2.transform.localPosition = object2Position;
            if (Config.GetFlagCanShow()==1)
            {
 
                currRoadTextureN_2 = currRoadTextureN_2 + 1;
                //wdgtPartRoad.gameObject.SetActive(true);
                //showWdgManager.StartPartRoadWdg();
                Config.SetRoadTwoTextureCurrN(currRoadTextureN_2);
                //Debug.Log("currRoadTextureN"+currRoadTextureN_2);
                SetTextureObjectTwo();
                Config.SetSaveBlock(1);
                Config.SetHeavenMove(true);
                Flag.SetActive(true);
                Config.SetFlagCanShow(0);
            }
            else
            {
                Debug.Log("currRoadTextureN"+currRoadTextureN_1);
                Debug.Log("currRoadTextureN"+currRoadTextureN_2);
                if (currRoadTextureN_1!=currRoadTextureN_2)
                {
                    currRoadTextureN_1 = currRoadTextureN_2;
                    Config.SetRoadOneTextureCurrN(currRoadTextureN_1);
                    Config.SetSaveBlock(0);
                }
                SetSpriteTextureN();
            }
        }
    }

    private void UpdateDistanceValue()
    {
        // Рассчитываем значение переменной `value` каждый кадр
       // elapsedTime += Time.deltaTime;
       float deltaDistance = Time.deltaTime * Config.GetPerClickScaleKf() * Config.GetMoveBoostRewValue() * Config.GetDistanceBoostKf();
       totalDistance += deltaDistance;
        // Обновляем значение плавно
        
        if (!valueClickAdded) // Проверяем, добавляли ли значение ранее
        {
            totalDistance += 0.1f; // Добавляем значение только один раз
            valueClickAdded = true; // Устанавливаем флаг в true, чтобы больше не добавлять
        }

        // Сохраняем значение в PlayerPrefs каждую секунду
        timeSinceLastSave += Time.deltaTime;
        Debug.Log("timeSinceLastSave"+timeSinceLastSave);
        if (timeSinceLastSave >= 1f)
        {
            Debug.Log("total DistanceSaved");
            timeSinceLastSave = 0f; // Сбрасываем счетчик
            Config.SetTotalDistance (totalDistance); // Сохраняем текущее значение
        }
        txtTotalDistance.text = $"{totalDistance:F2} m";
    }
    private void MoveHeaven ()
    {
        
    }

}
