using UnityEngine;
using System;

[CreateAssetMenu(fileName = "CharactersFactory", menuName = "Gameplay/CharacterFactory")]
public class CharacterFactory : ScriptableObject
{
    [SerializeField] private Character basicMale;
    [SerializeField] private Character female;
    [SerializeField] private Character hoodie;
    [SerializeField] private Character cowboy;
    [SerializeField] private Character male;
    [SerializeField] private Character alien;
    [SerializeField] private Character bear;
    [SerializeField] private Character chemicalman;
    [SerializeField] private Character chiken;
    [SerializeField] private Character hero;
    [SerializeField] private Character jesteran;
    [SerializeField] private Character knight;
    [SerializeField] private Character ninja;
    [SerializeField] private Character salaryman;
    [SerializeField] private Character samurai;
    [SerializeField] private Character sniper;
    [SerializeField] private Character soldier;
    [SerializeField] private Character spaceman;
    [SerializeField] private Character terrorist;
    [SerializeField] private Character rambo;

    public Character Get(CharacterSkins skinType, Vector3 spawnPosition)
    {
        Character instance = Instantiate(GetPrefab(skinType), spawnPosition, Quaternion.identity, null);
        instance.Initialize();
        return instance;
    }

    private Character GetPrefab(CharacterSkins skinType)
    {
        switch (skinType)
        {
            case CharacterSkins.BasicMale:
                return basicMale;
            case CharacterSkins.Female:
                return female;
            case CharacterSkins.Hoodie:
                return hoodie;
            case CharacterSkins.Cowboy:
                return cowboy;
            case CharacterSkins.Male:
                return male;
            case CharacterSkins.Alien:
                return alien;
            case CharacterSkins.Bear:
                return bear;
            case CharacterSkins.Chemicalman:
                return chemicalman;
            case CharacterSkins.Chiken:
                return chiken;
            case CharacterSkins.Hero:
                return hero;
            case CharacterSkins.Jesteran:
                return jesteran;
            case CharacterSkins.Knight:
                return knight;
            case CharacterSkins.Ninja:
                return ninja;
            case CharacterSkins.Salaryman:
                return salaryman;
            case CharacterSkins.Samurai:
                return samurai;
            case CharacterSkins.Sniper:
                return sniper;
            case CharacterSkins.Soldier:
                return soldier;
            case CharacterSkins.Spaceman:
                return spaceman;
            case CharacterSkins.Terrorist:
                return terrorist;
            case CharacterSkins.Rambo:
                return rambo;

            default:
                throw new ArgumentException(nameof(skinType));
        }
    }
}
