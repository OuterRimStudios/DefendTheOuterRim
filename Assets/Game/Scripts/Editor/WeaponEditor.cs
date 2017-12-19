using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Weapon)), CanEditMultipleObjects]
public class WeaponEditor : Editor
{
    public SerializedProperty
        weaponType_Prop,
        myCamera_Prop,
        minDamage_Prop,
        maxDamage_Prop,
        critChance_Prop,
        critDmgMultiplier_Prop,
        fireFrequency_Prop,
        successiveShots_Prop,
        timeBetweenSuccessiveShots_Prop,
        hasResource_Prop,
        hasAmmo_Prop,
        ammo_Prop,
        unlimitedAmmo_Prop,
        multifire_Prop,
        alternateFire_Prop,
        hasReload_Prop,
        reloadTime_Prop,
        hasOverheat_Prop,
        maxHeatAmount_Prop,
        heatIncreaseAmount_Prop,
        heatDecreaseAmount_Prop,
        heatIncreaseFrequency_Prop,
        heatDecreaseFrequency_Prop,
        heatIncreaseDelay_Prop,
        overheatedLength_Prop,
        hasCooldown_Prop,
        cooldownLength_Prop,
        startCooldownWhenOutOfAmmo_Prop,
        startCooldownOnFire_Prop,
        startCooldownAfter_Prop,
        hasCharges_Prop,
        maxCharges_Prop,
        isDelayed_Prop,
        delayEffect_Prop,
        delayTime_Prop,
        autoFire_Prop,
        targetTag_Prop,
        detectionDistance_Prop,
        canAim_Prop,
        aimPosition_Prop,
        aimRotation_Prop,
        aimAssist_Prop,
        aimAssistAccuracy_Prop,
        applyDotOnHit_Prop,
        weaponFireEffects_Prop,
        weaponFirePositions_Prop,
        weaponAnimator_Prop,
        damageOverTimeEffect_Prop,
        timeBetweenDamage_Prop,
        dotLength_Prop,
        targetableLayer_Prop,
        hitScanAttackRange_Prop,
        hitScanAccuracy_Prop,
        targetHitEffect_Prop,
        environmentHitEffect_Prop,
        projectile_Prop,
        isSticky_Prop,
        damageCollider_Prop,
        sustainedEffect_Prop,
        sustainedEffectFrequency_Prop,
        sustainedDamageFrequency_Prop;

