using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;

public class CustomButtonHandler : MonoBehaviour, IPointerClickHandler
{
    private int clickCount = 0;
    public GameObject otherObject; // Другой объект, который будет скрыт
    public float resetTime = 5f; // Время в секундах до сброса цвета
    private Color originalColor;
    private Image buttonImage;
    public GameObject movingObject; // Объект, который будет двигаться вниз
    public float moveDistance = 100f; // Расстояние движения
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
        // Увеличиваем счетчик кликов
        clickCount++;

        // Меняем цвет объекта на красный
        buttonImage.color = Color.red;

        // Если нажато 5 раз, скрываем объект и другой объект
        if (clickCount >= 5)
        {
            gameObject.SetActive(false);
            if (otherObject != null)
            {
                otherObject.SetActive(false);
            }

            // Сообщаем PlayerMovement об уничтожении кнопки
            if (playerMovement != null)
            {
                playerMovement.SetButtonDestroyed(true);
            }

            // Активация анимации и перемещение через CoroutineManager
            if (animator != null && coroutineManager != null)
            {
                coroutineManager.StartMoveObjectCoroutine(movingObject, moveDistance, 5f, animator);
            }
        }
        else
        {
            // Запускаем корутину для сброса цвета
            StartCoroutine(ResetColorAfterTime(resetTime));
        }
    }

    private IEnumerator ResetColorAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        buttonImage.color = originalColor;
    }
}
