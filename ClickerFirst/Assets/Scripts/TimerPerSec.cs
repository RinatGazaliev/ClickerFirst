using System.Collections;
using UnityEngine;

public class TimerPerSec : MonoBehaviour
{
    // Флаг для запуска/остановки таймера
    private bool timerRunning = false;
    [SerializeField] private ScoreZone _scoreZone;

    // Таймерная функция
    void Start()
    {
        _scoreZone.AddTotalScoreOnSec();
        _scoreZone.SpawnScorePerSec();
        StartTimer();
    }

    private void TimerFunction()
    {
        Debug.Log("1secLeft!");
        _scoreZone.AddTotalScoreOnSec();
        _scoreZone.SpawnScorePerSec();
        
    }

    // Запуск таймера
    public void StartTimer()
    {
        if (!timerRunning)
        {
            timerRunning = true;
            StartCoroutine(TimerCoroutine());
        }
    }

    // Остановка таймера
    public void StopTimer()
    {
        if (timerRunning)
        {
            timerRunning = false;
            StopCoroutine(TimerCoroutine());
        }
    }

    // Коррутина для таймера
    private IEnumerator TimerCoroutine()
    {
        while (timerRunning)
        {
            // Ждем 1 секунду
            yield return new WaitForSeconds(1f);

            // Вызываем функцию каждую секунду
            TimerFunction();
        }
    }
}
