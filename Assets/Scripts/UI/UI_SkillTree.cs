using UnityEngine;

public class UI_SkillTree : MonoBehaviour
{
    [SerializeField] private int skillPoint;
    [SerializeField] private UI_TreeConnectHandler[] parentNodes;


    private void Start() {
        UpdateAllConnections();
    }

    [ContextMenu("Reset Skill Tree")]
    public void RefundAllSkills()
    {
        UI_TreeNode[] allNodes = GetComponentsInChildren<UI_TreeNode>();

        foreach (var node in allNodes)
        {
            node.Refund();
        }
    }

    public void RemoveSkillPoint(int cost) => skillPoint -= cost;

    public bool EnoughSkillPoints(int cost) => skillPoint >= cost;

    public void AddSkillPoint(int point) => skillPoint += point;

    [ContextMenu("Update All Connections")]
    public void UpdateAllConnections()
    {
        foreach (var node in parentNodes)
        {
            node.UpdateAllConnections();
        }
    }
}
