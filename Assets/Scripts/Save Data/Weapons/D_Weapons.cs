using UnityEngine;

[CreateAssetMenu(fileName = "Weapons", menuName = "Weapons/Weapon 1", order = 1)]
public class D_Weapons : ScriptableObject
{
    public string itemName;
    public bool isCheckBuy;
    public float timeFire;
    public int coolDownReload;
    public float damage;
}