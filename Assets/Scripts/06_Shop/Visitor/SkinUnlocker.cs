public class SkinUnlocker : IShopItemVisitor
{
    private IPersistentData persistentData;

    public SkinUnlocker(IPersistentData _persistentData) => persistentData = _persistentData;

    public void Visit(ShopItem shopItem) => Visit((dynamic)shopItem);

    public void Visit(CharacterSkinItem characterSkinItem) => persistentData.PlayerData.OpenCharacterSkin(characterSkinItem.SkinType);

    public void Visit(BoostItem boostItem) => persistentData.PlayerData.OpenBoostSkin(boostItem.SkinType);
    
}
