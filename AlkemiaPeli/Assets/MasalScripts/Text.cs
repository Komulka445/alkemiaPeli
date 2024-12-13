using UnityEngine;
using TMPro;
public class Text : MonoBehaviour
{
    public float speed = 2f;
    public float maxScale = 1.2f;
    public float minScale = 0.8f;

    private Vector3 originalScale;

    void Start()
    {
        originalScale = transform.localScale;
    }

    void Update()
    {
        float scale = Mathf.PingPong(Time.time * speed, maxScale - minScale) + minScale;
        transform.localScale = originalScale * scale;
    }
}
