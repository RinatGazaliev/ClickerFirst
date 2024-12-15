using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgController : MonoBehaviour
{
    public GameObject gradientPrefab; // ������ ���������
    public RectTransform canvas; // ������
    public float moveSpeed = 50f; // �������� ��������
    public int gradientCount = 10; // ���������� ����������
    private List<GameObject> gradients = new List<GameObject>(); // ������ ����������
    private float gradientHeight; // ������ ���������
    private bool isMoving = false; // ���� ��� �������� ��������

    // Start is called before the first frame update
    void Start()
    {
        // �������� ������ ���������
        gradientHeight = gradientPrefab.GetComponent<RectTransform>().rect.height;

        // ������� ���������
        for (int i = 0; i < gradientCount; i++)
        {
            GameObject gradient = Instantiate(gradientPrefab, canvas); // ������� ������ �� �������
            gradient.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, i * gradientHeight);
            gradients.Add(gradient);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            MoveBackground();
        }
    }

    public void StartMoving()
    {
        isMoving = true;
    }

    public void StopMoving()
    {
        isMoving = false;
    }

    private void MoveBackground()
    {
        // ������� ��������� ����
        foreach (GameObject gradient in gradients)
        {
            RectTransform rect = gradient.GetComponent<RectTransform>();
            rect.anchoredPosition += Vector2.down * moveSpeed * Time.deltaTime;

            // ���� �������� ����� �� ������� ������, ���������� ��� ������
            if (rect.anchoredPosition.y < -gradientHeight)
            {
                rect.anchoredPosition += new Vector2(0, gradientHeight * gradientCount);
                UpdateGradient(gradient); // ��������� ������ ���������
            }
        }
    }

    private void UpdateGradient(GameObject gradient)
    {
        // ����� �� ������ �������� ������ ���������
        var image = gradient.GetComponent<UnityEngine.UI.Image>();
        // image.sprite = ...; // �������� ���� ������ ������ ������ �������
    }
}
