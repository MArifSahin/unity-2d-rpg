using System;
using UnityEngine;

[Serializable]
public class Stat_OffenseGroup
{
    // Physical damage stats
    public Stat damage;
    public Stat critPower;
    public Stat critChance;

    //Elemental damage stats
    public Stat fireDamage;
    public Stat iceDamage;
    public Stat lightningDamage;
}
