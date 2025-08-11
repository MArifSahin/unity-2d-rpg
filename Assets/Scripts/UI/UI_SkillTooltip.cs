using System.Collections;
using System.Text;
using TMPro;
using UnityEngine;

public class UI_SkillTooltip : UI_Tooltip
{
    private UI ui;
    private UI_SkillTree skillTree;

    [SerializeField] private TextMeshProUGUI skillName;
    [SerializeField] private TextMeshProUGUI skillDescription;
    [SerializeField] private TextMeshProUGUI skillRequirement;

    [Space]
    [SerializeField] private string metConditionHex;
    [SerializeField] private string unmetConditionHex;
    [SerializeField] private string importantInfoHex;
    [SerializeField] private Color exampleColor;
    [SerializeField] private string lockedSkillText = "You've taken different path - this skill is locked.";

    private Coroutine textEffectCoroutine;

    override protected void Awake()
    {
        base.Awake();
        ui = GetComponentInParent<UI>();
        //pass true if you want to access to inactive objects
        skillTree = ui.GetComponentInChildren<UI_SkillTree>(true);
    }

    public override void ShowTooltip(bool show, RectTransform targetRect)
    {
        base.ShowTooltip(show, targetRect);
    }

    public void ShowTooltip(bool show, RectTransform targetRect, UI_TreeNode treeNode)
    {
        base.ShowTooltip(show, targetRect);

        if (!show) return;

        skillName.text = treeNode.skillData.skillName;
        skillDescription.text = treeNode.skillData.description;

        string skillLockedText = GetColoredText(importantInfoHex, lockedSkillText);
        string requirements = treeNode.isLocked ? skillLockedText : GetRequirements(treeNode.skillData.cost, treeNode.neededNodes, treeNode.blockedNodes);

        skillRequirement.text = requirements;
    }

    private IEnumerator TextBlinkEffectCo(TextMeshProUGUI text, float blinkInterval, int blinkCount)
    {
        for (int i = 0; i < blinkCount; i++)
        {
            text.text = GetColoredText(unmetConditionHex, text.text);
            yield return new WaitForSeconds(blinkInterval);
            text.text = GetColoredText(importantInfoHex, text.text);
            yield return new WaitForSeconds(blinkInterval);
        }
    }

    public void LockedSkillEffect()
    {
        if (textEffectCoroutine != null)
        {
            StopCoroutine(textEffectCoroutine);
        }
        textEffectCoroutine = StartCoroutine(TextBlinkEffectCo(skillRequirement, 0.15f, 3));
    }

    private string GetRequirements(int skillCost, UI_TreeNode[] neededNodes, UI_TreeNode[] blockedNodes = null)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("Requirements:\n");

        string costColor = skillTree.EnoughSkillPoints(skillCost) ? metConditionHex : unmetConditionHex;;
        sb.AppendLine(GetColoredText(costColor, $" - {skillCost} skill point(s)"));

        foreach (var node in neededNodes)
        {
            string nodeColor = node.isUnlocked ? metConditionHex : unmetConditionHex;
            sb.AppendLine(GetColoredText(nodeColor, $" - {node.skillData.skillName}"));
        }

        if (blockedNodes != null && blockedNodes.Length > 0)
        {
            sb.AppendLine(GetColoredText(importantInfoHex, $"Locks out: "));
            foreach (var node in blockedNodes)
            {
                string nodeColor = node.isUnlocked ? metConditionHex : unmetConditionHex;
                sb.AppendLine(GetColoredText(nodeColor, $" - {node.skillData.skillName}"));
            }
        }

        return sb.ToString();
    }
}
