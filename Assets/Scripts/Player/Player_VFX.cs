using System.Collections;
using UnityEngine;

public class Player_VFX : Entity_VFX
{
    [Header("Image Echo VFX")]
    [Range(0.01f, 0.2f)]
    [SerializeField] private float imageEchoInterval = 0.05f;
    [SerializeField] private GameObject imageEchoPrefab;
    private Coroutine imageEchoCo;

    public void DoImageEchoEffect(float duration)
    {
        if (imageEchoCo != null)
        {
            StopCoroutine(imageEchoCo);
        }
        imageEchoCo = StartCoroutine(ImageEchoCo(duration));
    }

    private IEnumerator ImageEchoCo(float duration)
    {
        float time = 0;

        while (time < duration)
        {
            CreateImageEcho();
            yield return new WaitForSeconds(imageEchoInterval);
            time += imageEchoInterval;
        }
    }

    private void CreateImageEcho()
    {
        GameObject imageEcho = Instantiate(imageEchoPrefab, transform.position, transform.rotation);
        imageEcho.GetComponentInChildren<SpriteRenderer>().sprite = sr.sprite;
        // Ensure the echo is not parented to anything that moves vertically
        imageEcho.transform.parent = null;
    }
}
