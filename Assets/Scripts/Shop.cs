using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [SerializeField] private ShopContent contentItems;

    [SerializeField] private ShopCategoryButton characterSkinsButton;
    [SerializeField] private ShopCategoryButton weaponsButton;

    [SerializeField] private BuyButton buyButton;
    [SerializeField] private Button selectionButton;
    [SerializeField] private Image selectedText;

    [SerializeField] private ShopPanel shopPanel;

    [SerializeField] private SkinPlacement skinPlacement;

    [SerializeField] AudioSource audioSource;
    [SerializeField] ParticleSystem buySuccessPrefab;

    private IDataProvider dataProvider;

    private ShopItemView previewedItem;

    private Wallet wallet;

    private SkinSelector skinSelector;
    private SkinUnlocker skinUnlocker;
    private OpenSkinsChecker openSkinsChecker;
    private SelectedSkinChecker selectedSkinChecker;
    private ParticleSystem buySuccess;

    private void OnEnable()
    {
        characterSkinsButton.Click += OnCharacterSkinsButtonClick;
        weaponsButton.Click += OnBoostsButtonClick;


        buyButton.Click += OnBuyButtonClick;
        selectionButton.onClick.AddListener(OnSelectionButtonClick);
    }

    private void OnDisable()
    {
        characterSkinsButton.Click -= OnCharacterSkinsButtonClick;
        weaponsButton.Click -= OnBoostsButtonClick;
        shopPanel.ItemViewClicked -= OnItemViewClicked;

        buyButton.Click -= OnBuyButtonClick;
        selectionButton.onClick.RemoveListener(OnSelectionButtonClick);
    }

    public void Initialize(IDataProvider _dataProvider, Wallet _wallet, OpenSkinsChecker _openSkinsChecker, SelectedSkinChecker _selectedSkinChecker, SkinSelector _skinSelector, SkinUnlocker _skinUnlocker)
    {
        wallet = _wallet;
        openSkinsChecker = _openSkinsChecker;
        selectedSkinChecker = _selectedSkinChecker;
        skinSelector = _skinSelector;
        skinUnlocker = _skinUnlocker;

        dataProvider = _dataProvider;

        shopPanel.Initialize(_openSkinsChecker, _selectedSkinChecker);

        shopPanel.ItemViewClicked += OnItemViewClicked;

        OnCharacterSkinsButtonClick();
    }

    private void OnItemViewClicked(ShopItemView item)
    {
        previewedItem = item;

        skinPlacement.InstantiateModel(previewedItem.Model, previewedItem.Item);

        openSkinsChecker.Visit(previewedItem.Item);

        if (openSkinsChecker.IsOpened)
        {
            selectedSkinChecker.Visit(previewedItem.Item);

            if (selectedSkinChecker.IsSelected)
            {
                ShowSelectedText();
                return;
            }

            ShowSelectionButton();
        }
        else
        {
            ShowBuyButton(previewedItem.Price);
        }
    }

    private void OnBuyButtonClick()
    {
        if (wallet.IsEnough(previewedItem.Price))
        {
            wallet.Spend(previewedItem.Price);
            skinUnlocker.Visit(previewedItem.Item);
            SelectSkin();
            previewedItem.Unlock();
            dataProvider.Save();

            audioSource.Play();
            buySuccess = Instantiate(buySuccessPrefab, skinPlacement.transform.position, Quaternion.identity);
            buySuccess.Play();
        }
    }

    private void OnSelectionButtonClick()
    {
        SelectSkin();
        dataProvider.Save();
    }

    private void OnCharacterSkinsButtonClick()
    {
        characterSkinsButton.Select();
        weaponsButton.Unselect();
        shopPanel.Show(contentItems.CharacterSkinItems.Cast<ShopItem>());
    }

    private void OnBoostsButtonClick()
    {
        characterSkinsButton.Unselect();
        weaponsButton.Select();
        shopPanel.Show(contentItems.BoostItems.Cast<ShopItem>());
    }

    private void SelectSkin()
    {
        skinSelector.Visit(previewedItem.Item);
        shopPanel.Select(previewedItem);
        ShowSelectedText();
    }

    private void ShowSelectionButton()
    {
        selectionButton.gameObject.SetActive(true);
        HideBuyButton();
        HideSelectedText();
    }

    private void ShowSelectedText()
    {
        selectedText.gameObject.SetActive(true);
        HideSelectoinButton();
        HideBuyButton();
    }

    private void ShowBuyButton(int price)
    {
        buyButton.gameObject.SetActive(true);
        buyButton.UpdateText(price);

        if (wallet.IsEnough(price))
            buyButton.Unlock();
        else
            buyButton.Lock();

        HideSelectedText();
        HideSelectoinButton();
    }

    private void HideBuyButton() => buyButton.gameObject.SetActive(false);
    private void HideSelectoinButton() => selectionButton.gameObject.SetActive(false);
    private void HideSelectedText() => selectedText.gameObject.SetActive(false);
}
