using System;
using UnityEngine;

[Serializable]
public class Stat
{
    [SerializeField] private float baseValue;

    public float GetValue()
    {
        return baseValue;
    }

    //buff or items affecting base value can be added here
    //all calculations should be done here
}
