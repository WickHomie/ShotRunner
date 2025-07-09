using DG.Tweening;
using UnityEngine;

public class MainMenuSettings : MonoBehaviour
{
    [SerializeField] private CanvasGroup bodyAlphaGroup;
    [SerializeField] private GameObject anticlicker;
    [SerializeField] private GameObject settingPanel;

    private void Awake()
    {
        bodyAlphaGroup.alpha = 0;
        settingPanel.SetActive(false);
    }

    public void Show()
    {
        anticlicker.SetActive(true);
        settingPanel.SetActive(true);
        bodyAlphaGroup.DOFade(1, 0.5f);        
    }

    public void Hide()
    {
        anticlicker.SetActive(false);
        settingPanel.SetActive(false);
        bodyAlphaGroup.DOFade(0, 0.5f);
    }
}
