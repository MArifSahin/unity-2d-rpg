using NUnit.Framework;
using UnityEngine;

public class Entity_Stats : MonoBehaviour
{
    public Stat_SetupSO defaultStatSetup;

    public Stat_Resources resources;
    public Stat_MajorGroup major;
    public Stat_OffenseGroup offense;
    public Stat_DefenseGroup defense;

    public float GetElementalDamage(out ElementType element, float scaleFactor = 1)
    {
        float fireDamage = offense.fireDamage.GetValue();
        float iceDamage = offense.iceDamage.GetValue();
        float lightningDamage = offense.lightningDamage.GetValue();

        float bonusElementalDamage = major.intelligence.GetValue() * 1f; // Assuming each point of intellect gives 1 elemental damage
        //highest elemental damage is taken
        float highestElementalDamage = fireDamage;
        element = ElementType.Fire;

        if (iceDamage > highestElementalDamage)
        {
            highestElementalDamage = iceDamage;
            element = ElementType.Ice;
        }
        if (lightningDamage > highestElementalDamage)
        {
            highestElementalDamage = lightningDamage;
            element = ElementType.Lightning;
        }


        if (highestElementalDamage <= 0)
        {
            element = ElementType.None; // No elemental damage
            return 0f; // No elemental damage to return
        }

        float bonusFireDamage = (element == ElementType.Fire) ? 0f : fireDamage * 0.25f;
        float bonusIceDamage = (element == ElementType.Ice) ? 0f : iceDamage * 0.25f;
        float bonusLightningDamage = (element == ElementType.Lightning) ? 0f : lightningDamage * 0.25f;

        float weakerElementalDamage = bonusFireDamage + bonusIceDamage + bonusLightningDamage + bonusElementalDamage;
        float finalElementalDamage = highestElementalDamage + weakerElementalDamage;

        return finalElementalDamage * scaleFactor; // Apply scale factor if needed
    }

    public float GetElementalResistance(ElementType element)
    {
        float baseResistance = 0;
        float bonusResistance = major.intelligence.GetValue() * 0.5f; // Assuming each point of intellect gives 0.5 resistance

        switch (element)
        {
            case ElementType.Fire:
                baseResistance = defense.fireResistance.GetValue();
                break;
            case ElementType.Ice:
                baseResistance = defense.iceResistance.GetValue();
                break;
            case ElementType.Lightning:
                baseResistance = defense.lightningResistance.GetValue();
                break;
        }
        float finalResistance = baseResistance + bonusResistance;

        // Clamp the resistance to a maximum value, e.g., 75%
        float resistanceCap = 0.75f;
        finalResistance = Mathf.Clamp(finalResistance, 0, resistanceCap);

        return finalResistance;
    }

    public float GetPhysicalDamage(out bool isCrit, float scaleFactor = 1)
    {
        float baseDamage = offense.damage.GetValue();
        float bonusDamage = major.strength.GetValue();
        float totalBaseDamage = baseDamage + bonusDamage;

        float baseCritChance = offense.critChance.GetValue();
        float bonusCritChance = major.agility.GetValue() * 0.3f;
        float totalCritChance = baseCritChance + bonusCritChance;

        float baseCritPower = offense.critPower.GetValue();
        float bonusCritPower = major.strength.GetValue() * 0.5f;
        float totalCritPower = (baseCritPower + bonusCritPower) / 100f; // Convert crit power to a multiplier

        bool isCriticalHit = Random.Range(0f, 100) < totalCritChance;

        isCrit = isCriticalHit;
        float finalDamage = isCriticalHit
            ? totalBaseDamage * totalCritPower
            : totalBaseDamage;

        return finalDamage * scaleFactor; // Apply scale factor if needed
    }

    public float GetArmorMitigation(float armorReduction)
    {
        float baseArmor = defense.armor.GetValue();
        float bonusArmor = major.vitality.GetValue() * 1f; // Assuming each point of vitality gives 1 armor
        float totalArmor = baseArmor + bonusArmor;

        float armorReductionMultiplier = Mathf.Clamp01(1 - armorReduction); // Convert armor reduction to a multiplier
        totalArmor *= armorReductionMultiplier;

        float mitigation = totalArmor / (totalArmor + 100f); // Simple armor mitigation formula
        float mitigationCap = 0.80f; // 80% mitigation cap

        float finalArmor = Mathf.Clamp(mitigation, 0, mitigationCap);

        return finalArmor;
    }

