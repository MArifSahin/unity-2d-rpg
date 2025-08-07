using UnityEngine;

[CreateAssetMenu(menuName = "RPG Setup/Default Stat Setup", fileName = "DefaultStatSetup")]
public class Stat_SetupSO : ScriptableObject
{
    [Header("Resources")]
    public float maxHealth;
    public float healthRegeneration;

    [Header("Offence - Physical Damage")]
    public float attackSpeed = 1f;
    public float damage = 10f;
    public float critChance = 0.1f;
    public float critPower = 150f;
    public float armorReduction;

    [Header("Offence - Elemental Damage")]
    public float fireDamage;
    public float iceDamage;
    public float lightningDamage;

    [Header("Defence - Pyhsical Resistance")]
    public float armor;
    public float evasion;

    [Header("Defence - Elemental Resistance")]
    public float fireResistance;
    public float iceResistance;
    public float lightningResistance;

    [Header("Major Stats")]
    public float strength;
    public float agility;
    public float intelligence;
    public float vitality;
}
