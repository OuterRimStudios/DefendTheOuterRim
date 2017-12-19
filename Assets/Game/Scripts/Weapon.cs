using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    //Custom Variables
    public enum WeaponType
    {
        None,
        Hitscan,
        Projectile,
        Sustained
    };

    [Header("Weapon Creator"), Space]
    public WeaponType weaponType = WeaponType.None;

    public Camera myCamera;
    public int minDamage;
    public int maxDamage;
    public int critChance;
    public float critDamageMultiplier = 1f;
    public float fireFrequency = 1f;

    public int successiveShots;
    public float timeBetweenSuccessiveShots;

    bool isFiring;

    public bool hasResource;

    public bool hasAmmo;
    public int  ammo;
    public bool unlimitedAmmo;

    [Space]
    public bool multifire;
    public bool alternateFire;

    bool outOfAmmo;

    [Space]
    public bool hasReload;
    public float reloadTime;

    bool reloading;

    [Space]
    public bool hasOverheat;
    public float maxHeatAmount;
    public float heatIncreaseAmount;
    public float heatDecreaseAmount;
    public float heatIncreaseFrequency;
    public float heatDecreaseFrequency;

    public float heatIncreaseDelay;
    public float overheatedLength;

    [Space]
    public bool hasCooldown;
    public float cooldownLength;
    public bool startCooldownWhenOutOfAmmo;
    public bool startCooldownOnFire;
    public float startCooldownAfter;

    bool onCooldown;

    [Space]
    public bool hasCharges;
    public int maxCharges;

    int charges;


    [Space, Tooltip("There is a small delay before the weapon fires.")]
    public bool isDelayed;
    public GameObject delayEffect;
    public float delayTime;

    float heatAmount;
    bool overheating;

    [Space]
    public bool autoFire;
    public string targetTag;
    public float detectionDistance;

    [Space]
    public bool canAim;
    public Vector3 aimPosition;
    public Vector3 aimRotation;

    Vector3 baseAimPosition;
    Vector3 baseAimRotation;

    [Space]
    public bool aimAssist;
    public float aimAssistAccuracy;

    public bool applyDotOnHit;

    [Space]
    public GameObject[] weaponFireEffects;
    public GameObject[] weaponFirePositions;
    public Animator weaponAnimator;

    [Space, Header("Damage Over Time")]
    public GameObject damageOverTimeEffect;
    public float timeBetweenDamage;
    [Tooltip("How long the target will be taking ticking damage.")]
    public float dotLength;

    [Space, Header("HitScan")]
    public LayerMask targetableLayer;
    public float hitScanAttackRange;
    public float hitScanAccuracy;
    public GameObject targetHitEffect;
    public GameObject environmentHitEffect;

    [Space, Header("Projectile")]
    public ObjectPooling projectile;
    [Tooltip("Can the projectile stick to a target?")]
    public bool isSticky;

    [Space, Header("Sustained")]
    public Collider damageCollider;
    public GameObject sustainedEffect;
    public float sustainedEffectFrequency;
    public float sustainedDamageFrequency;

    //Internal Variables

    bool delayActive;
    int alternateFireCount;

    public virtual IEnumerator Fire()
    {
        //Runs if the weapon is delayed.
        if (isDelayed && !delayActive)
        {
            delayActive = true;
            StartCoroutine(Delay());
        }

        if (!isFiring)
        {
            isFiring = true;

            for (int i = 0; i < successiveShots; i++)
            {
                if (weaponAnimator)
                    weaponAnimator.SetTrigger("Fire");

                switch (weaponType)
                {
                    case WeaponType.Hitscan:
                        FireHitScan();
                        break;
                    case WeaponType.Projectile:
                        FireProjectile();
                        break;
                    case WeaponType.Sustained:
                        FireSustained();
                        break;
                }
                yield return new WaitForSeconds(timeBetweenSuccessiveShots);
            }

            StartCoroutine(IsFiring());
        }
    }

    IEnumerator Delay()
    {
        if(delayEffect != null)
        delayEffect.SetActive(true);

        yield return new WaitForSeconds(delayTime);

        if (delayEffect != null)
            delayEffect.SetActive(false);

        delayActive = false;
    }

    void FireHitScan()
    {
        //I need a public reference to the camera object and the hitscan layermask.

        RaycastHit hit;
        Vector3 spreadAmount = new Vector3(Random.Range(-hitScanAccuracy, hitScanAccuracy),
            Random.Range(-hitScanAccuracy, hitScanAccuracy),
            Random.Range(-hitScanAccuracy, hitScanAccuracy));

        if(Physics.Raycast(myCamera.transform.position + spreadAmount, myCamera.transform.forward, out hit, hitScanAttackRange, targetableLayer))
        {
            Vector3 position = hit.point + (hit.normal * .1f);
            Quaternion rotation = Quaternion.LookRotation(hit.normal);
            
            if (hit.transform.tag.Equals("Enemy"))
            {
                //hit.transform.GetComponent<EnemyHealth>().TookDamage(RandomDamage());

                if (targetHitEffect != null)
                    Instantiate(targetHitEffect, position, rotation);
            }

            if (environmentHitEffect != null)
                Instantiate(environmentHitEffect, position, rotation);

        }
    }

    void FireProjectile()
    {
        if(multifire)
        {
            if(alternateFire)
            {

                print("alternateFireCount: " + alternateFireCount + " weaponFirePositions " + weaponFirePositions.Length);
                if (alternateFireCount <= weaponFirePositions.Length - 1)
                {
                    SpawnProjectile();

                    alternateFireCount++;
                }
                else
                {
                    alternateFireCount = 0;
                    SpawnProjectile();
                }
            }
            else
            {
                for(int i = 0; i < weaponFirePositions.Length; i++)
                {
                    GameObject obj = projectile.GetPooledObject();

                    if (obj == null)
                    {
                        return;
                    }
                    obj.transform.position = weaponFirePositions[i].transform.position;
                    obj.transform.rotation = weaponFirePositions[i].transform.rotation;
                    obj.SetActive(true);
                }
            }
        }
        else
        {
            GameObject obj = projectile.GetPooledObject();

            if (obj == null)
            {
                return;
            }
            obj.transform.position = weaponFirePositions[0].transform.position;
            obj.transform.rotation = weaponFirePositions[0].transform.rotation;
            obj.SetActive(true);
        }

        //Projectile projectileUpdate = myProjectile.GetComponent<Projectile>();
        //projectileUpdate.applyDotOnHit = applyDotOnHit;
        //myProjectile.GetComponent<Projectile>().isSticky = isSticky;
    }

    void SpawnProjectile()
    {
        GameObject obj = projectile.GetPooledObject();

        if (obj == null)
        {
            return;
        }
        obj.transform.position = weaponFirePositions[alternateFireCount].transform.position;
        obj.transform.rotation = weaponFirePositions[alternateFireCount].transform.rotation;
        obj.SetActive(true);
    }

    void FireSustained()
    {

    }

    IEnumerator IsFiring()
    {
        yield return new WaitForSeconds(fireFrequency);
        isFiring = false;
    }

    IEnumerator OnCooldown()
    {
        yield return new WaitForSeconds(cooldownLength);
        onCooldown = false;
    }

    int RandomDamage()
    {
		//Needs a critical strike multiplier
		int critRoll = Random.Range (0, 100);
		int damageRoll = Random.Range(minDamage, maxDamage);

		if (critRoll < critChance)
			return (int)Mathf.Round(damageRoll * critDamageMultiplier);
		else
			return damageRoll;
    }
}

