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
