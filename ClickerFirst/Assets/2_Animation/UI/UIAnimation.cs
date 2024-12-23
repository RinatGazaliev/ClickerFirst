using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine;

public class UIAnimation : MonoBehaviour
{
    private enum AnimOptions
    {
        btnFromLeft,
        btnFromTop,
        btnFromRight,
        wgt_FromTop
    }
    
    [SerializeField] private AnimOptions viewAnimType;
    private string viewType => viewAnimType.ToString();
    
    public float fadeTime = 0.5f;

    public CanvasGroup canvasGroup;

    public RectTransform rectTransform;
    [SerializeField] private Dropdown dropdown;
    private System.Action selectedFunction;
    
    // Start is called before the first frame update
    void Start()
    {
       // ButtSmoothFromLeft();
        SaveAnimFunct(viewType);
        CallAnimationFunct();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SaveAnimFunct(string viewAnimType)
    {
        switch (viewAnimType)
        {
            case "btnFromLeft":
                selectedFunction = ButtSmoothFromLeft;
                break;
            case "btnFromTop":
                selectedFunction = ButtSmoothFromTop;
                break;
            case "btnFromRight":
                selectedFunction = ButtSmoothFromRight;
                break;
            case "wgt_FromTop":
                selectedFunction = PanelFadeIn;
                break;
            default:
                selectedFunction = null;
                Debug.LogWarning("Неизвестное значение в Dropdown!");
                break;
        }
    }

    public void CallAnimationFunct()
    {
        if (selectedFunction != null)
        {
            selectedFunction.Invoke();
        }
        else
        {
            Debug.LogWarning("Функция не выбрана или не задана!");
        }
    }

    public void PanelFadeIn()
    {
        canvasGroup.alpha = 0;
        Vector3 startPos = rectTransform.transform.localPosition;
        startPos.y += -1000f;
        //rectTransform.transform.localPosition = new Vector3(0f,-1000f,0f);
        rectTransform.DOAnchorPos(startPos, fadeTime, false).SetEase(Ease.OutElastic);
        canvasGroup.DOFade(1, fadeTime);
    }

    public void PanelFadeOut()
    {
        canvasGroup.alpha = 1f;
        rectTransform.transform.localPosition = new Vector3(0f,0f,0f);
        rectTransform.DOAnchorPos(new Vector2(0f, -1000f), fadeTime, false).SetEase(Ease.InOutQuint);
        canvasGroup.DOFade(0, fadeTime);
    }

    public void ButtSmoothFromTop()
    {
        canvasGroup.alpha = 0;
        Vector3 endPos = rectTransform.transform.position;
        Debug.Log("endPos"+endPos);
        Vector3 startPos = endPos;
        startPos.y = startPos.y - 300;
        rectTransform.transform.position = startPos;
        rectTransform.DOMoveY(endPos.y, fadeTime, false).SetEase(Ease.OutElastic);
        canvasGroup.DOFade(1, fadeTime);
        
    }
    public void ButtSmoothFromLeft()
    {
        canvasGroup.alpha = 0;
        Vector3 endPos = rectTransform.transform.position;
        Debug.Log("endPos"+endPos);
        Vector3 startPos = endPos;
        startPos.x = startPos.x + 300;
        rectTransform.transform.position = startPos;
        rectTransform.DOMoveX(endPos.x, fadeTime, false).SetEase(Ease.OutElastic);
        canvasGroup.DOFade(1, fadeTime);
        
    }
    
    public void ButtSmoothFromRight()
    {
        canvasGroup.alpha = 0;
        Vector3 endPos = rectTransform.transform.position;
        Debug.Log("endPos"+endPos);
        Vector3 startPos = endPos;
        startPos.x = startPos.x - 300;
        rectTransform.transform.position = startPos;
        rectTransform.DOMoveX(endPos.x, fadeTime, false).SetEase(Ease.OutElastic);
        canvasGroup.DOFade(1, fadeTime);
        
    }
    
    
}
