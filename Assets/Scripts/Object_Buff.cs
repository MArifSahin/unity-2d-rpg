using System.Collections;
using UnityEngine;

public class Object_Buff : MonoBehaviour
{
    private SpriteRenderer sr;

    [Header("Buff Details")]
    [SerializeField] private float buffDuration = 4f;
    [SerializeField] private bool canBeUsed = true;

    [Header("Floating Effect")]
    [SerializeField] private float floatSpeed = 1;
    [SerializeField] private float floatRange = 0.2f;
    private Vector3 startPosition;


    private void Awake()
    {
        startPosition = transform.position;
        sr = GetComponentInChildren<SpriteRenderer>();
    }

    private void Update()
    {
        float yOffset = Mathf.Sin(Time.time * floatSpeed) * floatRange;
        transform.position = startPosition + new Vector3(0, yOffset, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!canBeUsed) return;

        StartCoroutine(BuffCoroutine(buffDuration));
    }

    private IEnumerator BuffCoroutine(float buffDuration) {
        canBeUsed = false;
        sr.color = Color.clear; // Make the sprite invisible
        yield return new WaitForSeconds(buffDuration);

        Destroy(gameObject); // Destroy the buff object after the duration
    }
}
