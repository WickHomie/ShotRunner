using UnityEngine;

[CreateAssetMenu(fileName = "ShopItemViewFactory", menuName = "Shop/ShopItemViewFactory")]
public class ShopItemViewFactory : ScriptableObject
{
    [SerializeField] private ShopItemView characterSkinItemPrefab;
    [SerializeField] private ShopItemView boostItemPrefab;

    public ShopItemView Get(ShopItem shopItem, Transform parent)
    {
        ShopItemVisitor visitor = new ShopItemVisitor(characterSkinItemPrefab, boostItemPrefab);
        visitor.Visit(shopItem);

        ShopItemView instance = Instantiate(visitor.Prefab, parent);
        instance.Initialize(shopItem);
        return instance;
    }

    private class ShopItemVisitor : IShopItemVisitor
    {
        private ShopItemView characterSkinItemPrefab;
        private ShopItemView boostItemPrefab;

        public ShopItemVisitor(ShopItemView _characterSkinItemPrefab, ShopItemView _boostItemPrefab)
        {
            characterSkinItemPrefab = _characterSkinItemPrefab;
            boostItemPrefab = _boostItemPrefab;
        }
        public ShopItemView Prefab { get; private set; }

        public void Visit(ShopItem shopItem) => Visit((dynamic)shopItem);

        public void Visit(CharacterSkinItem characterSkinItem) => Prefab = characterSkinItemPrefab;

        public void Visit(BoostItem boostItem) => Prefab = boostItemPrefab;
    }
}
