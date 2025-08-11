using UnityEngine;

public class UI_Tooltip : MonoBehaviour
{
    private RectTransform tooltipRectTransform;
    [SerializeField] private Vector2 offset = new Vector2(300, 20);

    protected virtual void Awake()
    {
        tooltipRectTransform = GetComponent<RectTransform>();
    }

    public virtual void ShowTooltip(bool show, RectTransform targetRect)
    {
        if (!show)
        {
            tooltipRectTransform.position = new Vector2(9999, 9999);
        }

        if (targetRect)
            UpdatePosition(targetRect);
    }

    private void UpdatePosition(RectTransform targetRect)
    {
        float screenCenterX = Screen.width / 2;
        float screenTop = Screen.height;
        float screenBottom = 0;

        Vector2 targetPosition = targetRect.position;

        targetPosition.x = targetPosition.x < screenCenterX
            ? targetPosition.x + offset.x
            : targetPosition.x - offset.x;

        float tooltipVerticalHalf = tooltipRectTransform.sizeDelta.y / 2;
        float topY = targetPosition.y + tooltipVerticalHalf;
        float bottomY = targetPosition.y - tooltipVerticalHalf;

        if (topY > screenTop)
        {
            targetPosition.y = screenTop - tooltipVerticalHalf - offset.y;
        }
        else if (bottomY < screenBottom)
        {
            targetPosition.y = screenBottom + tooltipVerticalHalf + offset.y;
        }

        tooltipRectTransform.position = targetPosition;
    }

    protected string GetColoredText(string color, string text)
    {
        return $"<color={color}>{text}</color>";
    }
}
