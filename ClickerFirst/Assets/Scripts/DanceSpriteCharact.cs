using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DanceSpriteCharact : MonoBehaviour
{
    public Image imageComponent; // ��������� Image
    public Sprite sprite1; // ������ ������
    public Sprite sprite2; // ������ ������
    public float switchInterval = 0.5f; // �������� ������������ � ��������

    private bool isSprite1Active = true;
    private Coroutine animationCoroutine;

    public void StartAnimation()
    {
        if (animationCoroutine != null)
        {
            StopCoroutine(animationCoroutine); // ������������� ���������� ��������
        }
        animationCoroutine = StartCoroutine(SwitchSprites());
    }

    public void StopAnimation()
    {
        if (animationCoroutine != null)
        {
            StopCoroutine(animationCoroutine);
            animationCoroutine = null;
        }
    }

    IEnumerator SwitchSprites()
    {
        while (true)
        {
            imageComponent.sprite = isSprite1Active ? sprite2 : sprite1;
            isSprite1Active = !isSprite1Active;

            yield return new WaitForSeconds(switchInterval);
        }
    }
}