    void OnEnable()
    {
        // Setup the SerializedProperties
        weaponType_Prop = serializedObject.FindProperty("weaponType");
        myCamera_Prop = serializedObject.FindProperty("myCamera");
        minDamage_Prop = serializedObject.FindProperty("minDamage");
        maxDamage_Prop = serializedObject.FindProperty("maxDamage");
        critChance_Prop = serializedObject.FindProperty("critChance");
        critDmgMultiplier_Prop = serializedObject.FindProperty("critDamageMultiplier")
;       fireFrequency_Prop = serializedObject.FindProperty("fireFrequency");
        successiveShots_Prop = serializedObject.FindProperty("successiveShots");
        timeBetweenSuccessiveShots_Prop = serializedObject.FindProperty("timeBetweenSuccessiveShots");
        hasResource_Prop = serializedObject.FindProperty("hasResource");
        hasAmmo_Prop = serializedObject.FindProperty("hasAmmo");
        ammo_Prop = serializedObject.FindProperty("ammo");
        unlimitedAmmo_Prop = serializedObject.FindProperty("unlimitedAmmo");
        multifire_Prop = serializedObject.FindProperty("multifire");
        alternateFire_Prop = serializedObject.FindProperty("alternateFire");
        hasReload_Prop = serializedObject.FindProperty("hasReload");
        reloadTime_Prop = serializedObject.FindProperty("reloadTime");
        hasOverheat_Prop = serializedObject.FindProperty("hasOverheat");
        maxHeatAmount_Prop = serializedObject.FindProperty("maxHeatAmount");
        heatIncreaseAmount_Prop = serializedObject.FindProperty("heatIncreaseAmount");
        heatDecreaseAmount_Prop = serializedObject.FindProperty("heatDecreaseAmount");
        heatIncreaseFrequency_Prop = serializedObject.FindProperty("heatIncreaseFrequency");
        heatDecreaseFrequency_Prop = serializedObject.FindProperty("heatDecreaseFrequency");
        heatIncreaseDelay_Prop = serializedObject.FindProperty("heatIncreaseDelay");
        overheatedLength_Prop = serializedObject.FindProperty("overheatedLength");
        hasCooldown_Prop = serializedObject.FindProperty("hasCooldown");
        cooldownLength_Prop = serializedObject.FindProperty("cooldownLength");
        startCooldownWhenOutOfAmmo_Prop = serializedObject.FindProperty("startCooldownWhenOutOfAmmo");
        startCooldownOnFire_Prop = serializedObject.FindProperty("startCooldownOnFire");
        startCooldownAfter_Prop = serializedObject.FindProperty("startCooldownAfter");
        hasCharges_Prop = serializedObject.FindProperty("hasCharges");
        maxCharges_Prop = serializedObject.FindProperty("maxCharges");
        isDelayed_Prop = serializedObject.FindProperty("isDelayed");
        delayEffect_Prop = serializedObject.FindProperty("delayEffect");
        delayTime_Prop = serializedObject.FindProperty("delayTime");
        autoFire_Prop = serializedObject.FindProperty("autoFire");
        targetTag_Prop= serializedObject.FindProperty("targetTag");
        detectionDistance_Prop = serializedObject.FindProperty("detectionDistance");
        canAim_Prop = serializedObject.FindProperty("canAim");
        aimPosition_Prop = serializedObject.FindProperty("aimPosition");
        aimRotation_Prop = serializedObject.FindProperty("aimRotation");
        aimAssist_Prop = serializedObject.FindProperty("aimAssist");
        aimAssistAccuracy_Prop = serializedObject.FindProperty("aimAssistAccuracy");
        applyDotOnHit_Prop = serializedObject.FindProperty("applyDotOnHit");
        weaponFireEffects_Prop = serializedObject.FindProperty("weaponFireEffects");
        weaponFirePositions_Prop = serializedObject.FindProperty("weaponFirePositions");
        weaponAnimator_Prop = serializedObject.FindProperty("weaponAnimator");
        damageOverTimeEffect_Prop = serializedObject.FindProperty("damageOverTimeEffect");
        timeBetweenDamage_Prop = serializedObject.FindProperty("timeBetweenDamage");
        dotLength_Prop = serializedObject.FindProperty("dotLength");
        targetableLayer_Prop = serializedObject.FindProperty("targetableLayer");
        hitScanAttackRange_Prop = serializedObject.FindProperty("hitScanAttackRange");
        hitScanAccuracy_Prop = serializedObject.FindProperty("hitScanAccuracy");
        targetHitEffect_Prop = serializedObject.FindProperty("targetHitEffect");
        environmentHitEffect_Prop = serializedObject.FindProperty("environmentHitEffect");
        projectile_Prop = serializedObject.FindProperty("projectile");
        isSticky_Prop = serializedObject.FindProperty("isSticky");
        damageCollider_Prop = serializedObject.FindProperty("damageCollider");
        sustainedEffect_Prop = serializedObject.FindProperty("sustainedEffect");
        sustainedEffectFrequency_Prop = serializedObject.FindProperty("sustainedEffectFrequency");
        sustainedDamageFrequency_Prop = serializedObject.FindProperty("sustainedDamageFrequency");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(weaponType_Prop, new GUIContent("Select a Weapon Type."));

        Weapon.WeaponType weaponType = (Weapon.WeaponType)weaponType_Prop.enumValueIndex;

        switch (weaponType)
        {
            case Weapon.WeaponType.None:
                break;
            case Weapon.WeaponType.Hitscan:
                SharedVariables();
                HitScan();
               // EditorGUILayout.PropertyField(controllable_Prop, new GUIContent("controllable"));
                break;

            case Weapon.WeaponType.Projectile:
                SharedVariables();
                Projectile();
               // EditorGUILayout.PropertyField(controllable_Prop, new GUIContent("controllable"));
                break;

            case Weapon.WeaponType.Sustained:
                SharedVariables();
                Sustained();
               // EditorGUILayout.PropertyField(controllable_Prop, new GUIContent("controllable"));
                break;

        }
        serializedObject.ApplyModifiedProperties();
    }

