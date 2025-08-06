using UnityEngine;

public class VFX_AutoController : MonoBehaviour
{
    [SerializeField] private bool autoDestroy = true;
    [SerializeField] private float destroyDelay = 1f;

    [SerializeField] private bool randomOffset = true;
    [SerializeField] private bool randomRotation = true;

    [Header("Random Rotation")]
    [SerializeField] private float minRotation = 0f;
    [SerializeField] private float maxRotation = 360f;


    [Header("Random Position")]
    [SerializeField] private float xMinOffset = -.3f;
    [SerializeField] private float xMaxOffset = .3f;
    [Space]
    [SerializeField] private float yMinOffset = -.3f;
    [SerializeField] private float yMaxOffset = .3f;

    void Start()
    {
        ApplyRandomOffset();
        ApplyRandomRotation();
        if (autoDestroy)
        {
            Destroy(gameObject, destroyDelay);
        }
    }

    private void ApplyRandomOffset()
    {
        if (randomOffset)
        {
            float xOffset = Random.Range(xMinOffset, xMaxOffset);
            float yOffset = Random.Range(yMinOffset, yMaxOffset);
            transform.position += new Vector3(xOffset, yOffset);
        }
    }

    private void ApplyRandomRotation()
    {
        if (randomRotation)
        {
            float randomZRotation = Random.Range(minRotation, maxRotation);
            transform.Rotate(0f, 0f, randomZRotation);
        }
    }
}
