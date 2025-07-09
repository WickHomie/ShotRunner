using UnityEngine;

public class SkinPlacement : MonoBehaviour
{
    [SerializeField] private Rotator rotator;

    private GameObject currentCharacter;
    private GameObject currentWeapon;
    private Animator characterAnimator;
    private RuntimeAnimatorController defaultAnimator;
    private RuntimeAnimatorController lastWeaponAnimator;

    private string weaponBoneName = "root/pelvis/spine_01/spine_02/spine_03/clavicle_r/upperarm_r/lowerarm_r/hand_r/socket_r";

    private void Start()
    {
        rotator.ResetRotation();
    }

    public void InstantiateModel(GameObject model, ShopItem itemData)
    {
        if (itemData is BoostItem weaponItem)
        {
            // Оружие - сохраняем аниматор и применяем
            if (weaponItem.WeaponAnimator != null)
            {
                lastWeaponAnimator = weaponItem.WeaponAnimator;
                ApplyCurrentAnimator();
            }

            if (currentWeapon != null) Destroy(currentWeapon);

            if (currentCharacter != null)
            {
                Transform weaponBone = currentCharacter.transform.Find(weaponBoneName);
                if (weaponBone != null)
                {
                    currentWeapon = Instantiate(model, weaponBone);
                    currentWeapon.transform.localPosition = Vector3.zero;
                    currentWeapon.transform.localRotation = Quaternion.identity;
                }
                else
                {
                    // Если кость не найдена, fallback на transform этого объекта
                    currentWeapon = Instantiate(model, transform);
                }
            }
            else
            {
                currentWeapon = Instantiate(model, transform);
            }
        }
        else
        {
            if (currentCharacter != null) Destroy(currentCharacter);

            currentCharacter = Instantiate(model, transform);
            characterAnimator = currentCharacter.GetComponent<Animator>();
            defaultAnimator = characterAnimator.runtimeAnimatorController;
            rotator.SetTarget(currentCharacter.transform);

            ApplyCurrentAnimator(); // Применяем сохраненный аниматор оружия

            // Если уже есть оружие — перевесить его на новую кость
            if (currentWeapon != null)
            {
                Transform weaponBone = currentCharacter.transform.Find(weaponBoneName);
                if (weaponBone != null)
                {
                    currentWeapon.transform.SetParent(weaponBone);
                    currentWeapon.transform.localPosition = Vector3.zero;
                    currentWeapon.transform.localRotation = Quaternion.identity;
                }
                else
                {
                    currentWeapon.transform.SetParent(transform);
                }
            }
        }
    }

    private void ApplyCurrentAnimator()
    {
        if (characterAnimator == null) return;

        // Приоритет: последний аниматор оружия > дефолтный аниматор персонажа
        characterAnimator.runtimeAnimatorController = lastWeaponAnimator != null
            ? lastWeaponAnimator
            : defaultAnimator;
    }

}
