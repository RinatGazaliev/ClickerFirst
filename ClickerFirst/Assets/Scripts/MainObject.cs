using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using System;
using UnityEngine;

public class MainObject : MonoBehaviour, IPointerClickHandler
{
    
    // Событие, на которое могут подписаться другие объекты
    public static event Action <GameObject> OnObjectClicked;

    void OnMouseDown()
    {
        // Вызываем событие и передаем объект, на который кликнули
        OnObjectClicked?.Invoke(gameObject);
       // Debug.Log("ConfiScorePerClickKF"+Config.GetPerClickScaleKf());
    }// Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnPointerClick()
    {
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("MainObjClicked");
        OnObjectClicked?.Invoke(gameObject);
        
    }
    

}
