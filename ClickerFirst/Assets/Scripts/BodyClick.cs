using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.U2D.Animation;

public class BodyClick : MonoBehaviour
{
    public static BodyClick instance;
    [SerializeField] private MainObject mainCharacter;
    // Start is called before the first frame update
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D polygonCollider;
    private SpriteSkin spriteSkin;

    

    void Start()
    {
        instance = this;
        polygonCollider = GetComponent<BoxCollider2D>();
        //spriteRenderer = GetComponent<SpriteRenderer>();
       // spriteSkin = GetComponent<SpriteSkin>();
    }

    void Update()
    {
        // Проверяем, если спрайт и SpriteSkin существуют
 
    }


    // Update is called once per frame
    public void  OnMouseDown()
    {
        
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Debug.Log("Клик на спрайте!");
        // Вызываем событие и передаем объект, на который кликнули
        mainCharacter.CallMainObjClicked(mousePosition);
           
    

        // Debug.Log("ConfiScorePerClickKF"+Config.GetPerClickScaleKf());
    }// Start is called before the first frame update
    
    public void OnPointerClick(PointerEventData eventData)
    {
        //Debug.Log("MainObjClicked");
        //OnObjectClicked?.Invoke(gameObject);
        
    }
    
    private bool IsClickOnSprite( Vector2 worldPoint)
    {
       // Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 localPoint = transform.InverseTransformPoint(worldPoint);

        // Преобразуем в координаты текстуры
        Sprite sprite = spriteRenderer.sprite;
        Rect textureRect = sprite.textureRect;
        Vector2 pivot = sprite.pivot;
        Vector2 textureCoord = new Vector2(
            localPoint.x * sprite.pixelsPerUnit + pivot.x,
            localPoint.y * sprite.pixelsPerUnit + pivot.y);

        textureCoord.x = Mathf.Clamp(textureCoord.x, 0, textureRect.width - 1);
        textureCoord.y = Mathf.Clamp(textureCoord.y, 0, textureRect.height - 1);

        Color pixelColor = sprite.texture.GetPixel((int)textureCoord.x, (int)textureCoord.y);

        // Проверяем альфа-пиксель
        return pixelColor.a > 0.1f;  // Порог для видимых пикселей
    }

    private void OnEnable()
    {
        PartRoadCompleted.OnPartRoadCompletedClosed += ActivateMainObjClick;
        ShowWgtManager.OnDisableCharClick += InactivateMainObjClick;
        GetNewItemPopUp.OnCloseNewItemPopUp += ActivateMainObjClick;
    }
    private void OnDisable()
    {
        PartRoadCompleted.OnPartRoadCompletedClosed -= ActivateMainObjClick;
        ShowWgtManager.OnDisableCharClick -= InactivateMainObjClick;
        GetNewItemPopUp.OnCloseNewItemPopUp -= ActivateMainObjClick;
    }

    public void ActivateMainObjClick()
    {
        polygonCollider.enabled=true;
    }
    public void InactivateMainObjClick()
    {
        polygonCollider.enabled=false;
    }
    
    public void PlayKickAppearSound()
    {
        SoundManager.instance.PlayRandomOuchSound();
    }
    public void PlayRunSounds()
    {
        SoundManager.instance.PlaySound_stepsLoop();
    }
    public void StopLoopSounds()
    {
        SoundManager.instance.StopLoopSound();
    }
}
