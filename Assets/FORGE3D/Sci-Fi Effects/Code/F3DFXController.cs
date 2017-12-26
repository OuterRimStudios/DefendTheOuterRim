using UnityEngine;
using System.Collections;
using System;

namespace Forge3D
{
// Weapon types
    public enum GunTypes
    {
        Vulcan,
        SoloGun,
        Sniper,
        ShotGun,
        Seeker,
        RailGun,
        PlasmaGun,
        PlasmaBeam,
        PlasmaBeamHeavy,
        LightningGun,
        FlameRed,
        LaserImpulse
    }

    public class F3DFXController : MonoBehaviour
    {
        // Singleton instance
        public static F3DFXController instance;

        // GUI captions
        string[] GunTypes =
        {
            "Vulcan", "Sologun", "Sniper", "Shotgun", "Seeker", "Railgun", "Plasmagun", "Plasma beam",
            "Heavy plasma beam", "Lightning gun", "Flamethrower", "Pulse laser"
        };

        // Current firing socket
        int currentBarrel = 0;
        // Timer reference                
        int timerID = -1;

        [Header("Turret setup")] public Transform[] barrels; // Sockets reference
        [HideInInspector]
        public ParticleSystem[] ShellParticles; // Bullet shells particle system

        public GunTypes defaultGunType; // Default starting weapon type

        public Transform projectile;
        public Transform muzzleFlash;
        public Transform impactEffect;
        public float gunOffset;

        public Transform beamEffect;

        public float fireFrequency;

        public bool isProjectile;
        public bool isBeam;

        void Awake()
        {
            // Initialize singleton  
            instance = this;

        /*    // Initialize bullet shells particles
            for (int i = 0; i < ShellParticles.Length; i++)
            {
                var em = ShellParticles[i].emission;
                em.enabled = false;
                ShellParticles[i].gameObject.SetActive(true);
            }*/
        }

        // Display GUI
        void OnGUI()
        {
            GUIStyle caption = new GUIStyle(GUI.skin.label);
            caption.fontSize = 25;
            caption.fontStyle = FontStyle.Bold;
            caption.wordWrap = false;

            GUIStyle tooltip = new GUIStyle(GUI.skin.label);
            tooltip.fontSize = 11;
            tooltip.wordWrap = false;

            GUILayout.BeginArea(new Rect(Screen.width/2 - 150, Screen.height - 150, 300, 120));

            GUILayout.BeginVertical();

            GUILayout.FlexibleSpace();

            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUILayout.Label(GunTypes[(int) defaultGunType], caption);
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();

            GUILayout.FlexibleSpace();

            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUILayout.Label("Press Left / Right arrow keys to switch", tooltip);
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();

            GUILayout.FlexibleSpace();

            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();

            if (GUILayout.Button("Previous", GUILayout.Width(90), GUILayout.Height(30)))
                PrevWeapon();

            GUILayout.FlexibleSpace();

            if (GUILayout.Button("Next", GUILayout.Width(90), GUILayout.Height(30)))
                NextWeapon();

            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            GUILayout.FlexibleSpace();

            GUILayout.EndVertical();

            GUILayout.EndArea();
        }

        void Update()
        {
            // Switch weapon types using keyboard keys
            if (Input.GetKeyDown(KeyCode.RightArrow))
                NextWeapon();
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
                PrevWeapon();
        }

        // Switch to next weapon type
        void NextWeapon()
        {
            if ((int) defaultGunType < Enum.GetNames(typeof (GunTypes)).Length - 1)
            {
                defaultGunType++;
            }
        }

        // Switch to previous weapon type
        void PrevWeapon()
        {
            if (defaultGunType > 0)
            {
                defaultGunType--;
            }
        }

        // Advance to next turret socket
        void AdvanceSocket()
        {
            currentBarrel++;
            if (currentBarrel >= barrels.Length)
                currentBarrel = 0;
        }

