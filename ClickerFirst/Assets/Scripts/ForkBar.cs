using System;
using UnityEngine;
using UnityEngine.UI;

public class ForkBar : MonoBehaviour
{
    
   // [SerializeField] private float valueA = 0f; // Начальное значение А
    [SerializeField] private float decrementB = 0.1f; // Значение В, на которое вычитается
    [SerializeField] private float interval = 1f;
    private Slider sliderPork;
    private float addBarValue;// Интервал времени между вычитаниями
    //private int perClickScaleKf;

    private float timer = 0f;
    
    
    //public static event Action  OnPerClickScaleKfChanged;
    // Start is called before the first frame update
    
    void Start()
    {
        sliderPork = GetComponent<Slider>();
        sliderPork.value = 0;
        addBarValue = Config.GetForkAddValue();
    }

    // Update is called once per frame
    void Update()
    {
        // Увеличиваем таймер на время, прошедшее с последнего кадра
        timer += Time.deltaTime;

        // Если прошел интервал, вычитаем значение B из A
        if (timer >= interval)
        {
            if ( sliderPork.value>0)
            {
                sliderPork.value -= decrementB;
            }

            if ( sliderPork.value<0)
            {
                sliderPork.value = 0f;
            }
            timer = 0f; // Сбрасываем таймер   

            Debug.Log($"Текущее значение A: {sliderPork.value}");
        }
    }
    
    void OnEnable()
    {
        // Подписываемся на событие
        MainObject.OnObjectClicked += AddValueToForkBar;
        
    }

    void OnDisable()
    {
        // Отписываемся от события
        MainObject.OnObjectClicked -= AddValueToForkBar;
        
    }

    private void AddValueToForkBar(GameObject clickedObject)
    {
        
        // Добавьте свою логику
        if (clickedObject.name == "MainClickObject")
        {
            float currValueSlider = sliderPork.value;
            currValueSlider = currValueSlider + addBarValue;
            sliderPork.value = currValueSlider;
            if (sliderPork.value>0.7f)
            {
                if (Config.GetPerClickScaleKf() < 2)
                {
                    Config.SetPerClickScaleKf(2);
                }

                if (sliderPork.value>1f)
                {
                    sliderPork.value = 1;  
                }

                
            }
            else
            {
                if (Config.GetPerClickScaleKf()  > 1)
                {
                    Config.SetPerClickScaleKf(1) ;
                }
            }
            Debug.Log("CurrValueSlider"+currValueSlider);
        }
    }
    
}