    void SharedVariables()  //These Variables show up as soon as you have a weaponType selected.
    {
        Weapon weapon = (Weapon)target;

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Set the shared variables.", EditorStyles.boldLabel);
        EditorGUILayout.Space();

        EditorGUILayout.PropertyField(minDamage_Prop, new GUIContent("Minimum Damage"));
        EditorGUILayout.PropertyField(maxDamage_Prop, new GUIContent("Maximum Damage"));
        EditorGUILayout.PropertyField(critChance_Prop, new GUIContent("Critical Strike Chance"));
        EditorGUILayout.PropertyField(critDmgMultiplier_Prop, new GUIContent("Critical Damage Multiplier"));
        EditorGUILayout.PropertyField(fireFrequency_Prop, new GUIContent("Fire Frequency"));
        EditorGUILayout.PropertyField(successiveShots_Prop, new GUIContent("Successive Shots"));
        EditorGUILayout.PropertyField(timeBetweenSuccessiveShots_Prop, new GUIContent("Time Between Successive Shots"));

        EditorGUILayout.Space();

        EditorGUILayout.PropertyField(weaponFireEffects_Prop, new GUIContent("Weapon Fire Effects"), true);
        EditorGUILayout.PropertyField(weaponFirePositions_Prop, new GUIContent("Weapon Fire Positions"), true);
        EditorGUILayout.PropertyField(weaponAnimator_Prop, new GUIContent("Weapon Animator"));

        EditorGUILayout.Space();

        // ---- //

        EditorGUILayout.PropertyField(canAim_Prop, new GUIContent("Can Aim?"));

        if (weapon.canAim)
        {
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Aim Variables", EditorStyles.boldLabel);
            EditorGUILayout.Space();

            EditorGUILayout.PropertyField(aimPosition_Prop, new GUIContent("Aim Position"));
            EditorGUILayout.PropertyField(aimRotation_Prop, new GUIContent("Aim Rotation"));
        }

        EditorGUILayout.PropertyField(hasResource_Prop, new GUIContent("Uses Resource?"));

        if (weapon.hasResource)
        {
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("What type of resource?", EditorStyles.boldLabel);
            EditorGUILayout.Space();

            EditorGUILayout.PropertyField(hasAmmo_Prop, new GUIContent("Ammo"));
            EditorGUILayout.PropertyField(hasOverheat_Prop, new GUIContent("Overheat"));
            EditorGUILayout.PropertyField(hasCooldown_Prop, new GUIContent("Cooldown"));

            HasResource(weapon);

            EditorGUILayout.Space();
        }

        EditorGUILayout.PropertyField(multifire_Prop, new GUIContent("Multifire?"));

        if (weapon.multifire)
        {
            EditorGUILayout.PropertyField(alternateFire_Prop, new GUIContent("Alternating Fire?"));
        }

        EditorGUILayout.PropertyField(isDelayed_Prop, new GUIContent("Fire Delay?"));

        if (weapon.isDelayed)
        {
            EditorGUILayout.PropertyField(delayTime_Prop, new GUIContent("Delay Time"));
            EditorGUILayout.PropertyField(delayEffect_Prop, new GUIContent("Delay Effect"));
        }

        EditorGUILayout.PropertyField(autoFire_Prop, new GUIContent("Auto Fired?"));

        if (weapon.autoFire)
        {
            EditorGUILayout.PropertyField(targetTag_Prop, new GUIContent("Targetable Tag"));
            EditorGUILayout.PropertyField(detectionDistance_Prop, new GUIContent("Detection Distance"));
        }

        EditorGUILayout.PropertyField(aimAssist_Prop, new GUIContent("Aim Assist?"));

        if (weapon.aimAssist)
        {
            EditorGUILayout.PropertyField(aimAssistAccuracy_Prop, new GUIContent("Aim Assist Accuracy"));
        }

        EditorGUILayout.PropertyField(applyDotOnHit_Prop, new GUIContent("Applies DoT on Hit?"));

        if (weapon.applyDotOnHit)
        {
            EditorGUILayout.PropertyField(damageOverTimeEffect_Prop, new GUIContent("Dot Effect"));
            EditorGUILayout.PropertyField(timeBetweenDamage_Prop, new GUIContent("Time Between Damage"));
            EditorGUILayout.PropertyField(dotLength_Prop, new GUIContent("Length of Dot"));
        }
    }

