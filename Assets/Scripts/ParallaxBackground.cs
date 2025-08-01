using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    private Camera mainCamera;
    private float lastCameraPosition;

    [SerializeField] private ParallaxLayer[] backgroundLayers;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        float currentCameraPositionX = mainCamera.transform.position.x;
        float distanceToMove = currentCameraPositionX - lastCameraPosition;
        lastCameraPosition = currentCameraPositionX;

        foreach (ParallaxLayer parallaxLayer in backgroundLayers)
        {
            parallaxLayer.Move(distanceToMove);
        }
    }
}
