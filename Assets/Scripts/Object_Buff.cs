using System;
using System.Collections;
using UnityEngine;

[Serializable]
public class Buff
{   
    public Stat_Type type; // Example buff type
    public float value = 5f; // Example value for the buff

    public Buff(Stat_Type buffType, float buffValue)
    {
        type = buffType;
        value = buffValue;
    }
}

public class Object_Buff : MonoBehaviour
{
    private SpriteRenderer sr;
    private Entity_Stats statsToModify;

    [Header("Buff Details")]
    [SerializeField] public string buffName = "Buff";
    [SerializeField] public float buffDuration = 4f;
    [SerializeField] private Buff[] buffs; // Array of buffs that can be applied
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

        statsToModify = collision.GetComponent<Entity_Stats>();

        StartCoroutine(BuffCoroutine(buffDuration));
    }

    private IEnumerator BuffCoroutine(float buffDuration)
    {
        canBeUsed = false;
        sr.color = Color.clear; // Make the sprite invisible

        ApplyBuff(true); // Apply the buffs

        yield return new WaitForSeconds(buffDuration);

        ApplyBuff(false); // Remove the buffs

        Destroy(gameObject); // Destroy the buff object after the duration
    }

    private void ApplyBuff(bool apply)
    {
        if (apply)
        {
            foreach (var buff in buffs)
            {
                statsToModify.GetStatByType(buff.type).AddModifier(buff.value, buffName);
            }
        }
        else
        {
            foreach (var buff in buffs)
            {
                statsToModify.GetStatByType(buff.type).RemoveModifier(buffName);
            }
        }
    }
}
