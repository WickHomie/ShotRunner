using System;
using System.Collections.Generic;
using Newtonsoft.Json;

public class PlayerData
{
    private CharacterSkins selectedCharacterSkin;
    private BoostSkins selectedBoostSkin;

    private List<CharacterSkins> openCharacterSkins;
    private List<BoostSkins> openBoostSkins;

    private int money;

    public PlayerData()
    {
        money = 100000;

        selectedCharacterSkin = CharacterSkins.BasicMale;
        selectedBoostSkin = BoostSkins.Pistol;

        openCharacterSkins = new List<CharacterSkins>() { selectedCharacterSkin };
        openBoostSkins = new List<BoostSkins>() { selectedBoostSkin };
    }

    [JsonConstructor]
    public PlayerData(int _money, CharacterSkins _selectedCharacterSkin, BoostSkins _selectedBoostSkin,
        List<CharacterSkins> _openCharacterSkins, List<BoostSkins> _openBoostSkins)
    {
        Money = _money;

        selectedCharacterSkin = _selectedCharacterSkin;
        selectedBoostSkin = _selectedBoostSkin;

        openCharacterSkins = _openCharacterSkins != null ? new List<CharacterSkins>(_openCharacterSkins) : new List<CharacterSkins>();
        openBoostSkins = _openBoostSkins != null ? new List<BoostSkins>(_openBoostSkins) : new List<BoostSkins>();
    }

    public int Money
    {
        get => money;

        set
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException(nameof(value));

            money = value;
        }
    }

    public CharacterSkins SelectedCharacterSkin
    {
        get => selectedCharacterSkin;
        set => selectedCharacterSkin = value;
    }

    public BoostSkins SelectedBoostSkin
    {
        get => selectedBoostSkin;
        set => selectedBoostSkin = value;
    }

    public IEnumerable<CharacterSkins> OpenCharacterSkins => openCharacterSkins;
    public IEnumerable<BoostSkins> OpenBoostSkins => openBoostSkins;

    public void OpenCharacterSkin(CharacterSkins skin)
    {
        if (openCharacterSkins.Contains(skin))
            throw new ArgumentException(nameof(skin));

        openCharacterSkins.Add(skin);
    }

    public void OpenBoostSkin(BoostSkins skin)
    {
        if (openBoostSkins.Contains(skin))
            throw new ArgumentException(nameof(skin));

        openBoostSkins.Add(skin);
    }
}