        // Fire turret weapon
        public void Fire()
        {
            switch (defaultGunType)
            {
                case Forge3D.GunTypes.Vulcan:
                    // Fire vulcan at specified rate until canceled
                    timerID = F3DTime.time.AddTimer(fireFrequency, Vulcan);
                    // Invoke manually before the timer ticked to avoid initial delay
                    Vulcan();
                    break;

                case Forge3D.GunTypes.SoloGun:
                    timerID = F3DTime.time.AddTimer(fireFrequency, SoloGun);
                    SoloGun();
                    break;

                case Forge3D.GunTypes.Sniper:
                    timerID = F3DTime.time.AddTimer(fireFrequency, Sniper);
                    Sniper();
                    break;

                case Forge3D.GunTypes.ShotGun:
                    timerID = F3DTime.time.AddTimer(fireFrequency, ShotGun);
                    ShotGun();
                    break;

                case Forge3D.GunTypes.Seeker:
                    timerID = F3DTime.time.AddTimer(fireFrequency, Seeker);
                    Seeker();
                    break;

                case Forge3D.GunTypes.RailGun:
                    timerID = F3DTime.time.AddTimer(fireFrequency, RailGun);
                    RailGun();
                    break;

                case Forge3D.GunTypes.PlasmaGun:
                    timerID = F3DTime.time.AddTimer(fireFrequency, PlasmaGun);
                    PlasmaGun();
                    break;

                case Forge3D.GunTypes.PlasmaBeam:
                    // Beams has no timer requirement
                    PlasmaBeam();
                    break;

                case Forge3D.GunTypes.PlasmaBeamHeavy:
                    // Beams has no timer requirement
                    PlasmaBeamHeavy();
                    break;

                case Forge3D.GunTypes.LightningGun:
                    // Beams has no timer requirement
                    LightningGun();
                    break;

                case Forge3D.GunTypes.FlameRed:
                    // Flames has no timer requirement
                    FlameRed();
                    break;

                case Forge3D.GunTypes.LaserImpulse:
                    timerID = F3DTime.time.AddTimer(fireFrequency, LaserImpulse);
                    LaserImpulse();
                    break;
            }
        }

        // Stop firing 
        public void Stop()
        {
            // Remove firing timer
            if (timerID != -1)
            {
                F3DTime.time.RemoveTimer(timerID);
                timerID = -1;
            }
            switch (defaultGunType)
            {
                case Forge3D.GunTypes.PlasmaBeam:
                    F3DAudioController.instance.PlasmaBeamClose(transform.position);
                    break;

                case Forge3D.GunTypes.PlasmaBeamHeavy:
                    F3DAudioController.instance.PlasmaBeamHeavyClose(transform.position);
                    break;

                case Forge3D.GunTypes.LightningGun:
                    F3DAudioController.instance.LightningGunClose(transform.position);
                    break;

                case Forge3D.GunTypes.FlameRed:
                    F3DAudioController.instance.FlameGunClose(transform.position);
                    break;
            }
        }

        // Fire vulcan weapon
        void Vulcan()
        {
            // Get random rotation that offset spawned projectile
            Quaternion offset = Quaternion.Euler(UnityEngine.Random.onUnitSphere); 
            // Spawn muzzle flash and projectile with the rotation offset at current socket position
            F3DPoolManager.Pools["GeneratedPool"].Spawn(muzzleFlash, barrels[currentBarrel].position,
                barrels[currentBarrel].rotation, barrels[currentBarrel]);
            GameObject newGO =
                F3DPoolManager.Pools["GeneratedPool"].Spawn(projectile,
                    barrels[currentBarrel].position + barrels[currentBarrel].forward,
                    offset*barrels[currentBarrel].rotation, null).gameObject;

            F3DProjectile proj = newGO.gameObject.GetComponent<F3DProjectile>();
            if (proj)
            {
                proj.SetOffset(gunOffset);
            }

            // Emit one bullet shell
            if(ShellParticles.Length > 0)
                ShellParticles[currentBarrel].Emit(1);

            // Play shot sound effect
            F3DAudioController.instance.VulcanShot(barrels[currentBarrel].position);

            // Advance to next turret socket
            AdvanceSocket();
        }

        // Spawn vulcan weapon impact
        public void VulcanImpact(Vector3 pos)
        {
            // Spawn impact prefab at specified position
            F3DPoolManager.Pools["GeneratedPool"].Spawn(impactEffect, pos, Quaternion.identity, null);
            // Play impact sound effect
            F3DAudioController.instance.VulcanHit(pos);
        }

        // Fire sologun weapon
        void SoloGun()
        {
            Quaternion offset = Quaternion.Euler(UnityEngine.Random.onUnitSphere);
            F3DPoolManager.Pools["GeneratedPool"].Spawn(muzzleFlash, barrels[currentBarrel].position,
                barrels[currentBarrel].rotation, barrels[currentBarrel]);
            GameObject newGO =
                F3DPoolManager.Pools["GeneratedPool"].Spawn(projectile,
                    barrels[currentBarrel].position + barrels[currentBarrel].forward,
                    offset*barrels[currentBarrel].rotation, null).gameObject as GameObject;
            F3DProjectile proj = newGO.GetComponent<F3DProjectile>();
            if (proj)
            {
                proj.SetOffset(gunOffset);
            }
            F3DAudioController.instance.SoloGunShot(barrels[currentBarrel].position);
            AdvanceSocket();
        }

