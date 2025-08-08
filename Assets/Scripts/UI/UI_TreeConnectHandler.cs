using System;
using UnityEngine;

[Serializable]
public class UI_TreeConnectDetails
{
    public UI_TreeConnectHandler childNode;
    public NodeDirectionType directionType;
    [Range(100, 250)]
    public float length;
    
}

public class UI_TreeConnectHandler : MonoBehaviour
{
    private RectTransform rect;
    [SerializeField] private UI_TreeConnectDetails[] connectionsDetails;
    [SerializeField] private UI_TreeConnection[] connections;

    private void OnValidate()
    {
        if (!rect)
        {
            rect = GetComponent<RectTransform>();
        }

        if (connectionsDetails.Length != connections.Length)
        {
            Debug.LogWarning("Connections details length does not match connections length. Please ensure they are the same.");
        }
        UpdateConnection();
    }

    private void UpdateConnection()
    {
        for (int i = 0; i < connectionsDetails.Length; i++)
        {
            if (i < connections.Length)
            {
                var details = connectionsDetails[i];
                var connection = connections[i];
                Vector2 targetPosition = connection.GetConnectionPoint(rect);

                connection.DirectConnection(details.directionType, details.length);
                details.childNode.SetPosition(targetPosition);
            }
            else
            {
                Debug.LogWarning($"Connection details for index {i} does not have a corresponding connection.");
            }
        }
    }

    public void SetPosition(Vector2 position) => rect.anchoredPosition = position;
}
