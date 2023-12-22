using Lean.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnIconWeapons : MonoBehaviour
{
    [SerializeField]GameObject weapon;
    void Start()
    {
        CreateWeapon();
    }

    private void CreateWeapon()
    {
        for (int i = 0; i < 3; i++)
        {
            GameObject iconweapon = LeanPool.Spawn(weapon, Vector3.zero, Quaternion.identity, transform);
            iconweapon.GetComponentInChildren<Text>().text = "Weapon "+(i+1);
        }
    }
}