    void HitScan()
    {
        EditorGUILayout.PropertyField(myCamera_Prop, new GUIContent("Player Camera"));
        EditorGUILayout.PropertyField(targetableLayer_Prop, new GUIContent("Targetable Layer"));
        EditorGUILayout.PropertyField(hitScanAttackRange_Prop, new GUIContent("Attack Range"));
        EditorGUILayout.PropertyField(hitScanAccuracy_Prop, new GUIContent("Hitscan Spread"));
        EditorGUILayout.PropertyField(targetHitEffect_Prop, new GUIContent("Target Hit Effect"));
        EditorGUILayout.PropertyField(environmentHitEffect_Prop, new GUIContent("Environmental Hit Effect"));
    }

    void Projectile()
    {
        EditorGUILayout.PropertyField(projectile_Prop, new GUIContent("Projectile"));
        EditorGUILayout.PropertyField(isSticky_Prop, new GUIContent("Is the projectile sticky?"));
    }

    void Sustained()
    {
        EditorGUILayout.PropertyField(damageCollider_Prop, new GUIContent("Sustained Damage Collider"));
        EditorGUILayout.PropertyField(sustainedEffect_Prop, new GUIContent("Sustained Effect"));
        EditorGUILayout.PropertyField(sustainedEffectFrequency_Prop, new GUIContent("Sustained Effect Active Frequency"));
        EditorGUILayout.PropertyField(sustainedDamageFrequency_Prop, new GUIContent("Sustained Damage Frequency"));
    }

    void HasResource(Weapon weapon)
    {

        if (weapon.hasAmmo)
        {
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Ammo Varaibles", EditorStyles.boldLabel);

            EditorGUILayout.PropertyField(ammo_Prop, new GUIContent("Max Ammo"));
            EditorGUILayout.PropertyField(unlimitedAmmo_Prop, new GUIContent("Unlimited Ammo?"));
        }

        if (weapon.hasOverheat)
        {
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Overheat Varaibles", EditorStyles.boldLabel);

            EditorGUILayout.PropertyField(maxHeatAmount_Prop, new GUIContent("Max Heat"));

            EditorGUILayout.PropertyField(heatIncreaseAmount_Prop, new GUIContent("Heat Increase Amount"));
            EditorGUILayout.PropertyField(heatDecreaseAmount_Prop, new GUIContent("Heat Decrease Amount"));

            EditorGUILayout.PropertyField(heatIncreaseFrequency_Prop, new GUIContent("Heat Increase Frequency"));
            EditorGUILayout.PropertyField(heatDecreaseFrequency_Prop, new GUIContent("Heat Decrease Frequency"));

            EditorGUILayout.PropertyField(heatIncreaseDelay_Prop, new GUIContent("Heat Increase Delay"));
            EditorGUILayout.PropertyField(overheatedLength_Prop, new GUIContent("Overheated Length"));
        }

        if (weapon.hasCooldown)
        {
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Cooldown Varaibles", EditorStyles.boldLabel);

            EditorGUILayout.PropertyField(cooldownLength_Prop, new GUIContent("Cooldown Length"));

            EditorGUILayout.PropertyField(startCooldownWhenOutOfAmmo_Prop, new GUIContent("Start Cooldown When out of Ammo"));
            EditorGUILayout.PropertyField(startCooldownOnFire_Prop, new GUIContent("Start Cooldown When Fired"));
            EditorGUILayout.PropertyField(startCooldownAfter_Prop, new GUIContent("Start Cooldown after time"));

            EditorGUILayout.PropertyField(hasCharges_Prop, new GUIContent("Ability has charges"));

            if (weapon.hasCharges)
                EditorGUILayout.PropertyField(maxCharges_Prop, new GUIContent("Overheated Length"));
        }
    }
}
