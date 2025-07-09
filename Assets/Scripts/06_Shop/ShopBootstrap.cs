using UnityEngine;

public class ShopBootstrap : MonoBehaviour
{
    [SerializeField] private Shop shop;
    [SerializeField] private WalletView walletView;

    private IDataProvider dataProvider;
    private IPersistentData persistentData;

    private Wallet wallet;

    private void Awake()
    {
        InitializeData();
        InitializeWallet();
        InitializeShop();
    }

    private void InitializeData()
    {
        persistentData = new PersistentData();
        dataProvider = new DataLocalProvider(persistentData);

        LoadDataOrInit();
    }

    private void InitializeWallet()
    {
        wallet = new Wallet(persistentData);

        walletView.Initialize(wallet);
    }

    private void InitializeShop()
    {
        OpenSkinsChecker openSkinsChecker = new OpenSkinsChecker(persistentData);
        SelectedSkinChecker selectedSkinChecker = new SelectedSkinChecker(persistentData);
        SkinSelector skinSelector = new SkinSelector(persistentData);
        SkinUnlocker skinUnlocker = new SkinUnlocker(persistentData);

        shop.Initialize(dataProvider, wallet, openSkinsChecker, selectedSkinChecker, skinSelector, skinUnlocker);
    }

    private void LoadDataOrInit()
    {
        if (dataProvider.TryLoad() == false)
            persistentData.PlayerData = new PlayerData();
    }
}
