using System;
using UnityEngine;
using UnityEngine.UI;

public class ShopCategoryButton : MonoBehaviour
{
    public event Action Click;

    [SerializeField] private Button button;

    [SerializeField] private Image image;
    [SerializeField] private Color selectColor;
    [SerializeField] private Color unselectColor;

    private void OnEnable() => button.onClick.AddListener(OnClick);
    private void OnDisable() => button.onClick.RemoveListener(OnClick);

    public void Select() => image.color = selectColor;
    public void Unselect() => image.color = unselectColor;

    private void OnClick() => Click?.Invoke();
}
