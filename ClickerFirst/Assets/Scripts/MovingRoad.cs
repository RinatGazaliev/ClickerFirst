using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovingRoad : MonoBehaviour
{
    // Start is called before the first frame update
    private float currSpeedKf = 500f;
    private bool isWalkActive = false;
    
    [SerializeField] ShowWgtManager showWdgManager;
   
    [SerializeField] Text txtTotalDistance;
    [SerializeField] GameObject Part1;
    [SerializeField] GameObject Part2;
    private float startPositionPart1;
    private float startPositionPart2;
    //public Transform object1; // Первый объект
    //public Transform object2; 
   
    private float timeSinceLastSave = 0f; // Таймер для сохранения в PlayerPrefs
    private float elapsedTime = 0f; // Общий накопленный таймер
    private float totalDistance;
    [SerializeField] private List<Sprite> SpriteRoadUp;
    [SerializeField] private List<Color> SpriteRoadDown;
    
    private int currRoadTextureN;

    void Start()
    {
        /*startPositionPart1 = Part1.transform.localPosition;
        startPositionPart2 = Part2.transform.localPosition;*/
        startPositionPart1 = Part1.transform.localPosition.x;
        startPositionPart2 = Part2.transform.localPosition.x;
        Debug.Log("startPositionPart1"+startPositionPart1);
        
       
        totalDistance = Config.GetTotalDistance();
        txtTotalDistance.text = $"{totalDistance:F2} m";
        currRoadTextureN = Config.GetRoadTextureCurrN();
        SetSpriteTextureN();

    }

    // Update is called once per frame
    void Update()
    {
        if (isWalkActive)
        {
            MoveRoad();
            UpdateDistanceValue();

        }  // Двигаем объект 2 в сторону начальной позиции объекта 1
 
    }

    private void StartTimerWalkCoroutine(GameObject clickedObject)
    {
        StopAllCoroutines();
        StartCoroutine(HandleBoolChange());
    }

    private IEnumerator HandleBoolChange()
    {
        // Устанавливаем переменную в true
        isWalkActive = true;
        Debug.Log("myBool is now TRUE");

        // Ждем 1 секунду
        yield return new WaitForSeconds(1f);

        // Устанавливаем переменную в false
        isWalkActive = false;
        Debug.Log("myBool is now FALSE");
    }
    
    void OnEnable()
    {
        // Подписываемся на событие
        MainObject.OnObjectClicked += StartTimerWalkCoroutine;
        
    }

    void OnDisable()
    {
        // Отписываемся от события
        MainObject.OnObjectClicked -= StartTimerWalkCoroutine;
        
    }

    private void SetSpriteTextureN()
    {
        SetTextureObjectOne();
        SetTextureObjectTwo();
      //  currRoadTextureN
    }
    
    private void SetTextureObjectOne()
    {
        Transform child = Part1.transform.Find("SpriteDown");

        if (child != null)
        {
            child.GetComponent<Image>().color = SpriteRoadDown[currRoadTextureN];
        }
        else
        {
            Debug.Log("Объект с именем 'SpriteDown' не найден.");
        }
        Transform child2 = Part1.transform.Find("SpriteUp");

        if (child2 != null)
        {
            child2.GetComponent<Image>().sprite = SpriteRoadUp[currRoadTextureN];
        }
        else
        {
            Debug.Log("Объект с именем 'SpriteDown' не найден.");
        }
    }
    private void SetTextureObjectTwo()
    {
        Transform child3 = Part2.transform.Find("SpriteDown");

        if (child3 != null)
        {
            child3.GetComponent<Image>().color = SpriteRoadDown[currRoadTextureN];
        }
        else
        {
            Debug.Log("Объект с именем 'SpriteDown' не найден.");
        }
        
        Transform child4 = Part2.transform.Find("SpriteUp");

        if (child4 != null)
        {
            child4.GetComponent<Image>().sprite = SpriteRoadUp[currRoadTextureN];
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
        vector3.x = vector3.x - currSpeedKf * Time.deltaTime*Config.GetPerClickScaleKf()*Config.GetMoveBoostRewValue();
        Part2.transform.localPosition = vector3;
        // Debug.Log("Part2.transform.localPosition"+vector3.x);

        // Двигаем объект 1 с такой же разницей
        var position = Part1.transform.localPosition;
        position.x = position.x - currSpeedKf * Time.deltaTime*Config.GetPerClickScaleKf()*Config.GetMoveBoostRewValue();
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
            if (totalDistance>Config.DistanceToChangeTextureRoad[currRoadTextureN])
            {
                Debug.Log("shouldChangeText");
                currRoadTextureN = currRoadTextureN + 1;
                Config.SetRoadTextureCurrN(currRoadTextureN);
                //wdgtPartRoad.gameObject.SetActive(true);
                showWdgManager.StartPartRoadWdg();
                SetTextureObjectTwo();
                Config.SetHeavenMove(true);
            }
            else
            {
                SetSpriteTextureN();
            }
        }
    }

    private void UpdateDistanceValue()
    {
        // Рассчитываем значение переменной `value` каждый кадр
        elapsedTime += Time.deltaTime;
        totalDistance += Time.deltaTime*Config.GetPerClickScaleKf()*Config.GetMoveBoostRewValue(); // Обновляем значение плавно

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
