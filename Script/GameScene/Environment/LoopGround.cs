using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class LoopGround : MonoBehaviour
{
    [SerializeField] private float speed = 1f;
    [SerializeField] private float width = 11.52f;

    private SpriteRenderer spriteRenderer;

    private Vector2 startSize;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        startSize = new Vector2(spriteRenderer.size.x, spriteRenderer.size.y);
    }

    private void Update()
    {
        spriteRenderer.size = new Vector2(spriteRenderer.size.x + speed * Time.deltaTime, spriteRenderer.size.y);

        if (spriteRenderer.size.x >= width)
    {
        spriteRenderer.size = startSize;
    }
    }

    
}