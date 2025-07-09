using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "ShopContent", menuName = "Shop/ShopContent")]
public class ShopContent : ScriptableObject
{
    [SerializeField] private List<CharacterSkinItem> characterSkinItems;
    [SerializeField] private List<BoostItem> boostItems;

    public IEnumerable<CharacterSkinItem> CharacterSkinItems => characterSkinItems;
    public IEnumerable<BoostItem> BoostItems => boostItems;

    private void OnValidate()
    {
        var characterSkinsDublicates = characterSkinItems
            .Where(item => item != null)
            .GroupBy(item => item.SkinType)
            .Where(group => group.Count() > 1);

        foreach (var duplicateGroup in characterSkinsDublicates)
        {
            Debug.LogError($"Duplicate CharacterSkinItem detected: SkinType = {duplicateGroup.Key}. Count = {duplicateGroup.Count()}");
            foreach (var item in duplicateGroup)
            {
                Debug.LogError($"Duplicate item: {item.name}", item);
            }
        }

        var boostItemsDublicates = boostItems
            .Where(item => item != null)
            .GroupBy(item => item.SkinType)
            .Where(group => group.Count() > 1);

        foreach (var duplicateGroup in boostItemsDublicates)
        {
            Debug.LogError($"Duplicate BoostItem detected: SkinType = {duplicateGroup.Key}. Count = {duplicateGroup.Count()}");
            foreach (var item in duplicateGroup)
            {
                Debug.LogError($"Duplicate item: {item.name}", item);
            }
        }
    }
}
