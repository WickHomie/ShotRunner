using TMPro;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] TMP_Text coinCountText;

    WalletView walletView;
    
    int coin = 0;

    public void AddCoin(int amount)
    {     
        coin += amount;
        coinCountText.text = coin.ToString();
    }
}
