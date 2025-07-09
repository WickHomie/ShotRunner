using DG.Tweening;
using UnityEngine;

public class Popup : MonoBehaviour
{
    [SerializeField] private CanvasGroup bodyAlphaGroup;

    public void Hide(System.Action onComplete = null)
    {
        bodyAlphaGroup.DOFade(0, 0.5f)
            .SetUpdate(true)
            .OnComplete(() =>
            {
                gameObject.SetActive(true);
                onComplete?.Invoke();
            });
    }
}


