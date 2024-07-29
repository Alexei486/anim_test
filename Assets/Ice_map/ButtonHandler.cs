using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;

public class CustomButtonHandler : MonoBehaviour, IPointerClickHandler
{
    private int clickCount = 0;
    public GameObject otherObject; // ������ ������, ������� ����� �����
    public float resetTime = 5f; // ����� � �������� �� ������ �����
    private Color originalColor;
    private Image buttonImage;
    public GameObject movingObject; // ������, ������� ����� ��������� ����
    public float moveDistance = 100f; // ���������� ��������
    private Animator animator;
    private CoroutineManager coroutineManager;
    private PlayerMovement playerMovement;

    void Start()
    {
        buttonImage = GetComponent<Image>();
        originalColor = buttonImage.color;
        coroutineManager = FindObjectOfType<CoroutineManager>();

        if (movingObject != null)
        {
            animator = movingObject.GetComponent<Animator>();
            playerMovement = movingObject.GetComponent<PlayerMovement>();
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // ����������� ������� ������
        clickCount++;

        // ������ ���� ������� �� �������
        buttonImage.color = Color.red;

        // ���� ������ 5 ���, �������� ������ � ������ ������
        if (clickCount >= 5)
        {
            gameObject.SetActive(false);
            if (otherObject != null)
            {
                otherObject.SetActive(false);
            }

            // �������� PlayerMovement �� ����������� ������
            if (playerMovement != null)
            {
                playerMovement.SetButtonDestroyed(true);
            }

            // ��������� �������� � ����������� ����� CoroutineManager
            if (animator != null && coroutineManager != null)
            {
                coroutineManager.StartMoveObjectCoroutine(movingObject, moveDistance, 5f, animator);
            }
        }
        else
        {
            // ��������� �������� ��� ������ �����
            StartCoroutine(ResetColorAfterTime(resetTime));
        }
    }

    private IEnumerator ResetColorAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        buttonImage.color = originalColor;
    }
}
