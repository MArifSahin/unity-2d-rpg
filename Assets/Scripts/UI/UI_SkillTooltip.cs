using TMPro;
using UnityEngine;

public class UI_SkillTooltip : UI_Tooltip
{
    [SerializeField] private TextMeshProUGUI skillName;
    [SerializeField] private TextMeshProUGUI skillDescription;
    [SerializeField] private TextMeshProUGUI skillRequirement;

    public override void ShowTooltip(bool show, RectTransform targetRect)
    {
        base.ShowTooltip(show, targetRect);
    }

    public void ShowTooltip(bool show, RectTransform targetRect, Skill_DataSO skillData)
    {
        base.ShowTooltip(show, targetRect);

        if (!show) return;

        skillName.text = skillData.skillName;
        skillDescription.text = skillData.description;
        skillRequirement.text = $"Requirements:\n   - {skillData.cost} skill point";
    }
}
