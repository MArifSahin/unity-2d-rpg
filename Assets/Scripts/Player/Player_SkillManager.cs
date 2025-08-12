using UnityEngine;

public class Player_SkillManager : MonoBehaviour
{
    public Skill_Dash dash { get; private set; }

    void Awake()
    {
        dash = GetComponentInChildren<Skill_Dash>();
    }

    public Skill_Base GetSkill(SkillType skillType)
    {
        switch (skillType)
        {
            case SkillType.Dash:
                return dash;
            // Add other skills here as needed
            default:
                Debug.LogWarning($"Skill of type {skillType} not found.");
                return null;
        }
    }
}
