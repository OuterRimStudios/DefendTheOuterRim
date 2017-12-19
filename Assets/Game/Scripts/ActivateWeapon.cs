using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateWeapon : MonoBehaviour
{
    public Weapon weapon;
    
	public void Fire ()
    {
        StartCoroutine(weapon.Fire());
	}
}
