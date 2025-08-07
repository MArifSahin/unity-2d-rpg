using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Stat
{
    [SerializeField] private float baseValue;
    [SerializeField] private List<StatModifier> modifiers = new List<StatModifier>();

    private bool isModified = true;
    private float finalValue;

    public float GetValue()
    {
        if (isModified)
        {
            finalValue = GetFinalValue();
            isModified = false;
        }
        return finalValue;
    }

    public void AddModifier(float value, string source)
    {
        StatModifier modifier = new StatModifier(value, source);
        modifiers.Add(modifier);
        isModified = true;
    }

    public void RemoveModifier(string source)
    {
        modifiers.RemoveAll(modifier => modifier.source == source);
        isModified = true;
    }

    private float GetFinalValue()
    {
        finalValue = baseValue;
        foreach (var modifier in modifiers)
        {
            finalValue += modifier.value;
        }
        return finalValue;
    }

    public void SetBaseValue(float value)
    {
        baseValue = value;
    }
}

[Serializable]
public class StatModifier
{
    //E.g. Sword of Moon
    // +4 Damage, +5 Crit Chance etc. 
    public float value { get; private set; }
    public string source;

    public StatModifier(float value, string source)
    {
        this.value = value;
        this.source = source;
    }
}