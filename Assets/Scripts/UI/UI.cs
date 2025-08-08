using UnityEngine;

public class UI : MonoBehaviour
{
    public UI_SkillTooltip skillTooltip;

    private void Awake() {
        skillTooltip = GetComponentInChildren<UI_SkillTooltip>();
    }
}
