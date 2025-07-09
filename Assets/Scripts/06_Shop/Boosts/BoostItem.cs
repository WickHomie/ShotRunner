using UnityEngine;

[CreateAssetMenu(fileName = "BoostItem", menuName = "Shop/WeaponItem")]
public class BoostItem : ShopItem
{
    [field: SerializeField] public BoostSkins SkinType { get; private set; }
    [field: SerializeField] public RuntimeAnimatorController WeaponAnimator { get; private set; }
}
