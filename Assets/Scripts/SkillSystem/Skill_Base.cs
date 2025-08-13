using UnityEngine;

public class Skill_Base : MonoBehaviour
{
    [Header("General Details")]
    [SerializeField] protected SkillType skillType;
    [SerializeField] protected SkillUpgradeType skillUpgradeType;
    [SerializeField] private float cooldown;
    private float lastUsedTime;

    protected virtual void Awake()
    {
        lastUsedTime -= cooldown;
    }

    public void SetSkillUpgradeType(UpgradeData upgrade)
    {
        skillUpgradeType = upgrade.skillUpgradeType;
        cooldown = upgrade.cooldown;
    }

    public bool CanUseSkill() => !OnCooldown();

    protected bool Unlocked(SkillUpgradeType upgradeToCheck) => skillUpgradeType == upgradeToCheck;

    private bool OnCooldown() => Time.time < lastUsedTime + cooldown;

    public void SetSkillOnCooldown() => lastUsedTime = Time.time;

    public void ResetCooldown(float cooldownReduction) => lastUsedTime += cooldownReduction;

    public void ResetCooldown() => lastUsedTime = 0f;
}
