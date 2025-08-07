using System;
using UnityEngine;

[Serializable]
public class Stat_OffenseGroup
{
    public Stat attackSpeed;

    // Physical damage stats
    public Stat damage;
    public Stat critPower;
    public Stat critChance;
    public Stat armorReduction;


    //Elemental damage stats
    public Stat fireDamage;
    public Stat iceDamage;
    public Stat lightningDamage;
}
