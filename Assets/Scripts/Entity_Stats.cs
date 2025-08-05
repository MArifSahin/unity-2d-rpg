using UnityEngine;

public class Entity_Stats : MonoBehaviour
{
    public Stat maxHealth; // maximum health points
    public Stat vitality; // each point gives +5 HP

    public float GetMaxHP()
    {
        return maxHealth.GetValue() + (vitality.GetValue() * 5);
    }
}
