using UnityEngine;

public enum SkillUpgradeType
{
    // Dash Tree
    Dash,  //Dash to avoid damage
    Dash_CloneOnStart,  // Creates a clone when dash starts
    Dash_CloneOnStartAndArrival,  // Creates a clone when dash starts and arrives
    Dash_ShardOnStart,  // Creates a shard when dash starts
    Dash_ShardOnStartAndArrival  // Creates a shard when dash starts and arrives
}
