using UnityEngine;

public class UI : MonoBehaviour
{
    public UI_SkillTooltip skillTooltip;
    public UI_SkillTree skillTree;
    public bool skillTreeEnabled;

    private void Awake()
    {
        skillTooltip = GetComponentInChildren<UI_SkillTooltip>();
        skillTree = GetComponentInChildren<UI_SkillTree>(true);
    }

    public void ToggleSkillTree()
    {
        skillTreeEnabled = !skillTreeEnabled;
        skillTree.gameObject.SetActive(skillTreeEnabled);
        skillTooltip.ShowTooltip(false, null);
    }
}
