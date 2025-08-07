using NUnit.Framework;
using UnityEngine;

public class Entity_Stats : MonoBehaviour
{
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

        float bonusFireDamage = (fireDamage == highestElementalDamage) ? 0f : fireDamage * 0.25f;
        float bonusIceDamage = (iceDamage == highestElementalDamage) ? 0f : iceDamage * 0.25f;
        float bonusLightningDamage = (lightningDamage == highestElementalDamage) ? 0f : lightningDamage * 0.25f;

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

    public Stat GetStatByType(Stat_Type statType)
    {
        switch (statType)
        {
            case Stat_Type.MaxHealth:
                return resources.maxHealth;
            case Stat_Type.HealthRegeneration:
                return resources.healthRegeneration;
            case Stat_Type.Strength:
                return major.strength;
            case Stat_Type.Intelligence:
                return major.intelligence;
            case Stat_Type.Agility:
                return major.agility;
            case Stat_Type.Vitality:
                return major.vitality;
            case Stat_Type.AttackSpeed:
                return offense.attackSpeed;
            case Stat_Type.Damage:
                return offense.damage;
            case Stat_Type.CritChance:
                return offense.critChance;
            case Stat_Type.CritPower:
                return offense.critPower;
            case Stat_Type.Armor:
                return defense.armor;
            case Stat_Type.ArmorReduction:
                return offense.armorReduction;
            case Stat_Type.FireDamage:
                return offense.fireDamage;
            case Stat_Type.IceDamage:
                return offense.iceDamage;
            case Stat_Type.LightningDamage:
                return offense.lightningDamage;
            case Stat_Type.FireResistance:
                return defense.fireResistance;
            case Stat_Type.IceResistance:
                return defense.iceResistance;
            case Stat_Type.LightningResistance:
                return defense.lightningResistance;
            case Stat_Type.Evasion:
                return defense.evasion;
            default:
                throw new System.ArgumentOutOfRangeException(nameof(statType), statType, null);
        }
    }
}
