using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateWeapon : MonoBehaviour
{
    public Weapon weapon;

    private void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
            Fire();
    }

    public void Fire ()
    {
        StartCoroutine(weapon.Fire());
	}
}
