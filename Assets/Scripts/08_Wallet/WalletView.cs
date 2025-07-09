using TMPro;
using UnityEngine;

public class WalletView : MonoBehaviour
{
    [SerializeField] private TMP_Text value;

    private Wallet wallet;

    public void Initialize(Wallet _wallet)
    {
        wallet = _wallet;

        UpdateValue(wallet.GetCurrentCoins());

        wallet.CoinsChanged += UpdateValue;

    }

    private void OnDestroy() => wallet.CoinsChanged -= UpdateValue;

    private void UpdateValue(int _value) => value.text = _value.ToString();
}
