using UnityEngine;

public class GameplayBootstrap : MonoBehaviour
{
    [SerializeField] private Transform characterSpawnPoint;
    [SerializeField] private CharacterFactory characterFactory;
    [SerializeField] private GameObject player;
    [SerializeField] private WalletView walletView;
    [SerializeField] private LevelGenerator levelGenerator;
    [SerializeField] private WeaponFactory weaponFactory;

    public float rotation = 0f;

    private IDataProvider dataProvider;
    private IPersistentData persistentData;

    private Wallet wallet;

    private Character character;
    private Weapon weapon;
    Transform weaponAttachPoint;
    

    private void Awake()
    {
        InitializeData();
        InitializeWallet();
        DoSpawn();
    }

    private void DoSpawn()
    {
        character = characterFactory.Get(persistentData.PlayerData.SelectedCharacterSkin, characterSpawnPoint.position);
        character.transform.SetParent(characterSpawnPoint);
        character.transform.localPosition = Vector3.zero;
        character.transform.rotation = Quaternion.Euler(0, rotation, 0);

        weaponAttachPoint = character.GetWeaponBone();
        weapon = weaponFactory.Get(persistentData.PlayerData.SelectedBoostSkin);
        weapon.transform.SetParent(weaponAttachPoint);
        weapon.transform.localPosition = Vector3.zero;
        weapon.transform.localRotation = Quaternion.identity;

        InitializeAnimator(character.gameObject);
        InitializeAudioWeapon(weapon.gameObject);


    }

    private void InitializeData()
    {
        persistentData = new PersistentData();
        dataProvider = new DataLocalProvider(persistentData);

        LoadDataOrInit();
    }

    private void InitializeAnimator(GameObject character)
    {
        if (character != null)
        {
            Animator animator = character.GetComponent<Animator>();
            Movement movement = player.GetComponent<Movement>();
            if (movement != null)
            {
                movement.InitAnim(animator);
            }
            
        }
    }

    private void InitializeAudioWeapon(GameObject weapon)
    {
        if (weapon != null)
        {
            AudioSource weaponAudio = weapon.GetComponent<AudioSource>();
            Movement movement = player.GetComponent<Movement>();
            if (movement != null)
            {
                movement.InitSound(weaponAudio);
            }
            
        }
    }

    private void InitializeWallet()
    {
        wallet = new Wallet(persistentData);

        walletView.Initialize(wallet);

        levelGenerator.InitWallet(wallet, dataProvider);
    }
    

    private void LoadDataOrInit()
    {
        if (dataProvider.TryLoad() == false)
            persistentData.PlayerData = new PlayerData();
    }
}
