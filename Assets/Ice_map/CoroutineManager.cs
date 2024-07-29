using UnityEngine;
using System.Collections;

public class CoroutineManager : MonoBehaviour
{
    public void StartMoveObjectCoroutine(GameObject movingObject, float moveDistance, float moveSpeed, Animator animator)
    {
        StartCoroutine(MoveObjectDown(movingObject, moveDistance, moveSpeed, animator));
    }

    private IEnumerator MoveObjectDown(GameObject movingObject, float moveDistance, float moveSpeed, Animator animator)
    {
        Vector3 startPosition = movingObject.transform.position;
        Vector3 endPosition = startPosition + new Vector3(0, -moveDistance, 0);
        float elapsedTime = 0;

        animator.SetBool("isRunning", false);
        animator.SetBool("isIdle3", false);

        while (elapsedTime < moveDistance / moveSpeed)
        {
            movingObject.transform.position = Vector3.Lerp(startPosition, endPosition, (elapsedTime * moveSpeed) / moveDistance);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        movingObject.transform.position = endPosition;
    }
}
