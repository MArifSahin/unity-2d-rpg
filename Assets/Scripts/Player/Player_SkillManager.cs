using UnityEngine;

public class Player_SkillManager : MonoBehaviour
{
    public Skill_Dash dash { get; private set; }

    void Awake()
    {
        dash = GetComponentInChildren<Skill_Dash>();
        
    }
}
