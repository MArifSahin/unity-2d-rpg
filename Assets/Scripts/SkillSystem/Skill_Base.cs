using UnityEngine;

public class Skill_Base : MonoBehaviour
{
    [Header("General Details")]
    [SerializeField] private float cooldown;
    private float lastUsedTime;

    protected virtual void Awake()
    {
        lastUsedTime -= cooldown;
    }

    public bool CanUseSkill() => !OnCooldown();

    private bool OnCooldown() => Time.time < lastUsedTime + cooldown;

    public void SetSkillOnCooldown() => lastUsedTime = Time.time;

    public void ResetCooldown(float cooldownReduction) => lastUsedTime += cooldownReduction;

    public void ResetCooldown() => lastUsedTime = 0f;
}
