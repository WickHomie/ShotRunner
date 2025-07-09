public class SelectedSkinChecker : IShopItemVisitor
{
    private IPersistentData persistentData;

    public bool IsSelected { get; private set; }

    public SelectedSkinChecker(IPersistentData _persistentData) => persistentData = _persistentData;

    public void Visit(ShopItem shopItem) => Visit((dynamic)shopItem);

    public void Visit(CharacterSkinItem characterSkinItem) => IsSelected = persistentData.PlayerData.SelectedCharacterSkin == characterSkinItem.SkinType;

    public void Visit(BoostItem boostItem) => IsSelected = persistentData.PlayerData.SelectedBoostSkin == boostItem.SkinType;
}
