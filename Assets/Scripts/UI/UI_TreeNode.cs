using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_TreeNode : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    [SerializeField] private Image skillIcon;
    [SerializeField] private string lockedColorHex = "#9F9797";
    private Color lastColor;
    public bool isUnlocked;
    public bool isLocked;

    private void Awake()
    {
        UpdateIconColor(GetColorByHex(lockedColorHex));
    }

    private void Unlock()
    {
        isUnlocked = true;
        UpdateIconColor(Color.white);
        //Find Player_SkillManager
        //Unlock skill on skill manager
    }

    private bool CanBeUnlocked()
    {
        if (isLocked || isUnlocked) return false; //isLocked is for blocking other skills
        return true;
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
        else Debug.Log("Can not be unlocked");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!isUnlocked)
            UpdateIconColor(Color.white * .9f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!isUnlocked)
            UpdateIconColor(lastColor);
    }

    private Color GetColorByHex(String hex)
    {
        ColorUtility.TryParseHtmlString(hex, out Color color);
        return color;
    }
}
