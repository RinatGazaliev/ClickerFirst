using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ParallaxBGMove : MonoBehaviour
{
    [SerializeField] private int slowerSpeedKf = 4;
    private bool isParallaxMove = false;
    private float currSpeedKf = 500f;
    
    //[SerializeField] Text txtTotalDistance;
    [SerializeField] GameObject Part1;
    [SerializeField] GameObject Part2;
    [SerializeField] int LayerN;
    //[SerializeField] GameObject Flag;
   //public Transform object1; // Первый объект
    //public Transform object2; 
    private GameObject part1ChildToMove;
    private GameObject part2ChildToMove;
   
    private float timeSinceLastSave = 0f; // Таймер для сохранения в PlayerPrefs
    private float elapsedTime = 0f; // Общий накопленный таймер
    private float totalDistance;
    [SerializeField] private List<GameObject> ScenesGameObjects;
    //[SerializeField] private List<Color> SpriteRoadDown;
    private bool valueClickAdded = false;
    
    private int currRoadTextureN;
    public static event Action<bool> OnIsWalkingChange;

    void Start()
    {
        /*startPositionPart1 = Part1.transform.localPosition;
        startPositionPart2 = Part2.transform.localPosition;*/

       // Debug.Log("startPositionPart1"+startPositionPart1);
        
       
        totalDistance = Config.GetTotalDistance();
        //txtTotalDistance.text = $"{totalDistance:F2} m";
        currRoadTextureN = Config.GetParallaxTextureCurrN(LayerN);
        SetSpriteTextureN();

    }

    // Update is called once per frame
    void Update()
    {
        
        if (isParallaxMove)
        {
            MoveRoad();
           // UpdateDistanceValue();
           
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
        isParallaxMove = true;
       
        Debug.Log("myBool is now TRUE");

        // Ждем 1 секунду
        yield return new WaitForSeconds(1f);

        // Устанавливаем переменную в false
        isParallaxMove = false;
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
        MoveObjectToPart1();
        
      //  currRoadTextureN
    }
    
    private void SetTextureObjectOne()
    {
        //Part1.
        //Sce[currRoadTextureN].SetActive(true);
        /*Transform child = Part1.transform.Find("SpriteDown");

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
        }*/
    }
    private void SetTextureObjectTwo()
    {
        ScenesGameObjects[currRoadTextureN+1].SetActive(true);
        /*Transform child3 = Part2.transform.Find("SpriteDown");

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
        }*/
    }

    private void MoveRoad()
    {
        var vector3 = part2ChildToMove.transform.localPosition;
        Debug.Log("Part2.transform.localPosition"+vector3.x);
        vector3.x = vector3.x - (currSpeedKf*Time.deltaTime*Config.GetPerClickScaleKf()*Config.GetMoveBoostRewValue())/slowerSpeedKf;
        part2ChildToMove.transform.localPosition = vector3;
        // Debug.Log("Part2.transform.localPosition"+vector3.x);

        // Двигаем объект 1 с такой же разницей
        var position = part1ChildToMove.transform.localPosition;
        position.x = position.x - (currSpeedKf*Time.deltaTime*Config.GetPerClickScaleKf()*Config.GetMoveBoostRewValue())/slowerSpeedKf;
        part1ChildToMove.transform.localPosition = position;

        if (part1ChildToMove.transform.localPosition.x <= 0)
        {
            currRoadTextureN = currRoadTextureN + 1;
            Config.SetParallaxTextureCurrN(currRoadTextureN, LayerN);

            SetSpriteTextureN();
            //Time.timeScale = 0f;
        }
    }
    private void MoveObjectToPart2()
    {
        if (Part2 == null)
        {
            Debug.LogWarning("Контейнер Part1 не найден.");
            return;
        }

        // Удаление всех дочерних объектов
        foreach (Transform child in Part2.transform)
        {
            Destroy(child.gameObject);  // Удаление дочернего объекта
        }

        if (currRoadTextureN+1 >= 0 && currRoadTextureN+1 < ScenesGameObjects.Count)
        {
            GameObject objToCopy = ScenesGameObjects[currRoadTextureN + 1];

            if (objToCopy != null)
            {
                Vector3 localPosition = new Vector3(3572, 0, 0);
           
                part2ChildToMove = Instantiate(objToCopy);
                part2ChildToMove.transform.SetParent(Part2.transform);  // Установка нового родителя
                part2ChildToMove.transform.localPosition = localPosition;  // Сброс позиции
                //part2ChildToMove.transform.localRotation = Quaternion.identity;  // Сброс поворота
                part2ChildToMove.transform.localScale = Vector3.one;  // Сброс масштаба
                //startPositionPart2 = part2ChildToMove.transform.localPosition.x;
            }
            else
            {
                Debug.LogWarning("Объект для перемещения не найден.");
            }
        }
        else
        {
            Debug.LogWarning("Некорректный индекс currRoadTextureN.");
        }
    }
    
    private void MoveObjectToPart1()
    {
        if (Part1 == null)
        {
            Debug.LogWarning("Контейнер Part1 не найден.");
            return;
        }

        // Удаление всех дочерних объектов
        foreach (Transform child in Part1.transform)
        {
            Destroy(child.gameObject);  // Удаление дочернего объекта
        }

        if (Part2.transform.childCount >0)
        {
            while (Part2.transform.childCount > 0)
            {
                part1ChildToMove = Part2.transform.GetChild(0).GameObject();
                part1ChildToMove.transform.SetParent(Part1.transform); // Устанавливаем нового родителя
                Vector3 localPosition = new Vector3(3572, 0, 0);
                part1ChildToMove.transform.localPosition = localPosition;  // Сброс позиции
                //part1ChildToMove.transform.localRotation = Quaternion.identity;  // Сброс поворота
                part1ChildToMove.transform.localScale = Vector3.one;  
            }
           
            
        }
        else if (currRoadTextureN >= 0 && currRoadTextureN < ScenesGameObjects.Count)
        {
            GameObject objToCopy = ScenesGameObjects[currRoadTextureN];

            if (objToCopy != null)
            {
                Vector3 localPosition = new Vector3(3572, 0, 0);
           
                part1ChildToMove = Instantiate(objToCopy);
                part1ChildToMove.transform.SetParent(Part1.transform);  // Установка нового родителя
                part1ChildToMove.transform.localPosition = localPosition;  // Сброс позиции
                //part1ChildToMove.transform.localRotation = Quaternion.identity;  // Сброс поворота
                part1ChildToMove.transform.localScale = Vector3.one;  // Сброс масштаба
               // startPositionPart1 = part1ChildToMove.transform.localPosition.x;
              
            }
            else
            {
                Debug.LogWarning("Объект для перемещения не найден.");
            }
        }
        else
        {
            Debug.LogWarning("Некорректный индекс currRoadTextureN.");
        }
        MoveObjectToPart2();
    }
}
