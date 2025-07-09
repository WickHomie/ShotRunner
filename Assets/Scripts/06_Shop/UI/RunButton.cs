using UnityEngine;
using DG.Tweening;

public class RunButton : MonoBehaviour
{
    private Tween moveAnim;
    private Tween scaleAnim;

    void Start()
    {
        Vector3 startPos = transform.localPosition;

        Vector3[] path = new Vector3[]
        {
            startPos + new Vector3(15f, 0, 0),
            startPos + new Vector3(15f, 8f, 0),
            startPos + new Vector3(0, 8f, 0),
            startPos
        };

        moveAnim = transform.DOLocalPath(path, 6f, PathType.CatmullRom)
            .SetEase(Ease.InOutSine)
            .SetLoops(-1, LoopType.Restart);
    }

    public void OnButtonClick()
    {
        if (moveAnim != null && moveAnim.IsActive())
            moveAnim.Kill();

        scaleAnim = transform.DOScale(Vector3.zero, 0.5f).SetEase(Ease.InBack).OnComplete(() =>
            {
                gameObject.SetActive(false);
            });
    }

    private void OnDestroy()
    {
        if (moveAnim != null && moveAnim.IsActive())
            moveAnim.Kill();

        if (scaleAnim != null && scaleAnim.IsActive())
            scaleAnim.Kill();
    }

}
