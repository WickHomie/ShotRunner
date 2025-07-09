using UnityEngine;

public class Coin : Pickup
{
    [SerializeField] int coinAmound = 0;
    [SerializeField] private AudioSource coin;
    [SerializeField] private MeshRenderer mesh;

    Wallet wallet;
    private IDataProvider dataProvider;

    private static int comboCount = 0;
    private static float lastPickupTime = -10f;
    private static float comboResetTime = 1f;


    public void Init(Wallet wallet, IDataProvider dataProvider)
    {
        this.wallet = wallet;
        this.dataProvider = dataProvider;
    }


    protected override void OnPickup()
    {
        wallet.AddCoins(coinAmound);
        dataProvider.Save();

        mesh.enabled = false;
        CoinAudioPlay();

        //Destroy(gameObject);
    }

    private void CoinAudioPlay()
    {
        float minPitch = 0.8f;
        float maxPitch = 1.2f;

        if (Time.time - lastPickupTime <= comboResetTime)
        {
            comboCount++;
        }
        else
        {
            comboCount = 1;
        }
        lastPickupTime = Time.time;

        float pitch = Mathf.Lerp(minPitch, maxPitch, Mathf.Clamp01((comboCount - 1) * 0.05f));
        coin.pitch = pitch;
        coin.Play();
    }
}
