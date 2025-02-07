using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LoadSceneManager : MonoBehaviour
{
    
    public static LoadSceneManager instance;
 
    public Slider progressBar;// Прогресс бар
    public GameObject BGImage;
    private float minimumLoadTime = 2f;
    // Start is called before the first frame update
    void Start()
    {
       
        /*if (Config.GetTutN()<2)
        {
            Config.SetTutN(2);
        }*/
      //Config.SetTutN(2);
        //Config.SetTotalScore(10000000);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        CrazyInit.OnCrazyInitialized += LoadGame;
    }
    private void OnDisable()
    {
        CrazyInit.OnCrazyInitialized -= LoadGame;
    }

    private void LoadGame()
    {
        StartCoroutine(LoadMenuScene_IEnumerator());
    }

    public IEnumerator LoadMenuScene_IEnumerator()
    {
        float startTime = Time.time;

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("GameScene");
        asyncLoad.allowSceneActivation = false;
        while (!asyncLoad.isDone)
        {
            // Рассчитываем прогресс загрузки
            float loadingProgress = Mathf.Clamp01(asyncLoad.progress / 0.9f);

            // Рассчитываем фактический прогресс с учетом минимального времени
            float elapsedTime = Time.time - startTime;
            float displayProgress = Mathf.Clamp01(elapsedTime / minimumLoadTime);

            // Прогресс бар плавно увеличивается от текущего прогресса до 100%
            progressBar.value = Mathf.Lerp(progressBar.value, Mathf.Max(loadingProgress, displayProgress), Time.deltaTime / 0.5f);

            // Если загрузка достигла 90%
            if (asyncLoad.progress >= 0.9f)
            {
                // Если прошло минимальное время, активируем сцену
                if (elapsedTime >= minimumLoadTime)
                {
                    asyncLoad.allowSceneActivation = true;
                }
            }

            yield return null;
        }
    }
}
