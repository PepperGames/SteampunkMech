using UnityEngine;

[CreateAssetMenu(fileName = "NewMechWeapon", menuName = "Mech Weapons")]
public class WeaponData : ScriptableObject
{
    public int weaponID;
    public float damage;
    public float attackRate;
    public int ammo;
    public float reloadTime; // Время перезарядки
    public enum WeaponType { Melee, Ranged }
    public WeaponType weaponType;
    public GameObject weaponPrefab;
}