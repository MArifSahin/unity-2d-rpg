using System.Collections;
using UnityEngine;

public class VFX_AutoController : MonoBehaviour
{
    private SpriteRenderer sr;

    [SerializeField] private bool autoDestroy = true;
    [SerializeField] private float destroyDelay = 1f;

    [SerializeField] private bool randomOffset = true;
    [SerializeField] private bool randomRotation = true;
    [SerializeField] private bool canFade;
    [SerializeField] private float fadeSpeed = 1f;

    [Header("Random Rotation")]
    [SerializeField] private float minRotation = 0f;
    [SerializeField] private float maxRotation = 360f;


    [Header("Random Position")]
    [SerializeField] private float xMinOffset = -.3f;
    [SerializeField] private float xMaxOffset = .3f;
    [Space]
    [SerializeField] private float yMinOffset = -.3f;
    [SerializeField] private float yMaxOffset = .3f;

    private void Awake() {
        sr = GetComponentInChildren<SpriteRenderer>();
    }

    void Start()
    {
        if (canFade)
        {
            StartCoroutine(FadeCo());
        }

        ApplyRandomOffset();
        ApplyRandomRotation();
        if (autoDestroy)
        {
            Destroy(gameObject, destroyDelay);
        }
    }

    private IEnumerator FadeCo()
    {
        Color targetColor = Color.white;

        while (targetColor.a > 0)
        {
            targetColor.a -= fadeSpeed * Time.deltaTime;
            sr.color = targetColor;
            yield return null;
        }
        
        sr.color = targetColor;
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
