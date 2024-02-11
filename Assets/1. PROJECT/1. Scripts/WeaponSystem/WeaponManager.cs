using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public int leftHandWeaponID;
    public int rightHandWeaponID;
    public WeaponData[] allWeapons;
    public Transform leftHandHolder;
    public Transform rightHandHolder;

    private IWeapon leftHandWeapon;
    private IWeapon rightHandWeapon;

    void Start()
    {
        EquipWeapon(leftHandWeaponID, true);
        EquipWeapon(rightHandWeaponID, false);
    }

    void Update()
    {
        if (Input.GetMouseButton(0) && leftHandWeapon != null) // ЛКМ
        {
            leftHandWeapon.UseWeapon();
        }

        if (Input.GetMouseButton(1) && rightHandWeapon != null) // ПКМ
        {
            rightHandWeapon.UseWeapon();
        }
    }

    public void EquipWeapon(int weaponID, bool isLeftHand)
    {
        WeaponData weaponData = FindWeaponDataById(weaponID);
        if (weaponData == null)
        {
            Debug.LogError("Weapon data not found for ID: " + weaponID);
            return;
        }

        Transform weaponHolder = isLeftHand ? leftHandHolder : rightHandHolder;
        if (weaponHolder.childCount > 0)
        {
            Destroy(weaponHolder.GetChild(0).gameObject); // Удаление старого оружия
        }

        GameObject weaponObject = Instantiate(weaponData.weaponPrefab, weaponHolder);
        IWeapon weaponComponent = weaponObject.GetComponent<IWeapon>();
        if (weaponComponent == null)
        {
            Debug.LogError("Weapon prefab does not have a component that implements IWeapon");
            return;
        }

        if (isLeftHand)
            leftHandWeapon = weaponComponent;
        else
            rightHandWeapon = weaponComponent;
    }

    private WeaponData FindWeaponDataById(int id)
    {
        foreach (WeaponData weapon in allWeapons)
        {
            if (weapon.weaponID == id)
                return weapon;
        }
        return null;
    }
}

