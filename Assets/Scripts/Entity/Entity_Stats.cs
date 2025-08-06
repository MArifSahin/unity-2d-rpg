using NUnit.Framework;
using UnityEngine;

public class Entity_Stats : MonoBehaviour
{
    public Stat maxHealth; // maximum health points
    public Stat_MajorGroup major;
    public Stat_OffenseGroup offense;
    public Stat_DefenseGroup defense;

    public float GetPhysicalDamage(out bool isCrit)
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

        return finalDamage;
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
        float baseMaxHealth = maxHealth.GetValue();
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
}
