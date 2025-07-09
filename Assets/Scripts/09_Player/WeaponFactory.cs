using UnityEngine;
using System;

[CreateAssetMenu(fileName = "WeaponsFactory", menuName = "Gameplay/WeaponFactory")]
public class WeaponFactory : ScriptableObject
{
    [SerializeField] private Weapon pistol;
    [SerializeField] private Weapon HMG;
    [SerializeField] private Weapon SMG;
    [SerializeField] private Weapon shotgun;
    [SerializeField] private Weapon assaultRifle;
    [SerializeField] private Weapon sniperRifle;
    [SerializeField] private Weapon huntingRifle;
    [SerializeField] private Weapon chemicalGun;
    [SerializeField] private Weapon electricGun;
    [SerializeField] private Weapon iceGun;
    [SerializeField] private Weapon railGun;
    [SerializeField] private Weapon crossBow;
    [SerializeField] private Weapon sawGun;
    [SerializeField] private Weapon gravityGun;
    [SerializeField] private Weapon flameThrower;
    [SerializeField] private Weapon bazooka;
    [SerializeField] private Weapon miniGun;

    public Weapon Get(BoostSkins weaponType)
    {
        Weapon instance = Instantiate(GetPrefab(weaponType));
        instance.Initialize();
        return instance;
    }

    private Weapon GetPrefab(BoostSkins weaponType)
    {
        switch (weaponType)
        {
            case BoostSkins.Pistol:
                return pistol;
            case BoostSkins.HMG:
                return HMG;
            case BoostSkins.SMG:
                return SMG;
            case BoostSkins.Shotgun:
                return shotgun;
            case BoostSkins.AssaultRifle:
                return assaultRifle;
            case BoostSkins.SniperRifle:
                return sniperRifle;
            case BoostSkins.HuntingRifle:
                return huntingRifle;
            case BoostSkins.ChemicalGun:
                return chemicalGun;
            case BoostSkins.ElectricGun:
                return electricGun;
            case BoostSkins.IceGun:
                return iceGun;
            case BoostSkins.RailGun:
                return railGun;
            case BoostSkins.CrossBow:
                return crossBow;
            case BoostSkins.SawGun:
                return sawGun;
            case BoostSkins.GravityGun:
                return gravityGun;
            case BoostSkins.FlameThrower:
                return flameThrower;
            case BoostSkins.Bazooka:
                return bazooka;
            case BoostSkins.MiniGun:
                return miniGun;

            default:
                throw new ArgumentException(nameof(weaponType));
        }
    }
}
