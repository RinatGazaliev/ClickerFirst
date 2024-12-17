using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewAutoClicker : MonoBehaviour
{
    private bool isAutoClickRunning = false;
    private float autoClickDuration = 15f; 
    private float autoClickInterval = 0.5f; // Интервал между событиями OnAutoClick
    private float autoClickTimer = 0f;
    private Button btnSelf;
    [SerializeField] private Slider timerSlider;
    [SerializeField] private MainObject sausageObject;// Таймер для события OnAutoClick
    
    public static event Action OnAutoClickerClick;
    // Start is called before the first frame update
    void Start()
    {
        if (timerSlider != null)
        {
            timerSlider.minValue = 0f;
            timerSlider.maxValue = 1f;
            timerSlider.value = 1f;
        }
        btnSelf=GetComponent<Button>();
        btnSelf.onClick.AddListener(OnPointerClick);
        InitViews();
    }

    // Update is called once per frame
    void Update()
    {
        if (isAutoClickRunning)
        {
            autoClickTimer += Time.deltaTime;
            if (autoClickTimer >= autoClickInterval)
            {
                autoClickTimer = 0f;
                sausageObject.OnMouseDown();
            }
        }

    }
    
    private IEnumerator StartAutoClickTimer()
    {
        isAutoClickRunning = true; // Включаем авто-клик
        Debug.Log("Auto-click started");
        float elapsedTime = 0f;

        while (elapsedTime < autoClickDuration)
        {
            elapsedTime += Time.deltaTime;

            // Обновляем значение слайдера
            if (timerSlider != null)
            {
                timerSlider.value = 1f - (elapsedTime / autoClickDuration);
            }

            yield return null;
        }

        if (timerSlider != null)
        {
            timerSlider.value = 0f;
        }
        isAutoClickRunning = false; // Выключаем авто-клик
        Debug.Log("Auto-click ended");
    }
    
    private void OnPointerClick ()
    {
        timerSlider.gameObject.SetActive(true);
        btnSelf.interactable = false;
        
        if (!isAutoClickRunning) // Если таймер ещё не запущен
        {
            StartCoroutine(StartAutoClickTimer());
        }
    }

    public void InitViews()
    {
        timerSlider.gameObject.SetActive(false);
        btnSelf.interactable = true;
    }
}
