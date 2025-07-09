using UnityEngine;

public class MainMenuBootstrap : MonoBehaviour
{
    [SerializeField] private Transform characterSpawnPoint;
    [SerializeField] private CharacterFactory characterFactory;
    [SerializeField] private WeaponFactory weaponFactory;

    public float rotation = 0f;

    private IDataProvider dataProvider;
    private IPersistentData persistentData;

    private void Awake()
    {
        InitializeData();
        DoSpawn();
    }

    private void DoSpawn()
    {
        Character character = characterFactory.Get(persistentData.PlayerData.SelectedCharacterSkin, characterSpawnPoint.position);
        Transform weaponAttachPoint = character.GetWeaponBone();
        character.transform.SetParent(characterSpawnPoint);
        character.transform.localPosition = Vector3.zero;
        character.transform.rotation = Quaternion.Euler(0, rotation, 0);

        Weapon weapon = weaponFactory.Get(persistentData.PlayerData.SelectedBoostSkin);

        weapon.transform.SetParent(weaponAttachPoint);
        weapon.transform.localPosition = Vector3.zero;
        weapon.transform.localRotation = Quaternion.identity;
    }

    private void InitializeData()
    {
        persistentData = new PersistentData();
        dataProvider = new DataLocalProvider(persistentData);

        LoadDataOrInit();
    }

    private void LoadDataOrInit()
    {
        if (dataProvider.TryLoad() == false)
            persistentData.PlayerData = new PlayerData();
    }

    public void RebootSave()
    {
        dataProvider.ResetSave();
    }
}
