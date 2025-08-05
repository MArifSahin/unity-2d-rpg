using UnityEngine;

public class Entity_Stats : MonoBehaviour
{
    public Stat maxHealth; // maximum health points
    public Stat_MajorGroup major;
    public Stat_OffenseGroup offense;
    public Stat_DefenseGroup defense;

    public float GetMaxHP()
    {
        return maxHealth.GetValue() + (major.vitality.GetValue() * 5);
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
