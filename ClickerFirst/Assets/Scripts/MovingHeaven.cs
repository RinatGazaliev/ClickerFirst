using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MovingHeaven : MonoBehaviour
{
    private float currSpeedKf = 300f;
    
    [SerializeField] private List<Material> MaterialsHeaven;
    private bool isHeavenMove = false;
    private float startPositionPart1;
    private float startPositionPart2;
    [SerializeField] GameObject Part1;
    [SerializeField] GameObject Part2;
    private int currRoadTextureN;
    // Start is called before the first frame update
    void Start()
    {
        startPositionPart1 = Part1.transform.localPosition.y;
        startPositionPart2 = Part2.transform.localPosition.y; 
        currRoadTextureN = Config.GetRoadOneTextureCurrN();
        SetTextures();
    }

    // Update is called once per frame
    void Update()
    {
        if (isHeavenMove)
        {
            MoveHeaven();
        }
        
    }

    private void OnEnable()
    {
        Config.OnChangeHeavenMove+=UpdateHeavenMove;
    }

    private void OnDisable()
    {
        Config.OnChangeHeavenMove-=UpdateHeavenMove;
    }

    private void MoveHeaven()
    {
        var vector3 = Part2.transform.localPosition;
        //Debug.Log("Part2.transform.localPosition"+vector3.y);
        vector3.y = vector3.y - currSpeedKf * Time.deltaTime*Config.GetPerClickScaleKf();
        Part2.transform.localPosition = vector3;
        // Debug.Log("Part2.transform.localPosition"+vector3.x);

        // Двигаем объект 1 с такой же разницей
        var position = Part1.transform.localPosition;
        position.y = position.y - currSpeedKf * Time.deltaTime*Config.GetPerClickScaleKf();
        Part1.transform.localPosition = position;
        if (Part2.transform.localPosition.y<= startPositionPart1)
        {
               
            // Перемещаем объекты на начальные позиции
            var object1Position = Part1.transform.localPosition;
            object1Position.y = startPositionPart1;
            Part1.transform.localPosition = object1Position;
                
            var object2Position = Part2.transform.localPosition;
            object2Position.y = startPositionPart2;
            Part2.transform.localPosition = object2Position;
            currRoadTextureN = currRoadTextureN + 1;
            SetTextures();
            Config.SetHeavenMove(false);

        }
    }

    private void SetTextures()
    {
       Part1.GetComponent<Image>().material = MaterialsHeaven[currRoadTextureN];
       Part2.GetComponent<Image>().material = MaterialsHeaven[currRoadTextureN+1];
    }

    private void UpdateHeavenMove(bool _isHeavenMove)
    {
        isHeavenMove = _isHeavenMove;
    }
}
