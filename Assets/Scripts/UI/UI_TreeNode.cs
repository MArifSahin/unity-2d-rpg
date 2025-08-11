using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_TreeNode : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    private UI ui;
    private RectTransform rectTransform;
    private UI_SkillTree skillTree;
    private UI_TreeConnectHandler connectHandler;

    [Header("Unlock Details")]
    public UI_TreeNode[] neededNodes;
    public UI_TreeNode[] blockedNodes;
    public bool isUnlocked;
    public bool isLocked;

    [Header("Skill Details")]
    [SerializeField] public Skill_DataSO skillData;
    [SerializeField] private string skillName;
    [SerializeField] private Image skillIcon;
    [SerializeField] private string lockedColorHex = "#9F9797";
    private Color lastColor;


    private void Awake()
    {
        ui = GetComponentInParent<UI>();
        rectTransform = GetComponent<RectTransform>();
        skillTree = GetComponentInParent<UI_SkillTree>();
        connectHandler = GetComponent<UI_TreeConnectHandler>();

        UpdateIconColor(GetColorByHex(lockedColorHex));
    }

    public void Refund()
    {
        isUnlocked = false;
        isLocked = false;
        UpdateIconColor(GetColorByHex(lockedColorHex));

        skillTree.AddSkillPoint(skillData.cost);
        connectHandler.UnlockConnectionImage(false);
    }

    private void Unlock()
    {
        isUnlocked = true;
        UpdateIconColor(Color.white);
        LockConflictNodes();

        skillTree.RemoveSkillPoint(skillData.cost);
        connectHandler.UnlockConnectionImage(true);

        //Find Player_SkillManager
        //Unlock skill on skill manager
        //skill manager unlock skill from skill data skill type
    }

    private bool CanBeUnlocked()
    {
        if (isLocked || isUnlocked) return false; //isLocked is for blocking other skills

        if (!skillTree.EnoughSkillPoints(skillData.cost)) return false;

        foreach (var node in neededNodes)
        {
            if (!node.isUnlocked) return false;
        }

        foreach (var node in blockedNodes)
        {
            if (node.isUnlocked) return false; //if any blocked node is unlocked, this node can not be unlocked
        }

        return true;
    }

    private void LockConflictNodes()
    {
        foreach (var node in blockedNodes)
        {
            node.isLocked = true;
        }

    }

    private void UpdateIconColor(Color color)
    {
        if (skillIcon == null) return;
        lastColor = skillIcon.color;
        skillIcon.color = color;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (CanBeUnlocked()) Unlock();
        else if (isLocked) ui.skillTooltip.LockedSkillEffect();
        else Debug.Log("Can not be unlocked");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ui.skillTooltip.ShowTooltip(true, rectTransform, this);

        if (!(isUnlocked || isLocked))
        {
            ToggleNodeHighlight(true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ui.skillTooltip.ShowTooltip(false, null);

        if (!(isUnlocked || isLocked))
            ToggleNodeHighlight(false);
    }

    private void ToggleNodeHighlight(bool highlight)
    {
        Color highLightColor = Color.white * .9f;
        highLightColor.a = 1;
        Color colorToApply = highlight ? highLightColor : lastColor;
        UpdateIconColor(colorToApply);

    }

    private Color GetColorByHex(String hex)
    {
        ColorUtility.TryParseHtmlString(hex, out Color color);
        return color;
    }

    void OnDisable()
    {
        if (isLocked)
            UpdateIconColor(GetColorByHex(lockedColorHex));
        if (isUnlocked)
            UpdateIconColor(Color.white);
    }

    void OnValidate()
    {
        if (!skillData) return;
        skillName = skillData.skillName;
        skillIcon.sprite = skillData.icon;
        gameObject.name = "UI_TreeNode_" + skillName;

    }

}
