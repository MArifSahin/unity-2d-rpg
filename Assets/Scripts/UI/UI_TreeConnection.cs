using UnityEngine;
using UnityEngine.UI;

public class UI_TreeConnection : MonoBehaviour
{
    [SerializeField] private RectTransform rotationPoint;
    [SerializeField] private RectTransform connectionLength;
    [SerializeField] private RectTransform childNodeConnectionPoint;

    public void DirectConnection(NodeDirectionType directionType, float length, float offset)
    {
        bool shouldBeActive = directionType != NodeDirectionType.None;
        float finalLength = shouldBeActive ? length : 0f;
        float angle = GetDirectionAngle(directionType);

        rotationPoint.localRotation = Quaternion.Euler(0f, 0f, angle + offset);
        connectionLength.sizeDelta = new Vector2(finalLength, connectionLength.sizeDelta.y);
    }

    public Image GetConnectionImage()
    {
        return GetComponent<Image>();
    }

    public Vector2 GetConnectionPoint(RectTransform rect)
    {
        //TODO understand this code piece...
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            rect.parent as RectTransform,
            childNodeConnectionPoint.position,
            null, // Use the current camera
            out var localPosition
        );
        return localPosition;
    }

    private float GetDirectionAngle(NodeDirectionType directionType)
    {
        return directionType switch
        {
            NodeDirectionType.UpLeft => 135f,
            NodeDirectionType.UpRight => 45f,
            NodeDirectionType.DownLeft => 225f,
            NodeDirectionType.DownRight => 315f,
            NodeDirectionType.Up => 90f,
            NodeDirectionType.Down => 270f,
            NodeDirectionType.Right => 0f,
            NodeDirectionType.Left => 180f,
            _ => 0f
        };
    }
}

public enum NodeDirectionType
{
    None,
    UpLeft,
    UpRight,
    DownLeft,
    DownRight,
    Up,
    Down,
    Right,
    Left
}
