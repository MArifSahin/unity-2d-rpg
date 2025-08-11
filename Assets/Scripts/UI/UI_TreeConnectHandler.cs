using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class UI_TreeConnectDetails
{
    public UI_TreeConnectHandler childNode;
    public NodeDirectionType directionType;
    [Range(100, 250)]
    public float length;
    [Range(-50f, 50f)]
    public float rotation;
    
}

public class UI_TreeConnectHandler : MonoBehaviour
{
    private RectTransform rect => GetComponent<RectTransform>();
    [SerializeField] private UI_TreeConnectDetails[] connectionsDetails;
    [SerializeField] private UI_TreeConnection[] connections;

    private Image connectionImage;
    private Color originalColor;

    private void Awake() {
        if (connectionImage != null)
            originalColor = connectionImage.color;
    }

    private void OnValidate()
    {
        if (connectionsDetails.Length <= 0)
        {
            return;
        }

        if (connectionsDetails.Length != connections.Length)
        {
            Debug.LogWarning("Connections details length does not match connections length. Please ensure they are the same.");
        }
        UpdateConnection();
    }

    public void UpdateConnection()
    {
        for (int i = 0; i < connectionsDetails.Length; i++)
        {
            if (i < connections.Length)
            {
                var details = connectionsDetails[i];
                var connection = connections[i];
                Vector2 targetPosition = connection.GetConnectionPoint(rect);
                Image connectionImage = connection.GetConnectionImage();

                connection.DirectConnection(details.directionType, details.length, details.rotation);

                if (details.childNode == null)
                {
                    continue;
                }

                details?.childNode?.SetPosition(targetPosition);
                details?.childNode?.SetConnectionImage(connectionImage);
                details.childNode?.transform.SetAsLastSibling();
            }
            else
            {
                Debug.LogWarning($"Connection details for index {i} does not have a corresponding connection.");
            }
        }
    }

    public void UpdateAllConnections()
    {
        UpdateConnection();

        foreach (var node in connectionsDetails)
        {
            node.childNode?.UpdateConnection();
        }
    }

    public void UnlockConnectionImage(bool unlocked)
    {
        if (connectionImage != null)
        {
            connectionImage.color = unlocked ? Color.white : originalColor;
        }
    }

    public void SetConnectionImage(Image image) => connectionImage = image;

    public void SetPosition(Vector2 position) => rect.anchoredPosition = position;
}
