using System.Linq;

public class OpenSkinsChecker : IShopItemVisitor
{
    private IPersistentData persistentData;

    public bool IsOpened { get; private set; }

    public OpenSkinsChecker(IPersistentData _persistentData) => persistentData = _persistentData;

    public void Visit(ShopItem shopItem) => Visit((dynamic)shopItem);

    public void Visit(CharacterSkinItem characterSkinItem) => IsOpened = persistentData.PlayerData.OpenCharacterSkins.Contains(characterSkinItem.SkinType);

    public void Visit(BoostItem boostItem) => IsOpened = persistentData.PlayerData.OpenBoostSkins.Contains(boostItem.SkinType);

}
