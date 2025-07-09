using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float moveDistance = 10f;
    [SerializeField] private float minMoveSpeed = 3f;
    [SerializeField] private float maxMoveSpeed = 7f;
    [SerializeField] private float minPauseTime = 1f;
    [SerializeField] private float maxPauseTime = 3f;

    private Vector3 localStartPos;
    private Vector3 localUpPos;
    private Vector3 localDownPos;

    private void Start()
    {
        localStartPos = transform.localPosition;
        localUpPos = localStartPos + Vector3.up * moveDistance;
        localDownPos = localStartPos;
        StartCoroutine(MoveRoutine());
    }

    private IEnumerator MoveRoutine()
    {
        while (true)
        {
            float moveSpeed = Random.Range(minMoveSpeed, maxMoveSpeed);
            float pauseTime = Random.Range(minPauseTime, maxPauseTime);

            // Вверх относительно платформы
            yield return StartCoroutine(MoveToLocalPosition(localUpPos, moveSpeed));
            yield return new WaitForSeconds(pauseTime);

            moveSpeed = Random.Range(minMoveSpeed, maxMoveSpeed);
            pauseTime = Random.Range(minPauseTime, maxPauseTime);

            // Вниз относительно платформы
            yield return StartCoroutine(MoveToLocalPosition(localDownPos, moveSpeed));
            yield return new WaitForSeconds(pauseTime);
        }
    }

    private IEnumerator MoveToLocalPosition(Vector3 targetLocal, float moveSpeed)
    {
        while ((transform.localPosition - targetLocal).sqrMagnitude > 0.0001f)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, targetLocal, moveSpeed * Time.deltaTime);
            yield return null;
        }
        transform.localPosition = targetLocal;
    }
}