        // Spawn sologun weapon impact
        public void SoloGunImpact(Vector3 pos)
        {
            F3DPoolManager.Pools["GeneratedPool"].Spawn(impactEffect, pos, Quaternion.identity, null);
            F3DAudioController.instance.SoloGunHit(pos);
        }

        // Fire sniper weapon
        void Sniper()
        {
            Quaternion offset = Quaternion.Euler(UnityEngine.Random.onUnitSphere);

            F3DPoolManager.Pools["GeneratedPool"].Spawn(muzzleFlash, barrels[currentBarrel].position,
                barrels[currentBarrel].rotation, barrels[currentBarrel]);
            GameObject newGO =
                F3DPoolManager.Pools["GeneratedPool"].Spawn(projectile, barrels[currentBarrel].position,
                    offset*barrels[currentBarrel].rotation, null).gameObject as GameObject;
            F3DBeam beam = newGO.GetComponent<F3DBeam>();
            if (beam)
            {
                beam.SetOffset(gunOffset);
            }
            F3DAudioController.instance.SniperShot(barrels[currentBarrel].position);
            if (ShellParticles.Length > 0)
                ShellParticles[currentBarrel].Emit(1);
            AdvanceSocket();
        }

        // Spawn sniper weapon impact
        public void SniperImpact(Vector3 pos)
        {
            F3DPoolManager.Pools["GeneratedPool"].Spawn(impactEffect, pos, Quaternion.identity, null);
            F3DAudioController.instance.SniperHit(pos);
        }

        // Fire shotgun weapon
        void ShotGun()
        {
            Quaternion offset = Quaternion.Euler(UnityEngine.Random.onUnitSphere);
            F3DPoolManager.Pools["GeneratedPool"].Spawn(muzzleFlash, barrels[currentBarrel].position,
                barrels[currentBarrel].rotation, barrels[currentBarrel]);
            F3DPoolManager.Pools["GeneratedPool"].Spawn(projectile, barrels[currentBarrel].position,
                offset*barrels[currentBarrel].rotation, null);
            F3DAudioController.instance.ShotGunShot(barrels[currentBarrel].position);
            if (ShellParticles.Length > 0)
                ShellParticles[currentBarrel].Emit(1);
            AdvanceSocket();
        }

        // Fire seeker weapon
        void Seeker()
        {
            Quaternion offset = Quaternion.Euler(UnityEngine.Random.onUnitSphere);
            F3DPoolManager.Pools["GeneratedPool"].Spawn(muzzleFlash, barrels[currentBarrel].position,
                barrels[currentBarrel].rotation, barrels[currentBarrel]);
            GameObject newGO =
                F3DPoolManager.Pools["GeneratedPool"].Spawn(projectile, barrels[currentBarrel].position,
                    offset*barrels[currentBarrel].rotation, null).gameObject as GameObject;
            F3DProjectile proj = newGO.GetComponent<F3DProjectile>();
            if (proj)
            {
                proj.SetOffset(gunOffset);
            }
            F3DAudioController.instance.SeekerShot(barrels[currentBarrel].position);
            AdvanceSocket();
        }

        // Spawn seeker weapon impact
        public void SeekerImpact(Vector3 pos)
        {
            F3DPoolManager.Pools["GeneratedPool"].Spawn(impactEffect, pos, Quaternion.identity, null);
            F3DAudioController.instance.SeekerHit(pos);
        }

        // Fire rail gun weapon
        void RailGun()
        {
            Quaternion offset = Quaternion.Euler(UnityEngine.Random.onUnitSphere);
            F3DPoolManager.Pools["GeneratedPool"].Spawn(muzzleFlash, barrels[currentBarrel].position,
                barrels[currentBarrel].rotation, barrels[currentBarrel]);
            GameObject newGO =
                F3DPoolManager.Pools["GeneratedPool"].Spawn(projectile, barrels[currentBarrel].position,
                    offset*barrels[currentBarrel].rotation, null).gameObject as GameObject;
            F3DBeam beam = newGO.GetComponent<F3DBeam>();
            if (beam)
            {
                beam.SetOffset(gunOffset);
            }
            F3DAudioController.instance.RailGunShot(barrels[currentBarrel].position);
            if (ShellParticles.Length > 0)
                ShellParticles[currentBarrel].Emit(1);
            AdvanceSocket();
        }

