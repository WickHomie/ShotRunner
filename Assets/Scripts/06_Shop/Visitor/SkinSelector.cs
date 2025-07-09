public class SkinSelector : IShopItemVisitor
{
    private IPersistentData persistentData;

    public SkinSelector(IPersistentData _persistentData) => persistentData = _persistentData;

    public void Visit(ShopItem shopItem) => Visit((dynamic)shopItem);

    public void Visit(CharacterSkinItem characterSkinItem) => persistentData.PlayerData.SelectedCharacterSkin = characterSkinItem.SkinType;

    public void Visit(BoostItem boostItem) => persistentData.PlayerData.SelectedBoostSkin = boostItem.SkinType;
}
