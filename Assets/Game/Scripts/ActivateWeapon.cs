using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateWeapon : MonoBehaviour
{
    public Weapon weapon;
    
	void Update ()
    {
		if(Input.GetKey(KeyCode.Mouse0))
        {
            StartCoroutine(weapon.Fire());
        }
	}
}
