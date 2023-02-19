using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionFadeOut : MonoBehaviour
{
    public float minimum = 0.0f;
    public float maximum = 1f;
    public float speed = 5.0f;
    public float threshold = float.Epsilon;

    public SpriteRenderer sprite;

    void Update()
    {
        float step = speed * Time.deltaTime;
        sprite.color = new Color(1f, 1f, 1f, Mathf.Lerp(sprite.color.a, minimum, step));
    }
}
