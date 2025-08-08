using System;
using UnityEngine;

[CreateAssetMenu(menuName = "RPG Setup/Skill Data", fileName = "Skill data - ")]
public class Skill_DataSO : ScriptableObject
{
    public int cost;

    [Header("Skill Description")]
    public string skillName;
    [TextArea]
    public string description;
    public Sprite icon;

    //skill type that you should unlock
}
