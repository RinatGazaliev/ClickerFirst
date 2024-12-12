using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class TxtAddScorePerClick : MonoBehaviour
{
    // Start is called before the first frame update
    private Text txtAddScore;
    private Graphic  objectRenderer;
    private float animDuration = 1f;
    
    void Start()
    {
        txtAddScore = gameObject.GetComponent<Text>();
        txtAddScore.text = Config.GetScorePerClick().ToString();
        objectRenderer = gameObject.GetComponent<Graphic>();
        AnimateMoveUpDisappear();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void AnimateMoveUpDisappear()
    {
        Vector3 endValue = gameObject.transform.localPosition;
        endValue.y = endValue.y + 100;
        gameObject.transform.DOMove(endValue, animDuration);
        objectRenderer.DOFade(0, animDuration)
            .OnKill(DestroyObject);
        
    }
    
    private void DestroyObject()
    {
        Destroy(gameObject); // Уничтожаем сам объект
    }
}
