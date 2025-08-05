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
}