    public float GetArmorReduction()
    {
        float finalReduction = offense.armorReduction.GetValue() / 100f; // Convert armor reduction to a percentage

        return finalReduction;
    }

    public float GetMaxHP()
    {
        float baseMaxHealth = resources.maxHealth.GetValue();
        float bonusMaxHealth = major.vitality.GetValue() * 5; // Assuming each point of vitality gives 5 max health
        float finalMaxHealth = baseMaxHealth + bonusMaxHealth;

        return finalMaxHealth;
    }

    public float GetEvasion()
    {
        float baseEvasion = defense.evasion.GetValue();
        float bonusEvasion = major.agility.GetValue() * 0.5f;

        float totalEvasion = baseEvasion + bonusEvasion;
        float evasionCap = 80;

        float finalEvasion = Mathf.Clamp(totalEvasion, 0, evasionCap);
        return finalEvasion;
    }

    public Stat GetStatByType(StatType statType)
    {
        switch (statType)
        {
            case StatType.MaxHealth:
                return resources.maxHealth;
            case StatType.HealthRegeneration:
                return resources.healthRegeneration;
            case StatType.Strength:
                return major.strength;
            case StatType.Intelligence:
                return major.intelligence;
            case StatType.Agility:
                return major.agility;
            case StatType.Vitality:
                return major.vitality;
            case StatType.AttackSpeed:
                return offense.attackSpeed;
            case StatType.Damage:
                return offense.damage;
            case StatType.CritChance:
                return offense.critChance;
            case StatType.CritPower:
                return offense.critPower;
            case StatType.Armor:
                return defense.armor;
            case StatType.ArmorReduction:
                return offense.armorReduction;
            case StatType.FireDamage:
                return offense.fireDamage;
            case StatType.IceDamage:
                return offense.iceDamage;
            case StatType.LightningDamage:
                return offense.lightningDamage;
            case StatType.FireResistance:
                return defense.fireResistance;
            case StatType.IceResistance:
                return defense.iceResistance;
            case StatType.LightningResistance:
                return defense.lightningResistance;
            case StatType.Evasion:
                return defense.evasion;
            default:
                throw new System.ArgumentOutOfRangeException(nameof(statType), statType, null);
        }
    }

    [ContextMenu("Apply Default Stat Setup")]
    public void ApplyDefaultStatSetup()
    {
        if (defaultStatSetup == null)
        {
            Debug.LogError("Stat Setup is not assigned in Entity_Stats.");
            return;
        }

        resources.maxHealth.SetBaseValue(defaultStatSetup.maxHealth);
        resources.healthRegeneration.SetBaseValue(defaultStatSetup.healthRegeneration);

        offense.attackSpeed.SetBaseValue(defaultStatSetup.attackSpeed);
        offense.damage.SetBaseValue(defaultStatSetup.damage);
        offense.critChance.SetBaseValue(defaultStatSetup.critChance);
        offense.critPower.SetBaseValue(defaultStatSetup.critPower);
        offense.armorReduction.SetBaseValue(defaultStatSetup.armorReduction);

        offense.fireDamage.SetBaseValue(defaultStatSetup.fireDamage);
        offense.iceDamage.SetBaseValue(defaultStatSetup.iceDamage);
        offense.lightningDamage.SetBaseValue(defaultStatSetup.lightningDamage);

        defense.armor.SetBaseValue(defaultStatSetup.armor);
        defense.evasion.SetBaseValue(defaultStatSetup.evasion);

        defense.fireResistance.SetBaseValue(defaultStatSetup.fireResistance);
        defense.iceResistance.SetBaseValue(defaultStatSetup.iceResistance);
        defense.lightningResistance.SetBaseValue(defaultStatSetup.lightningResistance);

        major.strength.SetBaseValue(defaultStatSetup.strength);
        major.agility.SetBaseValue(defaultStatSetup.agility);
        major.intelligence.SetBaseValue(defaultStatSetup.intelligence);
        major.vitality.SetBaseValue(defaultStatSetup.vitality);
    }
}