        // Spawn rail gun weapon impact
        public void RailgunImpact(Vector3 pos)
        {
            F3DPoolManager.Pools["GeneratedPool"].Spawn(impactEffect, pos, Quaternion.identity, null);
            F3DAudioController.instance.RailGunHit(pos);
        }

        // Fire plasma gun weapon
        void PlasmaGun()
        {
            Quaternion offset = Quaternion.Euler(UnityEngine.Random.onUnitSphere);
            F3DPoolManager.Pools["GeneratedPool"].Spawn(muzzleFlash, barrels[currentBarrel].position,
                barrels[currentBarrel].rotation, barrels[currentBarrel]);
            GameObject newGO =
                F3DPoolManager.Pools["GeneratedPool"].Spawn(projectile, barrels[currentBarrel].position,
                    offset*barrels[currentBarrel].rotation, null).gameObject as GameObject;
            F3DProjectile proj = newGO.GetComponent<F3DProjectile>();
            if (proj)
            {
                proj.SetOffset(gunOffset);
            }
            F3DAudioController.instance.PlasmaGunShot(barrels[currentBarrel].position);
            AdvanceSocket();
        }

        // Spawn plasma gun weapon impact
        public void PlasmaGunImpact(Vector3 pos)
        {
            F3DPoolManager.Pools["GeneratedPool"].Spawn(impactEffect, pos, Quaternion.identity, null);
            F3DAudioController.instance.PlasmaGunHit(pos);
        }

        // Fire plasma beam weapon
        void PlasmaBeam()
        {
            for (int i = 0; i < barrels.Length; i++)
            {
                F3DPoolManager.Pools["GeneratedPool"].Spawn(beamEffect, barrels[i].position, barrels[i].rotation,
                    barrels[i]);   
            } 
            F3DAudioController.instance.PlasmaBeamLoop(transform.position, transform.parent);
        }

        // Fire heavy beam weapon
        void PlasmaBeamHeavy()
        {
            for (int i = 0; i < barrels.Length; i++)
            {
                F3DPoolManager.Pools["GeneratedPool"].Spawn(beamEffect, barrels[i].position, barrels[i].rotation,
                    barrels[i]);
            }  
            F3DAudioController.instance.PlasmaBeamHeavyLoop(transform.position, transform.parent);
        }

        // Fire lightning gun weapon
        void LightningGun()
        {
            for (int i = 0; i < barrels.Length; i++)
            {
                F3DPoolManager.Pools["GeneratedPool"].Spawn(beamEffect, barrels[i].position, barrels[i].rotation,
                    barrels[i]);
            }   
            F3DAudioController.instance.LightningGunLoop(transform.position, transform);
        }

        // Fire flames weapon
        void FlameRed()
        {
            for (int i = 0; i < barrels.Length; i++)
            {
                F3DPoolManager.Pools["GeneratedPool"].Spawn(beamEffect, barrels[i].position, barrels[i].rotation,
                    barrels[i]);
            }   
            F3DAudioController.instance.FlameGunLoop(transform.position, transform);
        }

        // Fire laser pulse weapon
        void LaserImpulse()
        {
            Quaternion offset = Quaternion.Euler(UnityEngine.Random.onUnitSphere);
            F3DPoolManager.Pools["GeneratedPool"].Spawn(muzzleFlash, barrels[currentBarrel].position,
                barrels[currentBarrel].rotation, barrels[currentBarrel]);
            GameObject newGO =
                F3DPoolManager.Pools["GeneratedPool"].Spawn(projectile, barrels[currentBarrel].position,
                    offset*barrels[currentBarrel].rotation, null).gameObject as GameObject;
            F3DProjectile proj = newGO.GetComponent<F3DProjectile>();
            if (proj)
            {
                proj.SetOffset(gunOffset);
            }
            F3DAudioController.instance.LaserImpulseShot(barrels[currentBarrel].position);

            AdvanceSocket();
        }

        // Spawn laser pulse weapon impact
        public void LaserImpulseImpact(Vector3 pos)
        {
            F3DPoolManager.Pools["GeneratedPool"].Spawn(impactEffect, pos, Quaternion.identity, null);
            F3DAudioController.instance.LaserImpulseHit(pos);
        }
    }
}